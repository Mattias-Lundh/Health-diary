using Fitness.Authorization;
using Fitness.Controller.Contracts;
using Fitness.Data.access;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Injection
{
    public static class ServiceCollectionExtension
    {
        public static void AddHealthDairyDependencies(this IServiceCollection services)
        {
            services.AddSingleton(new Database("Server=DESKTOP-3CSF1CU;Database=health;Trusted_Connection=True;"));
            services.AddSingleton<JWT>();
            services.AddSingleton<AppUserAuthentication>();            
        }
    }
}
