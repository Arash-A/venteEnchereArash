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

            services.AddMvc();

            // Ajout SB pour email sender
            services.AddSingleton<IEmailSender, EmailSender>();

            services.Configure<AuthMessageSenderOptions>(Configuration);
            // Fin SB

            // Ajout Arash pour les Taches automatique selon http://docs.hangfire.io
            // aussi: http://docs.hangfire.io/en/latest/configuration/using-dashboard.html#configuring-authorization
            //services.AddHangfire(x => x.UseSqlServerStorage(Configuration.GetConnectionString("DefaultConnection")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider) {
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

            //ajout sb pour créer admin et rôles (Si requis)

         //CreateRolesAdminUsers(serviceProvider).Wait();

            // Ajout SB pour faire nos mappings entre Model et ViewModels
            AutoMapperConfig.RegisterMappings();


            // Ajour Arash pour Hangfire
           // app.UseHangfireServer();
           // app.UseHangfireDashboard();


        }

    }

    public class AutoMapperConfig {
        // 
        public static void RegisterMappings() {

            AutoMapper.Mapper.Initialize(cfg => {            
                cfg.CreateMap<Categorie, Models.AdminViewModels.CategorieViewModel>();
                cfg.CreateMap<Models.AdminViewModels.CategorieViewModel, Categorie>();

                cfg.CreateMap<ApplicationUser, Vendeur>();
                cfg.CreateMap<Vendeur, ApplicationUser>();

                cfg.CreateMap<ApplicationUser, Miseur>();
                cfg.CreateMap<Miseur, ApplicationUser>();

            });                                                                                                        

            // Exemples utilisés dans le contrôleur :
            //
            //Ex1 pour mapper:  
            // Article article = _articleManager.lstArticles.FirstOrDefault(p => p.Titre.Equals(titre));
            // ArticleViewModel model = Mapper.Map<Article, ArticleViewModel>(article); // conversion d'une entité Article en ArticleViewModel
            //return View(model)

            //Ex2 pour mapper:
            // IList<Article> lArt = _articleManager.lstArticles;
            // IList<ArticleViewModel> model = Mapper.Map<IList<Article>, IList<ArticleViewModel>>(lArt);
            // return PartialView(model);

        }

    }

}
