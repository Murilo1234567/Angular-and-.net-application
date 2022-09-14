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

namespace Application
{
    public class Startup
    {
        public string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public void ConfigureServices(IServiceCollection service)
        {
            service.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins, policy =>
                {
                    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });

            service.AddControllers();

            /*-------------------------------- DBCONTEXT --------------------------------*/
            string connectionString = "Server: dbjumpshop.cmarhm2qjxjb.us-east-1.rds.amazonaws.com, Port: 5432, User_Id: dbuser_jump, Password: a1MLC1pKTyDS0l8XESby, Database: dbprod_jumpshop";
            service.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));

            service.AddScoped<IBaseRepository<Product>, BaseRepository<Product>>();
            service.AddScoped<IBaseService<Product>, BaseService<Product>>();

            service.AddSingleton(new MapperConfiguration(config =>
            {
                config.CreateMap<Product, ProductModel>();
            }).CreateMapper());
        }

        public static void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}