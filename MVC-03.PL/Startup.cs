using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MVC_03.DAL.Data;
using MVC_03.DAL.Models;
using MVC_03.PL.Helpers;
using MVC_03.PLL.Interfaces;
using MVC_03.PLL.Repositries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace MVC_03.PL
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddDbContext<AppDbContext>(options =>
             options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
            sqlOptions => sqlOptions.MigrationsAssembly("MVC-03.DAL")));


        //    services.AddScoped<IDepartmentRepository, DepartmentRepository>();

          //  services.AddScoped<IEmployeeRepository,EmplyeeRepository>();
          services.AddScoped<IUniteOfWork ,uniteOfWork > ();
            services.AddAutoMapper(M=>M.AddProfile(new mappingProfile()));
            services.AddIdentity<ApplicationUser, IdentityRole>(config =>
            {
                config.Password.RequiredUniqueChars = 2;
                config.Password.RequireDigit=true;
                config.Password.RequireLowercase=true;
                config.Password.RequireUppercase=true;
            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

         //   services.AddAuthentication()
           //     .AddCookie("Nedla", config =>
             //   {
               //     config.LoginPath = "/Account/SignIn";
                 //   config.AccessDeniedPath= "/Home/Erorr";
                  
               // });
               services.ConfigureApplicationCookie(confg=>

               {
                   confg.LogoutPath = "/Account/SignIn";
                   confg.ExpireTimeSpan= TimeSpan.FromMinutes(10);

			   });

        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
               
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
