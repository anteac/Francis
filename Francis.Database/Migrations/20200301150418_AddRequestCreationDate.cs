using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Francis.Database.Migrations
{
    public partial class AddRequestCreationDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "Progressions",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            if (migrationBuilder.IsSqlite())
            {
                return;
            }

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Progressions");
        }
    }
}
