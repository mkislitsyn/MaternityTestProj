using Maternity.Services.Interfaces;
using Maternity.Services.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Maternity.Services.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<IPatientService, PatientService>();
            return services;
        }
    }
}
