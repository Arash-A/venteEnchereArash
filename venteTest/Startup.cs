using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using venteTest.Data;
using venteTest.Models;
using venteTest.Services;
using Hangfire;
using System.Globalization;
using System.Threading;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using Rotativa.AspNetCore;

namespace venteTest
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));


            /*      services.AddIdentity<ApplicationUser, IdentityRole>()
                      .AddEntityFrameworkStores<ApplicationDbContext>()
                      .AddDefaultTokenProviders();
            */
            // Ajout SB : pour exiger confirmation par email à l'inscription.. https://docs.microsoft.com/en-us/aspnet/core/security/authentication/accconfirm?view=aspnetcore-2.1&tabs=aspnetcore2x
            services.AddIdentity<ApplicationUser, IdentityRole>(config => {
                config.SignIn.RequireConfirmedEmail = true;
            })
          .AddEntityFrameworkStores<ApplicationDbContext>()
          .AddDefaultTokenProviders();
            // Fin SB

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            //services.AddMvc();
            services.AddMvc().AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
            .AddDataAnnotationsLocalization();

            services.AddApplicationInsightsTelemetry(Configuration);
            services.AddLocalization(options => options.ResourcesPath = "Ressources");

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                new CultureInfo("en"),
                new CultureInfo("fr"),
            };

                options.DefaultRequestCulture = new RequestCulture(culture: "en", uiCulture: "en");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });
            //fin ajout carles


            // Ajout SB pour email sender
            services.AddSingleton<IEmailSender, EmailSender>();

            services.Configure<AuthMessageSenderOptions>(Configuration);
            // Fin SB

            // Ajout Arash pour les Taches automatique selon http://docs.hangfire.io
            // aussi: http://docs.hangfire.io/en/latest/configuration/using-dashboard.html#configuring-authorization
 //            services.AddHangfire(x => x.UseSqlServerStorage(Configuration.GetConnectionString("DefaultConnection")));

            //LOCALIZATION + GLOBALIZATION
            //services.AddLocalization(options => options.ResourcesPath = "Resources");
            //services.AddMvc().AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix).AddDataAnnotationsLocalization();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider) {


            //ajout carles localisation
            var localizationOption = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(localizationOption.Value);
            //Fin ajout carles


            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            RotativaConfiguration.Setup(env);

            // Ajour Arash pour Hangfire
            // app.UseHangfireServer();
            // app.UseHangfireDashboard();


        }

        //COOKIES IMPLEMENTATION. TO REVISIT
        //protected void Application_AcquireRequestState(object sender, EventArgs e)
        //{
        //    HttpCookie cultureCookie = Request.Cookies["langue"];
        //    if (cultureCookie != null)
        //    {
        //        Thread.CurrentThread.CurrentCulture = new CultureInfo(cultureCookie.Value);
        //        Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
        //    }

        //}

    }



}
