using Microsoft.EntityFrameworkCore.Migrations;

namespace Francis.Database.Migrations
{
    public partial class ConsolidateForeignKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            if (migrationBuilder.IsSqlite())
            {
                return;
            }

            migrationBuilder.DropForeignKey(
                name: "FK_Progressions_BotUsers_BotUserId",
                table: "Progressions");

            migrationBuilder.DropForeignKey(
                name: "FK_WatchedItems_BotUsers_BotUserId",
                table: "WatchedItems");

            migrationBuilder.DropColumn(
                name: "ChatId",
                table: "WatchedItems");

            migrationBuilder.DropColumn(
                name: "ChatId",
                table: "Progressions");

            migrationBuilder.AlterColumn<long>(
                name: "BotUserId",
                table: "WatchedItems",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "BotUserId",
                table: "Progressions",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Progressions_BotUsers_BotUserId",
                table: "Progressions",
                column: "BotUserId",
                principalTable: "BotUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
            if (migrationBuilder.IsSqlite())
            {
                return;
            }

            migrationBuilder.DropForeignKey(
                name: "FK_Progressions_BotUsers_BotUserId",
                table: "Progressions");

            migrationBuilder.DropForeignKey(
                name: "FK_WatchedItems_BotUsers_BotUserId",
                table: "WatchedItems");

            migrationBuilder.AlterColumn<long>(
                name: "BotUserId",
                table: "WatchedItems",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddColumn<long>(
                name: "ChatId",
                table: "WatchedItems",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<long>(
                name: "BotUserId",
                table: "Progressions",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddColumn<long>(
                name: "ChatId",
                table: "Progressions",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddForeignKey(
                name: "FK_Progressions_BotUsers_BotUserId",
                table: "Progressions",
                column: "BotUserId",
                principalTable: "BotUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WatchedItems_BotUsers_BotUserId",
                table: "WatchedItems",
                column: "BotUserId",
                principalTable: "BotUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
