using Microsoft.EntityFrameworkCore;
using VeterinaryClinic.Domain.Models;
using VeterinaryClinic.Domain.Repositories;

namespace VeterinaryClinic.Infrastructure.PostgreSQL;

/// <summary>
/// Репозиторий EF Core с загрузкой связей, необходимых аналитическим запросам.
/// </summary>
public sealed class PostgreSqlRepository<T>(ClinicDbContext db) : IRepository<T> where T : class
{
    private IQueryable<T> Query()
    {
        IQueryable<T> query = db.Set<T>();
        if (typeof(T) == typeof(Pet)) query = query.Include("Breed").Include("Owner");
        if (typeof(T) == typeof(Veterinarian)) query = query.Include("Specialization");
        if (typeof(T) == typeof(Appointment)) query = query.Include("Pet.Breed").Include("Pet.Owner").Include("Veterinarian.Specialization");
        return query.AsSplitQuery();
    }

    private static int Id(T entity) => (int)(typeof(T).GetProperty("Id")?.GetValue(entity)
        ?? throw new InvalidOperationException("У сущности отсутствует Id"));


    private async Task FillOwnerPetsAsync(CancellationToken cancellationToken)
    {
        if (typeof(T) != typeof(Owner)) return;
        var owners = db.Owners.Local.ToDictionary(x => x.Id);
        foreach (var owner in owners.Values) owner.Pets.Clear();
        var pets = await db.Pets.Include(x => x.Owner).Include(x => x.Breed).ToListAsync(cancellationToken);
        foreach (var pet in pets)
            if (pet.Owner is not null && owners.TryGetValue(pet.Owner.Id, out var owner) && !owner.Pets.Contains(pet))
                owner.Pets.Add(pet);
    }

    public async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var result = await Query().ToListAsync(cancellationToken);
        await FillOwnerPetsAsync(cancellationToken);
        return result;
    }
    public async Task<IReadOnlyList<T>> FindAsync(Func<T, bool> predicate, CancellationToken cancellationToken = default) =>
        (await GetAllAsync(cancellationToken)).Where(predicate).ToArray();
    public async Task<bool> AnyAsync(Func<T, bool> predicate, CancellationToken cancellationToken = default) =>
        (await GetAllAsync(cancellationToken)).Any(predicate);
    public async Task<int> CountAsync(Func<T, bool> predicate, CancellationToken cancellationToken = default) =>
        (await GetAllAsync(cancellationToken)).Count(predicate);
    public async Task<T?> GetAsync(int id, CancellationToken cancellationToken = default)
    {
        var result = await Query().ToListAsync(cancellationToken);
        await FillOwnerPetsAsync(cancellationToken);
        return result.FirstOrDefault(x => Id(x) == id);
    }
    public async Task<int> NextIdAsync(CancellationToken cancellationToken = default)
    {
        var entities = await db.Set<T>().ToListAsync(cancellationToken);
        return entities.Select(Id).DefaultIfEmpty().Max() + 1;
    }
    public async Task<T> CreateAsync(Func<int, T> factory, CancellationToken cancellationToken = default)
    {
        var entity = factory(await NextIdAsync(cancellationToken));
        await db.Set<T>().AddAsync(entity, cancellationToken);
        await db.SaveChangesAsync(cancellationToken);
        return entity;
    }
    public async Task<T> UpdateAsync(int id, Action<T> apply, CancellationToken cancellationToken = default)
    {
        var entity = await GetAsync(id, cancellationToken) ?? throw new KeyNotFoundException($"Entity with ID {id} was not found");
        apply(entity);
        await db.SaveChangesAsync(cancellationToken);
        return entity;
    }
    public async Task<bool> RemoveAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await GetAsync(id, cancellationToken);
        if (entity is null) return false;
        db.Set<T>().Remove(entity);
        await db.SaveChangesAsync(cancellationToken);
        return true;
    }
}
