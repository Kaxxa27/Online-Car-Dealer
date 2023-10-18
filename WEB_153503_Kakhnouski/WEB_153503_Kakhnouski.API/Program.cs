using Microsoft.EntityFrameworkCore;
using WEB_153503_Kakhnouski.API.Data;
using WEB_153503_Kakhnouski.API.Services.CarCategoryService;
using WEB_153503_Kakhnouski.API.Services.CarService;
using WEB_153503_Kakhnouski.Services.CarService;
using WEB_153503_Kakhnouski.Services.CategoryServicep;

namespace WEB_153503_Kakhnouski.API;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddScoped<ICarCategoryService, CarCategoryService>();
        builder.Services.AddScoped<ICarService, CarService>();


        // Database configuration.
        var connection = builder.Configuration.GetConnectionString("Default");
        builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(connection));


        var app = builder.Build();

        //await DbInitializer.SeedData(app);

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseStaticFiles();
        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}