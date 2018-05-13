using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using venteTest.Data;
using venteTest.Services;

namespace venteTest.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider, IConfiguration Configuration)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
             context.Database.EnsureCreated();

            //Ajout SB
            CreateRolesAdminUsers(serviceProvider, Configuration).Wait();

            if (!context.Categories.Any())
            {
                context.Categories.Add(new Categorie() { Nom = "Home & Garden",Description="Products For Home and Garden" });
                context.Categories.Add(new Categorie() { Nom = "Jewellery & Watches", Description = "Jewellery & Watches" });
                context.Categories.Add(new Categorie() { Nom = "Motors", Description = "Motors" });
                context.Categories.Add(new Categorie() { Nom = "Sporting Goods", Description = "Sporting Goods" });
                context.Categories.Add(new Categorie() { Nom = "Toys & Hobbies", Description = "Toys & Hobbies" });
                context.Categories.Add(new Categorie() { Nom = "Fashion", Description = "Fashion" });
                context.Categories.Add(new Categorie() { Nom = "Electronics", Description = "Electronics" });
                context.Categories.Add(new Categorie() { Nom = "Convertible&Art", Description = "Convertible&Art" });
                context.Categories.Add(new Categorie() { Nom = "Baby", Description = "Baby" });
                context.Categories.Add(new Categorie() { Nom = "Book", Description = "Book" });
                context.Categories.Add(new Categorie() { Nom = "Business & Industrial", Description = "Business & Industrial" });
                context.Categories.Add(new Categorie() { Nom = "Camera & Photo", Description = "Camera & Photo" });
                context.Categories.Add(new Categorie() { Nom = "Cell Phones & Accessories", Description = "Cell Phones & Accessories" });
                context.Categories.Add(new Categorie() { Nom = "Cell Clothing,Shoes & Accessories", Description = "Clothing,Shoes & Accessories" });
                context.Categories.Add(new Categorie() { Nom = "Computers,Tablets & Accessories", Description = "Computers,Tablets & Accessories" });
                context.Categories.Add(new Categorie() { Nom = "Craft", Description = "Craft" });
                context.Categories.Add(new Categorie() { Nom = "Heath & Beauty", Description = "Heath & Beauty" });
                context.Categories.Add(new Categorie() { Nom = "Musical Instruments & Gears", Description = "Musical Instruments & Gears" });
                context.Categories.Add(new Categorie() { Nom = "Gift Cards & Coupons", Description = "Gift Cards & Coupons" });
                context.Categories.Add(new Categorie() { Nom = "Others Category", Description = "Others Category" });
                context.SaveChanges();

            }

            // Définir configuration admin ID
            if (!context.ConfigurationAdmins.Any()) {
                context.ConfigurationAdmins.Add(new ConfigurationAdmin() { TauxGlobalComissionAuVendeur = 5/100m, PasGlobalEnchere = 1});
                context.SaveChanges();
            }


            var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            if (!context.Objets.Any())
            {
                // trouve un utilisateur pour le mettre vendeur
                ApplicationUser user = UserManager.Users.FirstOrDefault(p => p.UserName.Equals("isabelle.blais16@gmail.com")); //changer "isabelle.blais16@gmail.com" avec user.Email avec httpcontext.
                Vendeur vendeur = Mapper.Map<ApplicationUser, Vendeur>(user); // conversion d'une entité User en Vendeur
                ConfigurationAdmin configurationAdmin = context.ConfigurationAdmins.FirstOrDefault(p => p.ConfigurationAdminId.Equals(1));


                context.Objets.Add(new Objet() { Nom = "Sauder Kids Storage", Description= "Sauder Kids Storage Chest for Toys, Blankets, Clothes - Oiled Oak Finish",PrixDepart= 169.99,DateInscription=DateTime.Parse("2018-05-10"),DateLimite= DateTime.Parse("2018-05-17"),Status=Status.EnVente,imageUrl= "Uploads/kidStorage.jpg",CategorieID=1,Vendeur= vendeur, ConfigurationAdmin = configurationAdmin });
                context.Objets.Add(new Objet() { Nom = "Men's Leather Watch", Description = "SEIKO SARB017 Mechanical Alpinist Automatic Men's Leather Watch - Made In Japan", PrixDepart = 479.00, DateInscription = DateTime.Parse("2018-05-10"), DateLimite = DateTime.Parse("2018-05-17"), Status = Status.EnVente, imageUrl = "Uploads/LatherWatch.jpg", CategorieID = 2, Vendeur= vendeur, ConfigurationAdmin = configurationAdmin });
                context.Objets.Add(new Objet() { Nom = "Professional Headlight Hands Free", Description = "Energizer Atex Hard Case Professional Headlight Hands Free Head Torch", PrixDepart = 29.95, DateInscription = DateTime.Parse("2018-05-10"), DateLimite = DateTime.Parse("2018-05-17"), Status = Status.EnVente, imageUrl = "Uploads/HeadLight.jpg", CategorieID = 4, Vendeur= vendeur, ConfigurationAdmin = configurationAdmin });
                context.Objets.Add(new Objet() { Nom = "Galaxy Baby Groot Figure", Description = "Guardians of The Galaxy Baby Groot Figure Flowerpot Style Pen Pot Xmas Gift", PrixDepart = 9.99, DateInscription = DateTime.Parse("2018-05-10"), DateLimite = DateTime.Parse("2018-05-17"), Status = Status.EnVente, imageUrl = "Uploads/GardianGalaxy.jpg", CategorieID = 5, Vendeur= vendeur, ConfigurationAdmin = configurationAdmin });
                context.Objets.Add(new Objet() { Nom = "Alcohol Breath Tester", Description = "LCD Digital Alcohol Breath Tester Breathalyzer Analyzer Detector Test AT6000", PrixDepart = 8.54, DateInscription = DateTime.Parse("2018-05-10"), DateLimite = DateTime.Parse("2018-05-17"), Status = Status.EnVente, imageUrl = "Uploads/Alcoholtest.jpg", CategorieID = 7, Vendeur= vendeur, ConfigurationAdmin = configurationAdmin });
                context.Objets.Add(new Objet() { Nom = "Sport Camera phones", Description = "Zhiyun Crane-M 3-Axis Gimbal Stabilizer for Mirrorless/Sport Camera phones", PrixDepart = 235.00, DateInscription = DateTime.Parse("2018-05-10"), DateLimite = DateTime.Parse("2018-05-17"), Status = Status.EnVente, imageUrl = "Uploads/CameraPhone.jpg", CategorieID = 12, Vendeur= vendeur, ConfigurationAdmin = configurationAdmin });
                context.Objets.Add(new Objet() { Nom = "Portable Play Yard", Description = "Funsport Portable Play Yard Black Arrow / playard / play pen for babies", PrixDepart = 84.95, DateInscription = DateTime.Parse("2018-05-10"), DateLimite = DateTime.Parse("2018-05-17"), Status = Status.EnVente, imageUrl = "Uploads/BabyBed.jpg", CategorieID = 9, Vendeur= vendeur, ConfigurationAdmin = configurationAdmin });
                context.Objets.Add(new Objet() { Nom = "Diary Notebook", Description = "Vintage Classic Retro Leather Journal Travel Notepad Blank Book Diary Notebook", PrixDepart = 3.89, DateInscription = DateTime.Parse("2018-05-10"), DateLimite = DateTime.Parse("2018-05-17"), Status = Status.EnVente, imageUrl = "Uploads/Book.jpg", CategorieID = 10, Vendeur = vendeur, ConfigurationAdmin = configurationAdmin });
                context.Objets.Add(new Objet() { Nom = "Dell Precision T5400", Description = "Dell Precision T5400 2 x Xeon Quad Core E5420 2.50Ghz 16GB RAM 500Gb HDD", PrixDepart = 199.00, DateInscription = DateTime.Parse("2018-05-10"), DateLimite = DateTime.Parse("2018-05-17"), Status = Status.EnVente, imageUrl = "Uploads/Desktop.jpg", CategorieID = 15, Vendeur= vendeur, ConfigurationAdmin = configurationAdmin });
                context.Objets.Add(new Objet() { Nom = "Abrasive Brush", Description = "OSBORN 32138  Grit Abrasive Brush great for log and wood home restore NIB", PrixDepart = 89.98, DateInscription = DateTime.Parse("2018-05-10"), DateLimite = DateTime.Parse("2018-05-17"), Status = Status.EnVente, imageUrl = "Uploads/Brush.jpg", CategorieID = 11, Vendeur= vendeur, ConfigurationAdmin = configurationAdmin });
                context.Objets.Add(new Objet() { Nom = "Hiking Camping Blade", Description = "New Pocket Fold Knife Sharp Outdoor Portable Travel Hiking Camping Blade Gift HY", PrixDepart = 3.88, DateInscription = DateTime.Parse("2018-05-10"), DateLimite = DateTime.Parse("2018-05-17"), Status = Status.EnVente, imageUrl = "Uploads/Blade.jpg", CategorieID = 4, Vendeur= vendeur, ConfigurationAdmin = configurationAdmin });
                context.Objets.Add(new Objet() { Nom = "Cord Craft Tool", Description = "0.8mm 78m/Roll Leather Hand Sewing Waxed Thread Hand Stitching Cord Craft Tool", PrixDepart = 1.86, DateInscription = DateTime.Parse("2018-05-10"), DateLimite = DateTime.Parse("2018-05-17"), Status = Status.EnVente, imageUrl = "Uploads/Craft.jpg", CategorieID = 16, Vendeur= vendeur, ConfigurationAdmin = configurationAdmin });
                context.Objets.Add(new Objet() { Nom = "Cologne for Men", Description = "Azzaro Pour Homme Cologne for Men 100ml EDT Spray", PrixDepart = 1.86, DateInscription = DateTime.Parse("2018-05-10"), DateLimite = DateTime.Parse("2018-05-17"), Status = Status.EnVente, imageUrl = "Uploads/Parfum.jpg", CategorieID = 17, Vendeur= vendeur, ConfigurationAdmin = configurationAdmin });
                context.SaveChanges();

            }

            // Par défaut, un vendeur place une première mise sur son objet correspondant au prix de départ (discutable... fait pour tester)
            if (!context.Encheres.Any()) 
            {
                Miseur miseur = (Miseur)UserManager.Users.FirstOrDefault(p => p.UserName.Equals("isabelle.blais16@gmail.com"));

                Objet objet;
                objet = context.Objets.FirstOrDefault(p => p.ObjetID == 1);
                context.Encheres.Add(new Enchere() { Objet = objet, Niveau = objet.PrixDepart, Miseur = miseur});

                objet = context.Objets.FirstOrDefault(p => p.ObjetID == 2);
                context.Encheres.Add(new Enchere() { Objet = objet, Niveau = objet.PrixDepart, Miseur = miseur });

                objet = context.Objets.FirstOrDefault(p => p.ObjetID == 3);
                context.Encheres.Add(new Enchere() { Objet = objet, Niveau = objet.PrixDepart, Miseur = miseur });

                objet = context.Objets.FirstOrDefault(p => p.ObjetID == 4);
                context.Encheres.Add(new Enchere() { Objet = objet, Niveau = objet.PrixDepart, Miseur = miseur });

                objet = context.Objets.FirstOrDefault(p => p.ObjetID == 5);
                context.Encheres.Add(new Enchere() { Objet = objet, Niveau = objet.PrixDepart, Miseur = miseur });

                objet = context.Objets.FirstOrDefault(p => p.ObjetID == 6);
                context.Encheres.Add(new Enchere() { Objet = objet, Niveau = objet.PrixDepart, Miseur = miseur });

                objet = context.Objets.FirstOrDefault(p => p.ObjetID == 7);
                context.Encheres.Add(new Enchere() { Objet = objet, Niveau = objet.PrixDepart, Miseur = miseur });

                objet = context.Objets.FirstOrDefault(p => p.ObjetID == 8);
                context.Encheres.Add(new Enchere() { Objet = objet, Niveau = objet.PrixDepart, Miseur = miseur });

                objet = context.Objets.FirstOrDefault(p => p.ObjetID == 9);
                context.Encheres.Add(new Enchere() { Objet = objet, Niveau = objet.PrixDepart, Miseur = miseur });

                objet = context.Objets.FirstOrDefault(p => p.ObjetID == 10);
                context.Encheres.Add(new Enchere() { Objet = objet, Niveau = objet.PrixDepart, Miseur = miseur });

                objet = context.Objets.FirstOrDefault(p => p.ObjetID == 11);
                context.Encheres.Add(new Enchere() { Objet = objet, Niveau = objet.PrixDepart, Miseur = miseur });

                objet = context.Objets.FirstOrDefault(p => p.ObjetID == 12);
                context.Encheres.Add(new Enchere() { Objet = objet, Niveau = objet.PrixDepart, Miseur = miseur });

                objet = context.Objets.FirstOrDefault(p => p.ObjetID == 13);
                context.Encheres.Add(new Enchere() { Objet = objet, Niveau = objet.PrixDepart, Miseur = miseur });

                context.SaveChanges();
            }

        }

        //Ajout SB pour créer admin et rôles (Si requis)
        // Méthode adapté par SB selon: https://stackoverflow.com/questions/42471866/how-to-create-roles-in-asp-net-core-and-assign-them-to-users
        // Méthode pour créer les rôles dans la BD et définir un administrateur par défaut 
        private static async Task CreateRolesAdminUsers(IServiceProvider serviceProvider, IConfiguration Configuration) {
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
                EmailConfirmed = true, // on fait EmailConfirmed pour permettre le login immédiatement
                //propriétés supplémentaires ajoutés:
                Nom = Configuration["AppSettings:AdminLastName"],
                Prenom = Configuration["AppSettings:AdminFirstName"],
                Civilite = new Civilite { Abbreviation = CiviliteAbbreviation.M.ToString(), Name = CiviliteName.Monsieur.ToString() }.Abbreviation,
                Langue = new Language { Abbreviation = LanguageAbbreviation.fr.ToString(), Name = LanguageName.FR.ToString() }.Abbreviation,
                DateInscription = DateTime.Now,
                Adresse = "1204 rue Benoit, Chambly, QC J3L 5K8"
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

            //Créer un membre...VENDEUR par défaut!
            var user = new Vendeur {
                UserName = "isabelle.blais16@gmail.com", // Pour créer membre (avec CreateAsync), on doit mettre le email comme userName par convention
                Email = "isabelle.blais16@gmail.com",
                EmailConfirmed = true, // on fait EmailConfirmed pour permettre le login immédiatement
                //propriétés supplémentaires ajoutés:
                Nom = "Blain",
                Prenom = "Isabelle",
                Civilite = new Civilite { Abbreviation = CiviliteAbbreviation.Mme.ToString(), Name = CiviliteName.Madame.ToString() }.Abbreviation,
                Langue = new Language { Abbreviation = LanguageAbbreviation.fr.ToString(), Name = LanguageName.FR.ToString() }.Abbreviation,
                DateInscription = DateTime.Now,
                Adresse = "1204 rue Benoit, Chambly, QC J3L 5K8"
            };
            //Ensure you have these values in your appsettings.json file
            string userPWDn = Configuration["AppSettings:AdminUserPassword"]; //meme pw que l'Admin
            var _userN = await UserManager.FindByEmailAsync("isabelle.blais16@gmail.com"); //recherche par email
            if (_userN == null)
                _userN = await UserManager.FindByNameAsync("isabelle.blais16@gmail.com"); //recherche par nom d'utilisateur

            if (_userN == null) {
                var createNormalUser = await UserManager.CreateAsync(user, userPWDn);
                if (createNormalUser.Succeeded) {
                    //here we tie the new user to the role
                    await UserManager.AddToRoleAsync(user, "Member");
                }
            }

            //Créer un 2e membre:
            // etc.
        }
        // FIN AJOUT SB


    }
}
