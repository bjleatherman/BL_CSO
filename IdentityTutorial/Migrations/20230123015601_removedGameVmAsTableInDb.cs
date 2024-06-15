using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityTutorial.Migrations
{
    public partial class removedGameVmAsTableInDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameVM");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "PlayerFriendVM",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "StatusCode",
                table: "PlayerFriendVM",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusCode",
                table: "PlayerFriendVM");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "PlayerFriendVM",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "GameVM",
                columns: table => new
                {
                    GameId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AllCharged = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentCoordinateHistory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentDirectionHistory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Drones = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DronesValue = table.Column<int>(type: "int", nullable: false),
                    East = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EnemyCurrentDirectionHistory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EnemyDirectionHisory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EnemyHealth = table.Column<int>(type: "int", nullable: false),
                    EnemyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstMove = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Health = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HealthValue = table.Column<int>(type: "int", nullable: false),
                    IsTurn = table.Column<bool>(type: "bit", nullable: false),
                    IslandCoordinates = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastMoveDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MapLength = table.Column<int>(type: "int", nullable: false),
                    MapWidth = table.Column<int>(type: "int", nullable: false),
                    MaxDrones = table.Column<int>(type: "int", nullable: false),
                    MaxHealth = table.Column<int>(type: "int", nullable: false),
                    MaxMines = table.Column<int>(type: "int", nullable: false),
                    MaxSneak = table.Column<int>(type: "int", nullable: false),
                    MaxSonar = table.Column<int>(type: "int", nullable: false),
                    MaxTorpedo = table.Column<int>(type: "int", nullable: false),
                    Mines = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MinesValue = table.Column<int>(type: "int", nullable: false),
                    North = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlayerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sneak = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SneakValue = table.Column<int>(type: "int", nullable: false),
                    Sonar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SonarValue = table.Column<int>(type: "int", nullable: false),
                    South = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Surface = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SystemsAttack = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SystemsDetect = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SystemsEvade = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SystemsReactor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Torpedo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TorpedoValue = table.Column<int>(type: "int", nullable: false),
                    TotalCoordinateHistory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalDirectionHistory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    West = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameVM", x => x.GameId);
                });
        }
    }
}
