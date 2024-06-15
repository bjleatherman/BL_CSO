using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityTutorial.Migrations
{
    public partial class systemsDamageTrackerAddedToPlayerGameSave : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SystemsAttack",
                table: "PlayerGameSaves",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SystemsDetect",
                table: "PlayerGameSaves",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SystemsEvade",
                table: "PlayerGameSaves",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SystemsReactor",
                table: "PlayerGameSaves",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "GameVM",
                columns: table => new
                {
                    GameId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastMoveDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MapLength = table.Column<int>(type: "int", nullable: false),
                    MapWidth = table.Column<int>(type: "int", nullable: false),
                    IslandCoordinates = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxHealth = table.Column<int>(type: "int", nullable: false),
                    MaxMines = table.Column<int>(type: "int", nullable: false),
                    MaxDrones = table.Column<int>(type: "int", nullable: false),
                    MaxSneak = table.Column<int>(type: "int", nullable: false),
                    MaxTorpedo = table.Column<int>(type: "int", nullable: false),
                    MaxSonar = table.Column<int>(type: "int", nullable: false),
                    PlayerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsTurn = table.Column<bool>(type: "bit", nullable: false),
                    TotalDirectionHistory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentDirectionHistory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalCoordinateHistory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentCoordinateHistory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Health = table.Column<int>(type: "int", nullable: false),
                    Mines = table.Column<int>(type: "int", nullable: false),
                    Drones = table.Column<int>(type: "int", nullable: false),
                    Sneak = table.Column<int>(type: "int", nullable: false),
                    Torpedo = table.Column<int>(type: "int", nullable: false),
                    Sonar = table.Column<int>(type: "int", nullable: false),
                    EnemyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EnemyDirectionHisory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EnemyCurrentDirectionHistory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EnemyHealth = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameVM", x => x.GameId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameVM");

            migrationBuilder.DropColumn(
                name: "SystemsAttack",
                table: "PlayerGameSaves");

            migrationBuilder.DropColumn(
                name: "SystemsDetect",
                table: "PlayerGameSaves");

            migrationBuilder.DropColumn(
                name: "SystemsEvade",
                table: "PlayerGameSaves");

            migrationBuilder.DropColumn(
                name: "SystemsReactor",
                table: "PlayerGameSaves");
        }
    }
}
