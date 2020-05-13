using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sloth.DB.Repositories;

namespace Sloth.DB
{
    public static class Bootstrap
    {
        public static IServiceCollection AddSlothDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .AddDbContextPool<ISlothDbContext, SlothDbContext>(options =>
                {
                    var connectionString = configuration.GetConnectionString("SlothDatabase");

                    if (string.IsNullOrWhiteSpace(connectionString))
                    {
                        throw new Exception("DB ConnectionString is empty");
                    }

                    options
                        .EnableSensitiveDataLogging()
                        .UseSqlServer(connectionString);
                });
        }
        public static IServiceCollection AddSlothRepositories(this IServiceCollection services)
        {
            return services.AddTransient<IUserRepository, UserRepository>()
                            .AddTransient<ISessionRefreshTokenRepository, SessionRefreshTokenRepository>();
        }
    }
}
