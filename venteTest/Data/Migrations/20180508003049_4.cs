using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace venteTest.Data.Migrations
{
    public partial class _4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Prenom",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Nom",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Langue",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateInscription",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<string>(
                name: "Civilite",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Prenom",
                table: "AspNetUsers",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nom",
                table: "AspNetUsers",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Langue",
                table: "AspNetUsers",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateInscription",
                table: "AspNetUsers",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Civilite",
                table: "AspNetUsers",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
