namespace VeterinaryClinic.Application.Repositories;

/// <summary>
/// Определяет базовые операции доступа к данным
/// </summary>
public interface IRepository<T> where T : class
{
    /// <summary>
    /// Возвращает все сущности
    /// </summary>
    public IReadOnlyList<T> GetAll();
    /// <summary>
    /// Возвращает сущность по идентификатору
    /// </summary>
    public T? Get(int id);
    /// <summary>
    /// Возвращает следующий свободный идентификатор
    /// </summary>
    public int NextId();
    /// <summary>
    /// Добавляет сущность
    /// </summary>
    public void Add(T entity);
    /// <summary>
    /// Удаляет сущность по идентификатору
    /// </summary>
    public bool Remove(int id);
}
