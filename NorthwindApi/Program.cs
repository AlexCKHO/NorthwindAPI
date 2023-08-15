
using Microsoft.EntityFrameworkCore;
using NorthwindApi.Data;
using NorthwindApi.Data.Repositories;
using NorthwindApi.Models;
using NorthwindApi.Services;
using NorthwindAPI.Services;

namespace NorthwindApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();
            // Add services to the container.


            var dbConnection = builder.Configuration["DefaultConnection"];

            builder.Services.AddDbContext<NorthwindContext>(
                opt => opt.UseSqlServer(dbConnection)); //scope lifetime //connecting the db

            builder.Services.AddControllers().AddNewtonsoftJson(
                opt => opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                //stopping the loop from happening  in supplier calls product, product calls supplier

                );


            builder.Services.AddScoped(typeof(INorthwindRepository<>), typeof(NorthwindRepository<>));
            builder.Services.AddScoped(typeof(INorthwindService<>), typeof(NorthwindService<>)); //use typeof keyword becuase it is generic
            builder.Services.AddScoped<INorthwindRepository<Product>, ProductsRepository>();
            builder.Services.AddScoped<INorthwindRepository<Supplier>, SuppliersRepository>(); //DI

            //Dependency injection

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            /*            builder.Services.AddEndpointsApiExplorer();
                        builder.Services.AddSwaggerGen();*/

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                /*                app.UseSwagger();
                                app.UseSwaggerUI();*/

                app.UseDeveloperExceptionPage();



            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}