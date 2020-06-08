using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Francis.Database.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BotUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserName = table.Column<string>(nullable: true),
                    PlexId = table.Column<string>(nullable: true),
                    PlexToken = table.Column<string>(nullable: true),
                    OmbiId = table.Column<string>(nullable: true),
                    TelegramId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BotUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Options",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Options", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Progressions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BotUserId = table.Column<long>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    BotUserId1 = table.Column<int>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    Search = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: true),
                    Status = table.Column<int>(nullable: true),
                    ExcludedIds = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Progressions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Progressions_BotUsers_BotUserId1",
                        column: x => x.BotUserId1,
                        principalTable: "BotUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WatchedItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BotUserId = table.Column<long>(nullable: false),
                    ItemType = table.Column<int>(nullable: false),
                    RequestId = table.Column<long>(nullable: false),
                    BotUserId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WatchedItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WatchedItems_BotUsers_BotUserId1",
                        column: x => x.BotUserId1,
                        principalTable: "BotUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Progressions_BotUserId1",
                table: "Progressions",
                column: "BotUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_WatchedItems_BotUserId1",
                table: "WatchedItems",
                column: "BotUserId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Options");

            migrationBuilder.DropTable(
                name: "Progressions");

            migrationBuilder.DropTable(
                name: "WatchedItems");

            migrationBuilder.DropTable(
                name: "BotUsers");
        }
    }
}
