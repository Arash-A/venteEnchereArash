using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace venteTest.Data.Migrations
{
    public partial class _3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContactName",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "Civilite",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateInscription",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Langue",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Nom",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Prenom",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

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
                name: "Evaluations",
                columns: table => new
                {
                    EvaluationID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ApplicationUserId = table.Column<string>(nullable: true),
                    Commentaire = table.Column<string>(maxLength: 10000, nullable: true),
                    Cote = table.Column<int>(nullable: false),
                    DateEvaluation = table.Column<DateTime>(nullable: false),
                    MembreId = table.Column<int>(nullable: false),
                    Numero = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evaluations", x => x.EvaluationID);
                    table.ForeignKey(
                        name: "FK_Evaluations_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Objets",
                columns: table => new
                {
                    ObjetID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CategorieID = table.Column<int>(nullable: false),
                    DateInscription = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(maxLength: 300, nullable: false),
                    DureeMiseVente = table.Column<int>(nullable: false),
                    Nom = table.Column<string>(nullable: false),
                    PrixDepart = table.Column<decimal>(nullable: false),
                    imageUrl = table.Column<string>(maxLength: 1024, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Objets", x => x.ObjetID);
                    table.ForeignKey(
                        name: "FK_Objets_Categories_CategorieID",
                        column: x => x.CategorieID,
                        principalTable: "Categories",
                        principalColumn: "CategorieId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Encheres",
                columns: table => new
                {
                    EnchereID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ApplicationUserId = table.Column<string>(nullable: true),
                    MembreID = table.Column<int>(nullable: false),
                    NiveauEnchere = table.Column<decimal>(nullable: false),
                    ObjetID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Encheres", x => x.EnchereID);
                    table.ForeignKey(
                        name: "FK_Encheres_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Encheres_Objets_ObjetID",
                        column: x => x.ObjetID,
                        principalTable: "Objets",
                        principalColumn: "ObjetID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Fichiers",
                columns: table => new
                {
                    FichierId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
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
                name: "IX_Encheres_ApplicationUserId",
                table: "Encheres",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Encheres_ObjetID",
                table: "Encheres",
                column: "ObjetID");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_ApplicationUserId",
                table: "Evaluations",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Fichiers_ObjetId",
                table: "Fichiers",
                column: "ObjetId");

            migrationBuilder.CreateIndex(
                name: "IX_Objets_CategorieID",
                table: "Objets",
                column: "CategorieID");
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
                name: "Categories");

            migrationBuilder.DropColumn(
                name: "Civilite",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DateInscription",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Langue",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Nom",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Prenom",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "ContactName",
                table: "AspNetUsers",
                nullable: true);
        }
    }
}
