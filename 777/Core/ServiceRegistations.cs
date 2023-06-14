using System.Configuration;

namespace _777.Core
{
    public static class ServiceRegistations
    {
        public static void AddRegistrations(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ReCapthcaKeys>(configuration.GetSection("ReCapthca"));
        }
    }
}
