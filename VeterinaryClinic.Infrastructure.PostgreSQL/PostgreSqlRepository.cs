using System.Linq.Expressions;
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


    private async Task FillOwnerPetsAsync(CancellationToken cancellationToken, int? ownerId = null)
    {
        if (typeof(T) != typeof(Owner)) return;

        var owners = db.Owners.Local
            .Where(x => ownerId == null || x.Id == ownerId.Value)
            .ToDictionary(x => x.Id);

        if (owners.Count == 0) return;

        foreach (var owner in owners.Values)
            owner.Pets.Clear();

        var ownerIds = owners.Keys.ToArray();

        var pets = await db.Pets
            .Include(x => x.Breed)
            .Where(x => x.Owner != null && ownerIds.Contains(x.Owner.Id))
            .ToListAsync(cancellationToken);

        foreach (var pet in pets)
        {
            if (pet.Owner is not null && owners.TryGetValue(pet.Owner.Id, out var owner) &&
                !owner.Pets.Contains(pet))
            {
                owner.Pets.Add(pet);
            }
        }
    }

    public async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var result = await Query().ToListAsync(cancellationToken);
        await FillOwnerPetsAsync(cancellationToken);
        return result;
    }
    public async Task<IReadOnlyList<T>> FindAsync(
        Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return await Query().Where(predicate).ToListAsync(cancellationToken);
    }

    public async Task<bool> AnyAsync(
        Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return await db.Set<T>().AnyAsync(predicate, cancellationToken);
    }

    public async Task<int> CountAsync(
        Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return await db.Set<T>().CountAsync(predicate, cancellationToken);
    }

    public async Task<T?> GetAsync(int id, CancellationToken cancellationToken = default)
    {
        var result = await Query()
            .FirstOrDefaultAsync(x => EF.Property<int>(x, "Id") == id, cancellationToken);

        await FillOwnerPetsAsync(cancellationToken, id);
        return result;
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
