using Lin.Data.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services,string ConnectionStrings)
        {
            services.AddDbContext<SystemDBContext>(Options =>
            {
                Options.UseSqlServer(ConnectionStrings);
            }, ServiceLifetime.Singleton);
            return services;
        }
    }
}
