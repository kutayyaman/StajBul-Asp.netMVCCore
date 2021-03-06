using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using EmailService;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using StajBul.Data.Abstract;
using StajBul.Data.Concrete.EfCore;
using StajBul.Entity;
using StajBul.Service;
using StajBul.Service.Impl;
using StajBul.WebUI.Infrastructure;
using StajBul.WebUI.Util;

namespace StajBul.WebUI
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


            AddDependency.addLocalization(services);

            var emailConfig = Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();
            services.AddSingleton(emailConfig);
            AddDependency.addAllDependencies(services);
            //services.AddDbContext<StajBulContext>(options =>
            //options.UseNpgsql(Configuration.GetConnectionString("StajBulConnectionPostgreSQL"), b=>b.MigrationsAssembly("StajBul.WebUI")));

            services.AddDbContext<StajBulContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("StajBulConnectionMYSQL"), b=>b.MigrationsAssembly("StajBul.WebUI")));

            services.PostConfigure<CookieAuthenticationOptions>(IdentityConstants.ApplicationScheme,
                                                                opt =>
                                                                {
                                                                    //configure your other properties
                                                                    opt.LoginPath = "/User/Login";
                                                                });

            
            services.AddIdentity<User, IdentityRole<int>>(options => 
            {
                options.Password.RequiredLength = 7;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<StajBulContext>()
                .AddDefaultTokenProviders();

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseStatusCodePages();
            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            var options = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(options.Value);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            SeedData.SeedAsync(serviceProvider.GetRequiredService<StajBulContext>(), serviceProvider, Configuration).Wait();

        }
    }
}
