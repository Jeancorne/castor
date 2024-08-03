using Api.Core.Entities;

namespace Api.Infraestructure.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        void saveChanges();

        Task<int> SaveChangesAsync();

        IRepository<ClsUser, Guid> ClsUser { get; }

        IRepository<ClsEmployees, int> ClsEmployees { get; }

        IRepository<ClsJobs, Guid> ClsJobs { get; }
    }
}