using Microsoft.EntityFrameworkCore.Migrations;

namespace Francis.Database.Migrations
{
    public partial class RemoveDuplicateId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WatchedItems_BotUsers_BotUserId1",
                table: "WatchedItems");

            migrationBuilder.DropIndex(
                name: "IX_WatchedItems_BotUserId1",
                table: "WatchedItems");

            migrationBuilder.DropColumn(
                name: "BotUserId1",
                table: "WatchedItems");

            migrationBuilder.CreateIndex(
                name: "IX_WatchedItems_BotUserId",
                table: "WatchedItems",
                column: "BotUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_WatchedItems_BotUsers_BotUserId",
                table: "WatchedItems",
                column: "BotUserId",
                principalTable: "BotUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WatchedItems_BotUsers_BotUserId",
                table: "WatchedItems");

            migrationBuilder.DropIndex(
                name: "IX_WatchedItems_BotUserId",
                table: "WatchedItems");

            migrationBuilder.AddColumn<int>(
                name: "BotUserId1",
                table: "WatchedItems",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WatchedItems_BotUserId1",
                table: "WatchedItems",
                column: "BotUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_WatchedItems_BotUsers_BotUserId1",
                table: "WatchedItems",
                column: "BotUserId1",
                principalTable: "BotUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
