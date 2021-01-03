using EmailService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;
using StajBul.Data.Abstract;
using StajBul.Data.Concrete.EfCore;
using StajBul.Entity;
using StajBul.Service;
using StajBul.Service.Impl;
using StajBul.WebUI.Infrastructure;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;

namespace StajBul.WebUI.Util
{
    public class AddDependency
    {
        public static void addAllDependencies(IServiceCollection services)
        {
            services.AddScoped<IEmailSender, EmailSender>();

            services.AddTransient<IAddressRepo, EfAddressRepoImpl>();
            services.AddTransient<IAnnouncementRepo, EfAnnouncementRepoImpl>();
            services.AddTransient<ICategoryRepo, EfCategoryRepoImpl>();
            services.AddTransient<ICityRepo, EfCityRepoImpl>();
            services.AddTransient<IUserRepo, EfUserRepoImpl>();

            services.AddTransient<IAddressService, AddressServiceImpl>();
            services.AddTransient<IAnnouncementService, AnnouncementServiceImpl>();
            services.AddTransient<ICategoryService, CategoryServiceImpl>();
            services.AddTransient<ICityService, CityServiceImpl>();
            services.AddTransient<IUserService, UserServiceImpl>();

            services.AddTransient<IPasswordValidator<User>, CustomPasswordValidator>();
            services.AddTransient<IUserValidator<User>, CustomUserValidator>();
        }

        public static void addLocalization(IServiceCollection services)
        {
            services.AddLocalization(opts => { opts.ResourcesPath = "Resources"; });

            services.AddMvc()
                .AddViewLocalization(
                    LanguageViewLocationExpanderFormat.Suffix,
                    opts => { opts.ResourcesPath = "Resources"; })
                .AddDataAnnotationsLocalization();

            services.Configure<RequestLocalizationOptions>(
               opts =>
               {
                   var supportedCultures = new List<CultureInfo>
              {

                new CultureInfo("tr"),
                new CultureInfo("en")

              };

                   opts.DefaultRequestCulture = new RequestCulture("tr");
                   opts.SupportedCultures = supportedCultures;
                   opts.SupportedUICultures = supportedCultures;
               });
        }
    }
}
