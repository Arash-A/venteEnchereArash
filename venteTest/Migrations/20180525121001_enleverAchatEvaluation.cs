using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace venteTest.Migrations
{
    public partial class enleverAchatEvaluation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Evaluations_Objets_ObjetID",
                table: "Evaluations");

            migrationBuilder.DropIndex(
                name: "IX_Evaluations_ObjetID",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "ObjetID",
                table: "Evaluations");

            migrationBuilder.AddColumn<int>(
                name: "AchatEvaluationEvaluationID",
                table: "Objets",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Objets_AchatEvaluationEvaluationID",
                table: "Objets",
                column: "AchatEvaluationEvaluationID",
                unique: true,
                filter: "[AchatEvaluationEvaluationID] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Objets_Evaluations_AchatEvaluationEvaluationID",
                table: "Objets",
                column: "AchatEvaluationEvaluationID",
                principalTable: "Evaluations",
                principalColumn: "EvaluationID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Objets_Evaluations_AchatEvaluationEvaluationID",
                table: "Objets");

            migrationBuilder.DropIndex(
                name: "IX_Objets_AchatEvaluationEvaluationID",
                table: "Objets");

            migrationBuilder.DropColumn(
                name: "AchatEvaluationEvaluationID",
                table: "Objets");

            migrationBuilder.AddColumn<int>(
                name: "ObjetID",
                table: "Evaluations",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_ObjetID",
                table: "Evaluations",
                column: "ObjetID",
                unique: true,
                filter: "[ObjetID] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluations_Objets_ObjetID",
                table: "Evaluations",
                column: "ObjetID",
                principalTable: "Objets",
                principalColumn: "ObjetID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
