using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

[assembly: FunctionsStartup(typeof(Todo.Api.Startup))]

namespace Todo.Api
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {

            var serviceCollection = (ServiceCollection)builder.Services;
            var config = serviceCollection.FirstOrDefault(p => p.ServiceType == typeof(IConfiguration))?.ImplementationInstance as IConfiguration;


            string SqlConnection = Environment.GetEnvironmentVariable("SqlConnectionString");
            builder.Services.AddDbContext<ToDoContext>(
                options => options.UseNpgsql(SqlConnection));
        }
    }
}
