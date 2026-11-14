using System.Linq.Expressions;
namespace VeterinaryClinic.Domain.Repositories;

/// <summary>
/// Определяет единый набор операций чтения и изменения сущностей в хранилище
/// </summary>
/// <typeparam name="T">Тип сущности, хранящейся в репозитории</typeparam>
public interface IRepository<T> where T : class
{









    Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
    Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
    Task<int> CountAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
    Task<T?> GetAsync(int id, CancellationToken cancellationToken = default);
    Task<int> NextIdAsync(CancellationToken cancellationToken = default);
    Task<T> CreateAsync(Func<int, T> factory, CancellationToken cancellationToken = default);
    Task<T> UpdateAsync(int id, Action<T> apply, CancellationToken cancellationToken = default);
    Task<bool> RemoveAsync(int id, CancellationToken cancellationToken = default);
}
