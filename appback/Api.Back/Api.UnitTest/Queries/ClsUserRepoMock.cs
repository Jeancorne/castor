using Api.Application.Services;
using Api.Core.Entities;
using Api.Infraestructure.Data;
using Api.Infraestructure.Interface;
using Api.UnitTest.Utilities;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Linq.Expressions;

namespace Api.UnitTest.Queries
{
    public class ClsUserRepoMock
    {
        private Mock<IRepository<ClsUser, Guid>> repoClsUser = new Mock<IRepository<ClsUser, Guid>>();
        public Guid _id = new Guid("6b2bab98-0807-43c3-ab7c-18dd4200b8af");
        private List<ClsUser> lsUser = new List<ClsUser>();
        private ClsUser user = new ClsUser();

        public ClsUserRepoMock()
        {
            user.Id = _id;
            user.Name = "name";
            user.LastName = "apellido";
            user.Email = "prueba1@gmail.com";
            user.Password = UserServices.passwordHash("123");
            lsUser = new List<ClsUser>();
            lsUser.Add(user);
            repoClsUser.Setup(repo => repo.GetById(It.IsAny<Guid>())).ReturnsAsync((Guid id) => lsUser.FirstOrDefault(e => e.Id == id));
            repoClsUser.Setup(repo => repo.GetAllAsync()).ReturnsAsync(lsUser);
            repoClsUser.Setup(a => a.Where(It.Is<Expression<Func<ClsUser, bool>>>(predicate => predicate.Compile().Invoke(user))))
                 .Returns((Expression<Func<ClsUser, bool>> where, Expression<Func<ClsUser, object>>[] includeProperties) =>
                 {
                     TestableRepository<ClsUser, Guid> testableRepo = new TestableRepository<ClsUser, Guid>(new DBContext());
                     IQueryable<ClsUser> query = lsUser.AsQueryable().Where(where).AsNoTracking();
                     query = testableRepo.TestablePerformInclusions(includeProperties, query);
                     return query.ToList();
                 });

            repoClsUser.Setup(a => a.WhereAsync(It.IsAny<Expression<Func<ClsUser, bool>>>(), It.IsAny<Expression<Func<ClsUser, object>>[]>()))
                .ReturnsAsync((Expression<Func<ClsUser, bool>> where, Expression<Func<ClsUser, object>>[] includeProperties) =>
                {
                    TestableRepository<ClsUser, Guid> testableRepo = new TestableRepository<ClsUser, Guid>(new DBContext());
                    IQueryable<ClsUser> query = lsUser.AsQueryable().Where(where).AsNoTracking();
                    query = testableRepo.TestablePerformInclusions(includeProperties, query);
                    return query.ToList();
                });
        }

        public Mock<IRepository<ClsUser, Guid>> GetRepo()
        {
            return repoClsUser;
        }
    }
}