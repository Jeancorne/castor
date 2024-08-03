using Api.Core.Entities;
using Api.Infraestructure.Data;
using Api.Infraestructure.Interface;
using Microsoft.EntityFrameworkCore;

namespace Api.Infraestructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(DBContext context)
        {
            _context = context;
        }

        private readonly DBContext _context;

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }

        public void saveChanges()
        {
            _context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            DateTime dateShort = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
            //Verificar si hay datos nuevos
            var insertedEntries = _context.ChangeTracker.Entries().Where(x => x.State == EntityState.Added).Select(x => x.Entity);
            foreach (var insertedEntry in insertedEntries) { insertedEntry.GetType().GetProperty("CreatedAt")?.SetValue(insertedEntry, dateShort); }
            //Verificar si se han actualizado datos
            var modifiedEntries = _context.ChangeTracker.Entries().Where(x => x.State == EntityState.Modified).Select(x => x.Entity);
            foreach (var modifiedEntry in modifiedEntries) { modifiedEntry.GetType().GetProperty("UpdatedAt")?.SetValue(modifiedEntry, dateShort); }
            int count = await _context.SaveChangesAsync();
            return count;
        }

        private readonly IRepository<ClsUser, Guid>? _ClsUser = null;

        private readonly IRepository<ClsEmployees, int>? _ClsEmployees = null;

        private readonly IRepository<ClsJobs, Guid>? _ClsJobs = null;

        public IRepository<ClsEmployees, int> ClsEmployees => _ClsEmployees ?? new BaseRepository<ClsEmployees, int>(_context);

        public IRepository<ClsUser, Guid> ClsUser => _ClsUser ?? new BaseRepository<ClsUser, Guid>(_context);

        public IRepository<ClsJobs, Guid> ClsJobs => _ClsJobs ?? new BaseRepository<ClsJobs, Guid>(_context);
    }
}