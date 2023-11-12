using System;
using MazeChallenge.Domain.Context;
using MazeChallenge.Game.Contracts;
using MazeChallenge.Persistence.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace MazeChallenge.API.Installers
{
    public static class BusinessServiceInstaller
    {
        public static void AddBusinessServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IConfiguration>(configuration);
            services.RegisterAllDirectImplementations<IService>(ServiceLifetime.Scoped);
        }

        public static void AddAdditionalInterfaces(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            //services.AddScoped<IRequestHandler, RequestHandler>();
        }

        public static void AddDataBases(this IServiceCollection services, IConfiguration configuration)
        {

            if (configuration.GetValue<bool>("UseDataBase"))
            {
                services.AddDbContext<MazeDbContext>(options =>
                    options.UseSqlServer(configuration.GetValue<string>("ConnectionStrings:MazeChallengeDb")));
                using (var context = services.BuildServiceProvider().GetRequiredService<MazeDbContext>())
                {
                    context.Database.EnsureCreated();
                }
            }
            else
            {
                services.AddDbContext<MazeDbContext>(
                    opt => {
                        opt.UseInMemoryDatabase("Discoteque");
                    }
                );
            }
        }
    }
}

