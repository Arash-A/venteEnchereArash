using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using venteTest.Data;
using venteTest.Models;

namespace venteTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Ajout SB pour faire nos mappings entre Model et ViewModels
            AutoMapperConfig.RegisterMappings();


            var host = BuildWebHost(args);

            using (var scope = host.Services.CreateScope())
            {
                var Configuration = host.Services.GetRequiredService<IConfiguration>();

                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<ApplicationDbContext>();
                try
                {
                    SeedData.Initialize(services, Configuration);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred seeding the DB.");
                }
            }

            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
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

                cfg.CreateMap<ApplicationUser, Models.AdminViewModels.MemberViewModel>();

                cfg.CreateMap<Objet, Models.ObjetViewModel.AnnoncerObjectViewModel>();
                cfg.CreateMap<Models.ObjetViewModel.AnnoncerObjectViewModel, Objet>();

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
