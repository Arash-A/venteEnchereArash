using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace venteTest.Migrations
{
    public partial class newDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategorieId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    Nom = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategorieId);
                });

            migrationBuilder.CreateTable(
                name: "ConfigurationAdmin",
                columns: table => new
                {
                    ConfigurationAdminId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PasGlobalEnchere = table.Column<decimal>(nullable: false),
                    TauxGlobalComissionAuVendeur = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigurationAdmin", x => x.ConfigurationAdminId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    Adresse = table.Column<string>(nullable: true),
                    Civilite = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    DateInscription = table.Column<DateTime>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    Langue = table.Column<string>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    Nom = table.Column<string>(nullable: true),
                    NormalizedEmail = table.Column<string>(nullable: true),
                    NormalizedUserName = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    Prenom = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    UserName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Objets",
                columns: table => new
                {
                    ObjetID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AcheteurId = table.Column<string>(nullable: true),
                    CategorieID = table.Column<int>(nullable: false),
                    ConfigurationAdminId = table.Column<int>(nullable: true),
                    DateInscription = table.Column<DateTime>(nullable: false),
                    DateLimite = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(maxLength: 400, nullable: false),
                    Nom = table.Column<string>(nullable: false),
                    PrixDepart = table.Column<double>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    VendeurId = table.Column<string>(nullable: false),
                    imageUrl = table.Column<string>(maxLength: 1024, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Objets", x => x.ObjetID);
                    table.ForeignKey(
                        name: "FK_Objets_Users_AcheteurId",
                        column: x => x.AcheteurId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Objets_Categories_CategorieID",
                        column: x => x.CategorieID,
                        principalTable: "Categories",
                        principalColumn: "CategorieId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Objets_ConfigurationAdmin_ConfigurationAdminId",
                        column: x => x.ConfigurationAdminId,
                        principalTable: "ConfigurationAdmin",
                        principalColumn: "ConfigurationAdminId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Objets_Users_VendeurId",
                        column: x => x.VendeurId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Encheres",
                columns: table => new
                {
                    EnchereId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MiseurId = table.Column<string>(nullable: true),
                    Niveau = table.Column<double>(nullable: false),
                    ObjetId = table.Column<int>(nullable: false),
                    VendeurId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Encheres", x => x.EnchereId);
                    table.ForeignKey(
                        name: "FK_Encheres_Users_MiseurId",
                        column: x => x.MiseurId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Encheres_Objets_ObjetId",
                        column: x => x.ObjetId,
                        principalTable: "Objets",
                        principalColumn: "ObjetID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Encheres_Users_VendeurId",
                        column: x => x.VendeurId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Evaluations",
                columns: table => new
                {
                    VendeurId = table.Column<string>(nullable: true),
                    EvaluationID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Commentaire = table.Column<string>(maxLength: 10000, nullable: true),
                    Cote = table.Column<int>(nullable: false),
                    DateEvaluation = table.Column<DateTime>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    Numero = table.Column<string>(nullable: true),
                    ObjetId = table.Column<int>(nullable: false),
                    AcheteurId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evaluations", x => x.EvaluationID);
                    table.ForeignKey(
                        name: "FK_Evaluations_Objets_ObjetId",
                        column: x => x.ObjetId,
                        principalTable: "Objets",
                        principalColumn: "ObjetID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Evaluations_Users_VendeurId",
                        column: x => x.VendeurId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Evaluations_Users_AcheteurId",
                        column: x => x.AcheteurId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Fichiers",
                columns: table => new
                {
                    FichierId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NomLocale = table.Column<string>(nullable: true),
                    NomOriginal = table.Column<string>(nullable: false),
                    ObjetId = table.Column<int>(nullable: false),
                    Remarques = table.Column<string>(nullable: true),
                    verseLe = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fichiers", x => x.FichierId);
                    table.ForeignKey(
                        name: "FK_Fichiers_Objets_ObjetId",
                        column: x => x.ObjetId,
                        principalTable: "Objets",
                        principalColumn: "ObjetID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Encheres_MiseurId",
                table: "Encheres",
                column: "MiseurId");

            migrationBuilder.CreateIndex(
                name: "IX_Encheres_ObjetId",
                table: "Encheres",
                column: "ObjetId");

            migrationBuilder.CreateIndex(
                name: "IX_Encheres_VendeurId",
                table: "Encheres",
                column: "VendeurId");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_ObjetId",
                table: "Evaluations",
                column: "ObjetId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_VendeurId",
                table: "Evaluations",
                column: "VendeurId");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_AcheteurId",
                table: "Evaluations",
                column: "AcheteurId");

            migrationBuilder.CreateIndex(
                name: "IX_Fichiers_ObjetId",
                table: "Fichiers",
                column: "ObjetId");

            migrationBuilder.CreateIndex(
                name: "IX_Objets_AcheteurId",
                table: "Objets",
                column: "AcheteurId");

            migrationBuilder.CreateIndex(
                name: "IX_Objets_CategorieID",
                table: "Objets",
                column: "CategorieID");

            migrationBuilder.CreateIndex(
                name: "IX_Objets_ConfigurationAdminId",
                table: "Objets",
                column: "ConfigurationAdminId");

            migrationBuilder.CreateIndex(
                name: "IX_Objets_VendeurId",
                table: "Objets",
                column: "VendeurId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Encheres");

            migrationBuilder.DropTable(
                name: "Evaluations");

            migrationBuilder.DropTable(
                name: "Fichiers");

            migrationBuilder.DropTable(
                name: "Objets");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "ConfigurationAdmin");
        }
    }
}
