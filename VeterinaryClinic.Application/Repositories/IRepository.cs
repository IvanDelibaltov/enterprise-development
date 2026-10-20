namespace VeterinaryClinic.Application.Repositories;

public interface IRepository<T> where T : class
{
    public IReadOnlyList<T> GetAll();
    public T? Get(int id);
    public int NextId();
    public void Add(T entity);
    public bool Remove(int id);
}
