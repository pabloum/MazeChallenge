using System;
using MazeChallenge.Game.Contracts;
using MazeChallenge.Persistence.UnitOfWork;

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
    }
}

