using WEB_153503_Kakhnouski.Models;
using WEB_153503_Kakhnouski.Services.CarCategoryService;
using WEB_153503_Kakhnouski.Services.CarService;
using WEB_153503_Kakhnouski.Services.CategoryServicep;

namespace WEB_153503_Kakhnouski
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();


            builder.Services.AddScoped<ICarCategoryService, ApiCarCategoryService>();
            builder.Services.AddScoped<ICarService, ApiCarService>();


            UriData uriData = builder.Configuration.GetSection("UriData").Get<UriData>()!;

            builder.Services.AddHttpClient<ICarService, ApiCarService>(opt => opt.BaseAddress = new Uri(uriData.ApiUri));
            builder.Services.AddHttpClient<ICarCategoryService, ApiCarCategoryService>(opt => opt.BaseAddress = new Uri(uriData.ApiUri));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.MapRazorPages();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}