namespace VeterinaryClinic.Domain.Repositories;

/// <summary>
/// Основные операции с данными
/// </summary>
public interface IRepository<T> where T : class
{
    /// <summary>
    /// Возвращает все записи
    /// </summary>
    public IReadOnlyList<T> GetAll();
    /// <summary>Возвращает записи по условию</summary>
    IReadOnlyList<T> Find(Func<T, bool> predicate);
    /// <summary>Проверяет наличие записи по условию</summary>
    bool Any(Func<T, bool> predicate);
    /// <summary>Считает записи по условию</summary>
    int Count(Func<T, bool> predicate);
    /// <summary>
    /// Возвращает запись по идентификатору
    /// </summary>
    public T? Get(int id);
    /// <summary>
    /// Возвращает следующий свободный идентификатор
    /// </summary>
    public int NextId();
    /// <summary>
    /// Добавляет запись в репозиторий
    /// </summary>
    public void Add(T entity);
    /// <summary>Создаёт запись с новым идентификатором</summary>
    T Create(Func<int, T> factory);
    /// <summary>Обновляет запись по идентификатору</summary>
    T Update(int id, Action<T> apply);
    /// <summary>
    /// Удаляет запись по идентификатору
    /// </summary>
    public bool Remove(int id);
}
