using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace UCL.ISM.Authentication
{
    public static class DependencyInjection
    {
        public static void AddUserService(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<WebOptions>(configuration);
            services.AddSingleton<Service>();
        }
    }
}
