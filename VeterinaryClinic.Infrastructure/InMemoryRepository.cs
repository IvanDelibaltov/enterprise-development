using VeterinaryClinic.Application.Repositories;
namespace VeterinaryClinic.Infrastructure;

public sealed class InMemoryRepository<T>(IEnumerable<T> initial, Func<T, int> idSelector) : IRepository<T> where T : class
{
    private readonly List<T> _items = initial.ToList();
    public IReadOnlyList<T> GetAll() => _items.ToArray();
    public T? Get(int id) => _items.FirstOrDefault(x => idSelector(x) == id);
    public int NextId() => _items.Count == 0 ? 1 : _items.Max(idSelector) + 1;
    public void Add(T entity) => _items.Add(entity);
    public bool Remove(int id)
    {
        var entity = Get(id);
        return entity is not null && _items.Remove(entity);
    }
}
