using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using venteTest.Models;

namespace venteTest.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
        public DbSet<Enchere> Encheres { get; set;}
        public DbSet<Objet> Objets { get; set;}
        public DbSet<Categorie> Categories { get; set;}
        public DbSet<Evaluation> Evaluations { get; set;}
        public DbSet<Fichier> Fichiers { get; set;}
        public DbSet<ConfigurationAdmin> ConfigurationAdmins { get; set; }
        public DbSet<venteTest.Models.AchatEvaluation> AchatEvaluation { get; set; }


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=eAuctionDB;Trusted_Connection=True;");
        //}
    }
}
