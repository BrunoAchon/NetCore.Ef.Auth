using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WS.Data.Connection;
using WS.Data.Connections;
using WS.Data.Context;
using WS.Mananger.Interfaces.Services;

namespace WS.WebApi.Configuration
{
    public static class DataBaseConfig
    {
        public static void AddDataBaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<WsContext>(options => options
            .UseSqlServer(configuration.GetConnectionString("WsConnection")));

            services.AddScoped<IApplicationWriteDbConnection, ApplicationWriteDbConnection>();
            services.AddScoped<IApplicationReadDbConnection, ApplicationReadDbConnection>();
        }

        public static void UseDataBaseConfiguration(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            using var context = serviceScope.ServiceProvider.GetService<WsContext>();
            context.Database.Migrate();
            context.Database.EnsureCreated();
        }
    }
}
