using Api.Core.Entities;
using Api.Infraestructure.Data;
using Api.Infraestructure.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Api.Infraestructure.Repository
{
    public class BaseRepository<T, IdType> : IRepository<T, IdType> where T : BaseEntity<IdType>
    {
        protected readonly DBContext _context;
        protected readonly DbSet<T> _entities;

        public BaseRepository(DBContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        public IEnumerable<T?> GetAll() => _entities.AsEnumerable();

        public async Task<T?> GetById(IdType id) => await _entities.FindAsync(id);

        public async Task Add(T entity)
        {
            await _entities.AddAsync(entity);
        }

        public void Update(T entity) => _entities.Update(entity);

        public async Task Delete(IdType id)
        {
            T? entity = await GetById(id);
            if (entity != null) _entities.Remove(entity);
        }

        public async Task AddRange(ICollection<T> entity) => await _entities.AddRangeAsync(entity);

        public void UpdateRange(ICollection<T> entity) => _entities.UpdateRange(entity);

        public virtual IEnumerable<T> Where(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = this._entities.Where(where).AsQueryable().AsNoTracking();
            query = PerformInclusions(includeProperties, query);
            return query;
        }

        public async Task<IEnumerable<T?>> GetAllAsync()
        {
            return await _entities.ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> WhereAsync(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = this._entities.Where(where).AsQueryable().AsNoTracking();
            query = PerformInclusions(includeProperties, query);
            return await query.ToListAsync();
        }

        public IQueryable<T> PerformInclusions(IEnumerable<Expression<Func<T, object>>> includeProperties, IQueryable<T> query)
        {
            return includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }
    }
}