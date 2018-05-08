using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace venteTest.Data.Migrations
{
    public partial class _5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "imageUrl",
                table: "Objets",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1024);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "imageUrl",
                table: "Objets",
                maxLength: 1024,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 1024,
                oldNullable: true);
        }
    }
}
