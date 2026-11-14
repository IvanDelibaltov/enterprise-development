using VeterinaryClinic.Domain.Repositories;

namespace VeterinaryClinic.Infrastructure;

/// <summary>Репозиторий тестовых данных в оперативной памяти.</summary>
public sealed class InMemoryRepository<T>(IEnumerable<T> initial, Func<T, int> idSelector) : IRepository<T> where T : class
{
    private readonly List<T> _items = initial.ToList();
    public Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Task.FromResult<IReadOnlyList<T>>(_items.ToArray());
    }
    public Task<IReadOnlyList<T>> FindAsync(Func<T, bool> predicate, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Task.FromResult<IReadOnlyList<T>>(_items.Where(predicate).ToArray());
    }
    public Task<bool> AnyAsync(Func<T, bool> predicate, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Task.FromResult(_items.Any(predicate));
    }
    public Task<int> CountAsync(Func<T, bool> predicate, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Task.FromResult(_items.Count(predicate));
    }
    public Task<T?> GetAsync(int id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Task.FromResult(_items.FirstOrDefault(x => idSelector(x) == id));
    }
    public Task<int> NextIdAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Task.FromResult(_items.Count == 0 ? 1 : _items.Max(idSelector) + 1);
    }
    public async Task<T> CreateAsync(Func<int, T> factory, CancellationToken cancellationToken = default)
    {
        var entity = factory(await NextIdAsync(cancellationToken));
        _items.Add(entity);
        return entity;
    }
    public async Task<T> UpdateAsync(int id, Action<T> apply, CancellationToken cancellationToken = default)
    {
        var entity = await GetAsync(id, cancellationToken) ?? throw new KeyNotFoundException($"Entity with ID {id} was not found");
        apply(entity);
        return entity;
    }
    public async Task<bool> RemoveAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await GetAsync(id, cancellationToken);
        return entity is not null && _items.Remove(entity);
    }
}
