using Api.Core.Entities;
using System.Linq.Expressions;

namespace Api.Infraestructure.Interface
{
    public interface IRepository<T, IdType> where T : BaseEntity<IdType>
    {
        IEnumerable<T?> GetAll();

        Task<T?> GetById(IdType id);

        Task Add(T entity);

        void Update(T entity);

        Task Delete(IdType id);

        Task AddRange(ICollection<T> entity);

        void UpdateRange(ICollection<T> entity);

        Task<IEnumerable<T?>> GetAllAsync();

        IEnumerable<T> Where(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includeProperties);

        Task<IEnumerable<T>> WhereAsync(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includeProperties);
    }
}