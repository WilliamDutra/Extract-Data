using ExtractData.Domain;
using ExtractData.Domain.Interfaces;
using ExtractData.Domain.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExtractData.UI
{
    public static class Startup
    {
        public static ServiceProvider Container { get; set; }

        public static void Register(IServiceCollection serviceCollection)
        {
            var service = serviceCollection;

            service.AddScoped<ISQL, SQL>();
            service.AddScoped<IMySql, MysqlServerService>();
            
            Container = service.BuildServiceProvider();

        }

    }
}
