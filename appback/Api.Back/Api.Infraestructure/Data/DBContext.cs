using Api.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Api.Infraestructure.Data
{
    public class DBContext : DbContext
    {
        public DBContext()
        {
        }

        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }

        /// <summary>
        /// ================================================= TABLAS ==========================================
        /// </summary>
        ///
        public virtual DbSet<ClsUser> ClsUser { get; set; }

        public virtual DbSet<ClsEmployees> ClsEmployees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseInMemoryDatabase("TestDatabase");
            }
        }
    }
}