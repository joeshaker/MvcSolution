using DataAccess.Data.Contexts;
using DataAccess.Repositories.Classes;
using DataAccess.Repositories.Interfaces;
using Demo.BusinessLogic.Profiles;
using Demo.BusinessLogic.Services.Classes;
using Demo.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Demo.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Add servic to the container
            // Add services to the container.
            builder.Services.AddControllersWithViews(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            });
            //builder.Services.AddScoped<AppDbContexts>();
            builder.Services.AddDbContext<AppDbContexts>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")); 
                options.UseLazyLoadingProxies();
            });
            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            builder.Services.AddScoped<IDepartmentService,DepartmentService>();
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IEmployeeServices, EmployeeService>();
            builder.Services.AddAutoMapper(m => m.AddProfile(new MappingProfiles()));
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            #endregion

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts(); // to check all request are secured 
            }

            app.UseHttpsRedirection();// redirect https request

            app.UseStaticFiles();// routing static files

            app.UseRouting();// routing from routing table

            //app.UseAuthorization();
            //app.UseAuthentication();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
