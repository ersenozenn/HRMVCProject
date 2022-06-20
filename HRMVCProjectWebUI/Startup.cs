using DataAccess.Repositories;
using DataAccess.Repositories.Abstract;
using FluentValidation.AspNetCore;
using HRMVCProjectBusiness.ErrorMessages;
using HRMVCProjectBusiness.Services.Abstract;
using HRMVCProjectBusiness.Services.Concrete;
using HRMVCProjectDataAccess.Data;
using HRMVCProjectDataAccess.Repositories.Abstract;
using HRMVCProjectDataAccess.Repositories.Concrete;
using HRMVCProjectEntities.Concrete;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRMVCProjectWebUI
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
            services.AddControllersWithViews().AddFluentValidation
                (a => a.RegisterValidatorsFromAssemblyContaining<Startup>());//validation için *(validationlar bi yerde tutulmalý ve bunu sisteme bildirmeliyiz diye )
            services.AddRazorPages();
                        
            services.AddDbContext<HRMVCProjectDbContext>();

            //services.AddIdentity<User, UserRole>().AddEntityFrameworkStores<HRMVCProjectDbContext>();

            services.AddDefaultIdentity<User>()
                .AddRoles<UserRole>()
                .AddEntityFrameworkStores<HRMVCProjectDbContext>().AddDefaultTokenProviders().AddErrorDescriber<CustomIdentityErrorDescriber>().AddEntityFrameworkStores<HRMVCProjectDbContext>();

            //services.AddIdentity<User, UserRole>().AddUserStore<User>().AddRoleStore<UserRole>();
            //services.ConfigureApplicationCookie(options =>
            //{
            //    options.Cookie.HttpOnly = true;
            //    options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
            //    options.AccessDeniedPath = "/Error/Index";
            //});

            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
            services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));

            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IEmployeeService, EmployeeService>();

            services.AddScoped<IPermissionRepository, PermissionRepository>();
            services.AddScoped<IPermissionService, PermissionService>();

            services.AddScoped<IPermissionTypeRepository, PermissionTypeRepository>();
            services.AddScoped<IPermissionTypeService, PermissionTypeService>();

            services.AddScoped<IPermissionRepository, PermissionRepository>();
            services.AddScoped<IPermissionService, PermissionService>();

            services.AddScoped<IPermissionTypeRepository, PermissionTypeRepository>();
            services.AddScoped<IPermissionTypeService, PermissionTypeService>();

            services.AddScoped<IAdvancePaymentRepository, AdvancePaymentRepository>();
            services.AddScoped<IAdvancePaymentService, AdvancePaymentService>();
            
            services.AddScoped<ICostRepositroy, CostRepository>();
            services.AddScoped<ICostService, CostService>();
            
            services.AddScoped<ICostTypeRepository, CostTypeRepository>();
            services.AddScoped<ICostTypeService, CostTypeService>();

            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<ICompanyService, CompanyService>();

            services.AddMvc()
            .AddSessionStateTempDataProvider();
            services.AddSession();  
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            //else
            //{
            //    app.UseExceptionHandler("/Home/Error");
            //    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //    app.UseHsts();
            //}
            //This code added for seeing and fixing request error 
            app.UseDeveloperExceptionPage();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSession();


            //app.UseEndpoints(endpoints =>
            //{                               
            //    endpoints.MapRazorPages();
            //});



            //app.UseEndpoints(endpoints =>
            //{
            //    //endpoints.MapAreaControllerRoute(
            //    //   name: "Manager",
            //    //   areaName: "Manager",
            //    //   pattern: "Manager/{controller=Home}/{action=Index}");

            //    endpoints.MapControllerRoute(
            //        name: "User",
            //        pattern: "{controller=Employee}/{action=Index}/{id?}");

            //    endpoints.MapRazorPages();
            //});

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "area",
                    pattern: "{area:exists}/{controller}/{action}/{id?}");

            });

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllerRoute(
            //        name: "signin",
            //        pattern: "{controller=LogIn}/{action=SignIn}/{id?}");

            //});

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                
            });
        }
    }
}
