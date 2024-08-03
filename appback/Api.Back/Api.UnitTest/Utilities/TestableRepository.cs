using Api.Core.Entities;
using Api.Infraestructure.Data;
using Api.Infraestructure.Repository;
using System.Linq.Expressions;

namespace Api.UnitTest.Utilities
{
    public class TestableRepository<T, IdType> : BaseRepository<T, IdType> where T : BaseEntity<IdType>
    {
        public TestableRepository(DBContext context) : base(context)
        {
        }

        public IQueryable<T> TestablePerformInclusions(IEnumerable<Expression<Func<T, object>>> includeProperties, IQueryable<T> query)
        {
            return PerformInclusions(includeProperties, query);
        }
    }
}