using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Data.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AutoMapper;
using Application.Models;
using Service.Services;
using Domain.Interfaces;
using Domain.Entities;
using Data.Repository;
using Microsoft.AspNetCore.Authentication.Certificate;

namespace Application
{
    public class Startup
    {
        private readonly static string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public static void ConfigureServices(IServiceCollection service)
        {
            service.AddAuthentication(
                CertificateAuthenticationDefaults.AuthenticationScheme)
                .AddCertificate();

            service.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins, policy =>
                {
                    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });

            service.AddControllers();

            /*-------------------------------- DBCONTEXT --------------------------------*/
            string connectionString = "Server=localhost;Port=5432;User Id=postgres;Password=senha123;Database=localhost";
            service.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));

            service.AddScoped<IBaseRepository<Product>, BaseRepository<Product>>();
            service.AddScoped<IBaseService<Product>, BaseService<Product>>();

            service.AddSingleton(new MapperConfiguration(config =>
            {
                config.CreateMap<ProductModel, Product>();
                config.CreateMap<Product, ProductModel>();
            }).CreateMapper());
        }

        public static void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            
            app.UseCors(MyAllowSpecificOrigins);

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}