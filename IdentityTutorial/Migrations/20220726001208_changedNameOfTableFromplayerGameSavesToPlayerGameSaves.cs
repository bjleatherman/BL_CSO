using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityTutorial.Migrations
{
    public partial class changedNameOfTableFromplayerGameSavesToPlayerGameSaves : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_playerGameSaves",
                table: "playerGameSaves");

            migrationBuilder.RenameTable(
                name: "playerGameSaves",
                newName: "PlayerGameSaves");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlayerGameSaves",
                table: "PlayerGameSaves",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PlayerGameSaves",
                table: "PlayerGameSaves");

            migrationBuilder.RenameTable(
                name: "PlayerGameSaves",
                newName: "playerGameSaves");

            migrationBuilder.AddPrimaryKey(
                name: "PK_playerGameSaves",
                table: "playerGameSaves",
                column: "Id");
        }
    }
}
