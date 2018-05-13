using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using venteTest.Data;

namespace venteTest.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
             context.Database.EnsureCreated();

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

            if (!context.Objets.Any())
            {
                context.Objets.Add(new Objet() { Nom = "Sauder Kids Storage", Description= "Sauder Kids Storage Chest for Toys, Blankets, Clothes - Oiled Oak Finish",PrixDepart= 169.99,DateInscription=DateTime.Parse("2018-05-10"),DateLimite= DateTime.Parse("2018-05-17"),Status=Status.EnVente,imageUrl= "Uploads/kidStorage.jpg",CategorieID=1,Vendeur=new Vendeur { Nom="Kouaya",Prenom="Carles Romuald",Langue="Fr",Adresse= "1727 Rue Saint-Denis Montréal" } });
                context.Objets.Add(new Objet() { Nom = "Men's Leather Watch", Description = "SEIKO SARB017 Mechanical Alpinist Automatic Men's Leather Watch - Made In Japan", PrixDepart = 479.00, DateInscription = DateTime.Parse("2018-05-10"), DateLimite = DateTime.Parse("2018-05-17"), Status = Status.EnVente, imageUrl = "Uploads/LatherWatch.jpg", CategorieID = 2, Vendeur = new Vendeur { Nom = "Kouaya", Prenom = "Carles Romuald", Langue = "Fr", Adresse = "1727 Rue Saint-Denis Montréal" } });
                context.Objets.Add(new Objet() { Nom = "Professional Headlight Hands Free", Description = "Energizer Atex Hard Case Professional Headlight Hands Free Head Torch", PrixDepart = 29.95, DateInscription = DateTime.Parse("2018-05-10"), DateLimite = DateTime.Parse("2018-05-17"), Status = Status.EnVente, imageUrl = "Uploads/HeadLight.jpg", CategorieID = 4, Vendeur = new Vendeur { Nom = "Kouaya", Prenom = "Carles Romuald", Langue = "Fr", Adresse = "1727 Rue Saint-Denis Montréal" } });
                context.Objets.Add(new Objet() { Nom = "Galaxy Baby Groot Figure", Description = "Guardians of The Galaxy Baby Groot Figure Flowerpot Style Pen Pot Xmas Gift", PrixDepart = 9.99, DateInscription = DateTime.Parse("2018-05-10"), DateLimite = DateTime.Parse("2018-05-17"), Status = Status.EnVente, imageUrl = "Uploads/GardianGalaxy.jpg", CategorieID = 5, Vendeur = new Vendeur { Nom = "Kouaya", Prenom = "Carles Romuald", Langue = "Fr", Adresse = "1727 Rue Saint-Denis Montréal" } });
                context.Objets.Add(new Objet() { Nom = "Alcohol Breath Tester", Description = "LCD Digital Alcohol Breath Tester Breathalyzer Analyzer Detector Test AT6000", PrixDepart = 8.54, DateInscription = DateTime.Parse("2018-05-10"), DateLimite = DateTime.Parse("2018-05-17"), Status = Status.EnVente, imageUrl = "Uploads/Alcoholtest.jpg", CategorieID = 7, Vendeur = new Vendeur { Nom = "Kouaya", Prenom = "Carles Romuald", Langue = "Fr", Adresse = "1727 Rue Saint-Denis Montréal" } });
                context.Objets.Add(new Objet() { Nom = "Sport Camera phones", Description = "Zhiyun Crane-M 3-Axis Gimbal Stabilizer for Mirrorless/Sport Camera phones", PrixDepart = 235.00, DateInscription = DateTime.Parse("2018-05-10"), DateLimite = DateTime.Parse("2018-05-17"), Status = Status.EnVente, imageUrl = "Uploads/CameraPhone.jpg", CategorieID = 12, Vendeur = new Vendeur { Nom = "Kouaya", Prenom = "Carles Romuald", Langue = "Fr", Adresse = "1727 Rue Saint-Denis Montréal" } });
                context.Objets.Add(new Objet() { Nom = "Portable Play Yard", Description = "Funsport Portable Play Yard Black Arrow / playard / play pen for babies", PrixDepart = 84.95, DateInscription = DateTime.Parse("2018-05-10"), DateLimite = DateTime.Parse("2018-05-17"), Status = Status.EnVente, imageUrl = "Uploads/BabyBed.jpg", CategorieID = 9, Vendeur = new Vendeur { Nom = "Kouaya", Prenom = "Carles Romuald", Langue = "Fr", Adresse = "1727 Rue Saint-Denis Montréal" } });
                context.Objets.Add(new Objet() { Nom = "Diary Notebook", Description = "Vintage Classic Retro Leather Journal Travel Notepad Blank Book Diary Notebook", PrixDepart = 3.89, DateInscription = DateTime.Parse("2018-05-10"), DateLimite = DateTime.Parse("2018-05-17"), Status = Status.EnVente, imageUrl = "Uploads/Book.jpg", CategorieID = 10, Vendeur = new Vendeur { Nom = "Kouaya", Prenom = "Carles Romuald", Langue = "Fr", Adresse = "1727 Rue Saint-Denis Montréall" } });
                context.Objets.Add(new Objet() { Nom = "Dell Precision T5400", Description = "Dell Precision T5400 2 x Xeon Quad Core E5420 2.50Ghz 16GB RAM 500Gb HDD", PrixDepart = 199.00, DateInscription = DateTime.Parse("2018-05-10"), DateLimite = DateTime.Parse("2018-05-17"), Status = Status.EnVente, imageUrl = "Uploads/Desktop.jpg", CategorieID = 15, Vendeur = new Vendeur { Nom = "Kouaya", Prenom = "Carles Romuald", Langue = "Fr", Adresse = "1727 Rue Saint-Denis Montréal" } });
                context.Objets.Add(new Objet() { Nom = "Abrasive Brush ", Description = "OSBORN 32138  Grit Abrasive Brush great for log and wood home restore NIB", PrixDepart = 89.98, DateInscription = DateTime.Parse("2018-05-10"), DateLimite = DateTime.Parse("2018-05-17"), Status = Status.EnVente, imageUrl = "Uploads/Brush.jpg", CategorieID = 11, Vendeur = new Vendeur { Nom = "Kouaya", Prenom = "Carles Romuald", Langue = "Fr", Adresse = "1727 Rue Saint-Denis Montréal" } });
                context.Objets.Add(new Objet() { Nom = "Hiking Camping Blade", Description = "New Pocket Fold Knife Sharp Outdoor Portable Travel Hiking Camping Blade Gift HY", PrixDepart = 3.88, DateInscription = DateTime.Parse("2018-05-10"), DateLimite = DateTime.Parse("2018-05-17"), Status = Status.EnVente, imageUrl = "Uploads/Blade.jpg", CategorieID = 4, Vendeur = new Vendeur { Nom = "Kouaya", Prenom = "Carles Romuald", Langue = "Fr", Adresse = "1727 Rue Saint-Denis Montréal" } });
                context.Objets.Add(new Objet() { Nom = "Cord Craft Tool", Description = "0.8mm 78m/Roll Leather Hand Sewing Waxed Thread Hand Stitching Cord Craft Tool", PrixDepart = 1.86, DateInscription = DateTime.Parse("2018-05-10"), DateLimite = DateTime.Parse("2018-05-17"), Status = Status.EnVente, imageUrl = "Uploads/Craft.jpg", CategorieID = 16, Vendeur = new Vendeur { Nom = "Kouaya", Prenom = "Carles Romuald", Langue = "Fr", Adresse = "1727 Rue Saint-Denis Montréal" } });
                context.Objets.Add(new Objet() { Nom = "Cologne for Men", Description = "Azzaro Pour Homme Cologne for Men 100ml EDT Spray", PrixDepart = 1.86, DateInscription = DateTime.Parse("2018-05-10"), DateLimite = DateTime.Parse("2018-05-17"), Status = Status.EnVente, imageUrl = "Uploads/Parfum.jpg", CategorieID = 17, Vendeur = new Vendeur { Nom = "Kouaya", Prenom = "Carles Romuald", Langue = "Fr", Adresse = "1727 Rue Saint-Denis Montréal" } });
                context.SaveChanges();

            }

        }
    }
}
