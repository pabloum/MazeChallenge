using System;
using MazeChallenge.API.Installers;
using MazeChallenge.API.Middleware;

namespace MazeChallenge.API
{
	public class StartUp
	{
        public IConfiguration configRoot { get; }

        public StartUp(IConfiguration configuration)
        {
            configRoot = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Hotels Reservations API", Version = "v1" });
            });

            services.AddBusinessServices(configRoot);
            services.AddAdditionalInterfaces();
        }

        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            // Configure the HTTP request pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            else
            {
                app.UseHttpsRedirection();
            }

            app.UseRouting();

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.MapControllers();

            app.Run();
        }
    }
}

