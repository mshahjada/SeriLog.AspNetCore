using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebLogger.Config
{
    public static class ConfigDependency
    {
        public static void AddConfigurationDepdency(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<LoggerSettings>(opt => configuration.GetSection("AppLogger").Bind(opt));
        }
    }
}
