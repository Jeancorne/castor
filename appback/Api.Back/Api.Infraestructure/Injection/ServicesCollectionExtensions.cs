using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Api.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;


namespace Api.Infraestructure.Injection
{
    public static class ServicesCollectionExtensions
    {
        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration, bool memory)
        {
            services.AddDbContext<DBContext>(a =>
            {
                var optionsBuilder = new DbContextOptionsBuilder<DBContext>();
                if (memory)
                {
                    a.UseInMemoryDatabase("TestDatabase");
                }
                else
                {
                    a.UseMySql(configuration.GetConnectionString("ConnectionBD"), ServerVersion.AutoDetect(configuration.GetConnectionString("ConnectionBD")));
                }
            });
        }
    }
}
