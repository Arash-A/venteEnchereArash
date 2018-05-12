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
            services.AddDbContext<LibraryContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));


            /*      services.AddIdentity<ApplicationUser, IdentityRole>()
                      .AddEntityFrameworkStores<ApplicationDbContext>()
                      .AddDefaultTokenProviders();
            */
            // Ajout SB : pour exiger confirmation par email à l'inscription.. https://docs.microsoft.com/en-us/aspnet/core/security/authentication/accconfirm?view=aspnetcore-2.1&tabs=aspnetcore2x
            services.AddIdentity<ApplicationUser, IdentityRole>(config => {
                config.SignIn.RequireConfirmedEmail = true;
            })
          .AddEntityFrameworkStores<LibraryContext>()
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

            // Ajour Arash pour Hangfire
          //  app.UseHangfireServer();
            //app.UseHangfireDashboard();


        }
        //Ajout SB pour créer admin et rôles (Si requis)
        // Méthode adapté par SB selon: https://stackoverflow.com/questions/42471866/how-to-create-roles-in-asp-net-core-and-assign-them-to-users
        // Méthode pour créer les rôles dans la BD et définir un administrateur par défaut 
        private async Task CreateRolesAdminUsers(IServiceProvider serviceProvider) {
            //initializing custom roles 
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            string[] roleNames = { "Admin", "Manager", "Member" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames) {
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist) {
                    //create the roles and seed them to the database: Question 1
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            //Creer Admin
            var powerUser = new ApplicationUser {
                UserName = Configuration["AppSettings:AdminUserEmail"], // Pour créer membre (avec CreateAsync), on doit mettre le email comme userName par convention
                Email = Configuration["AppSettings:AdminUserEmail"],
                EmailConfirmed = true, // on fait EmailConfirmed pour admin seulement
                //propriétés supplémentaires ajoutés:
                Nom = Configuration["AppSettings:AdminLastName"],
                Prenom = Configuration["AppSettings:AdminFirstName"],
                Civilite = new Civilite { Abbreviation = CiviliteAbbreviation.M.ToString(), Name = CiviliteName.Monsieur.ToString() }.Abbreviation,
                Langue = new Language { Abbreviation = LanguageAbbreviation.fr.ToString(), Name = LanguageName.FR.ToString() }.Abbreviation,
                DateInscription = DateTime.Now
            };
            //Ensure you have these values in your appsettings.json file
            string userPWD = Configuration["AppSettings:AdminUserPassword"];
            var _user = await UserManager.FindByEmailAsync(Configuration["AppSettings:AdminUserEmail"]); //recherche par email
            if (_user == null)
                _user = await UserManager.FindByNameAsync(Configuration["AppSettings:AdminUserEmail"]); //recherche par nom d'utilisateur

            if (_user == null) {
                var createPowerUser = await UserManager.CreateAsync(powerUser, userPWD);
                if (createPowerUser.Succeeded) {
                    //here we tie the new user to the role
                    await UserManager.AddToRoleAsync(powerUser, "Admin");
                }
            }

            //Créer un membre
            var normalUser = new ApplicationUser {
                UserName = "isabelle.blais16@gmail.com", // Pour créer membre (avec CreateAsync), on doit mettre le email comme userName par convention
                Email = "isabelle.blais16@gmail.com",
                EmailConfirmed = true, // on fait EmailConfirmed pour admin seulement
                //propriétés supplémentaires ajoutés:
                Nom = "Blain",
                Prenom = "Isabelle",
                Civilite = new Civilite { Abbreviation = CiviliteAbbreviation.Mme.ToString(), Name = CiviliteName.Madame.ToString() }.Abbreviation, 
                Langue = new Language { Abbreviation = LanguageAbbreviation.fr.ToString(), Name = LanguageName.FR.ToString() }.Abbreviation,
                DateInscription = DateTime.Now
            };
            //Ensure you have these values in your appsettings.json file
            string userPWDn = Configuration["AppSettings:AdminUserPassword"]; //meme pw que l'Admin
            var _userN = await UserManager.FindByEmailAsync("isabelle.blais16@gmail.com"); //recherche par email
            if (_userN == null)
                _userN = await UserManager.FindByNameAsync("isabelle.blais16@gmail.com"); //recherche par nom d'utilisateur

            if (_userN == null) {
                var createNormalUser = await UserManager.CreateAsync(normalUser, userPWDn);
                if (createNormalUser.Succeeded) {
                    //here we tie the new user to the role
                    await UserManager.AddToRoleAsync(normalUser, "Member");
                }
            }

        }
        // FIN AJOUT SB
    }
}
