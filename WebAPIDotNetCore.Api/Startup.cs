using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;
using WebAPIDotNetCore.Api.Clients;
using WebAPIDotNetCore.Domain;
using WebAPIDotNetCore.Domain.Interfaces.Repositories;
using WebAPIDotNetCore.Domain.Interfaces.Services;
using WebAPIDotNetCore.Domain.Repositories;
using WebAPIDotNetCore.Domain.Services;

namespace WebAPIDotNetCore.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connection = Configuration["Database:ConnectionString"];
            services.AddDbContext<HPContext>(options =>
                options.UseSqlite(connection, options =>
                {
                    options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
                })
            );

            services.AddHttpClient<IPotterAPIClient, PotterAPIClient>();

            services.AddScoped<ICharacterService, CharacterService>();
            services.AddScoped<ICharacterRepository, CharacterRepository>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", 
                    new OpenApiInfo
                    {
                        Title = "Harry Potter",
                        Version = "v1",
                        Contact = new OpenApiContact
                        {
                            Name = "Vinicius Consulmagnos Romeiro",
                            Url = new Uri("https://github.com/vromeiro")
                        }
                    });

                string caminhoAplicacao =
                    PlatformServices.Default.Application.ApplicationBasePath;
                string nomeAplicacao =
                    PlatformServices.Default.Application.ApplicationName;
                string caminhoXmlDoc =
                    Path.Combine(caminhoAplicacao, $"{nomeAplicacao}.xml");

                c.IncludeXmlComments(caminhoXmlDoc);
            });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<HPContext>();
                context.Database.EnsureCreated();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Harry Potter");
            });
        }
    }
}
