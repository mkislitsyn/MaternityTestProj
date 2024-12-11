using Maternity.Repository.DbContexts;
using Maternity.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Maternity.Repository.Extensions
{
    public static class ApplicationRepositoryExtensions
    {
        public static IServiceCollection AddApplicationRepositories(this IServiceCollection services, IConfiguration config, string connectioDb)
        {
          

            services.AddDbContext<MaternityContext>(options => options.UseSqlServer(connectioDb));
            
            services.AddTransient<IPatientRepository, PatientRepository>();

            return services;
        }
    }
}
