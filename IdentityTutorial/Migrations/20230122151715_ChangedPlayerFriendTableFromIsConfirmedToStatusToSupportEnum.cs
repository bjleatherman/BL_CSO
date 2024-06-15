using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityTutorial.Migrations
{
    public partial class ChangedPlayerFriendTableFromIsConfirmedToStatusToSupportEnum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsConfirmed",
                table: "PlayerFriends");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "PlayerFriends",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Torpedo",
                table: "GameVM",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Sonar",
                table: "GameVM",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Sneak",
                table: "GameVM",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Mines",
                table: "GameVM",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Health",
                table: "GameVM",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Drones",
                table: "GameVM",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "AllCharged",
                table: "GameVM",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "DronesValue",
                table: "GameVM",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "East",
                table: "GameVM",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstMove",
                table: "GameVM",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "HealthValue",
                table: "GameVM",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MinesValue",
                table: "GameVM",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "North",
                table: "GameVM",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "SneakValue",
                table: "GameVM",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SonarValue",
                table: "GameVM",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "South",
                table: "GameVM",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Surface",
                table: "GameVM",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SystemsAttack",
                table: "GameVM",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SystemsDetect",
                table: "GameVM",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SystemsEvade",
                table: "GameVM",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SystemsReactor",
                table: "GameVM",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TorpedoValue",
                table: "GameVM",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "West",
                table: "GameVM",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "PlayerFriendVM",
                columns: table => new
                {
                    FriendId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FriendName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerFriendVM", x => x.FriendId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayerFriendVM");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "PlayerFriends");

            migrationBuilder.DropColumn(
                name: "AllCharged",
                table: "GameVM");

            migrationBuilder.DropColumn(
                name: "DronesValue",
                table: "GameVM");

            migrationBuilder.DropColumn(
                name: "East",
                table: "GameVM");

            migrationBuilder.DropColumn(
                name: "FirstMove",
                table: "GameVM");

            migrationBuilder.DropColumn(
                name: "HealthValue",
                table: "GameVM");

            migrationBuilder.DropColumn(
                name: "MinesValue",
                table: "GameVM");

            migrationBuilder.DropColumn(
                name: "North",
                table: "GameVM");

            migrationBuilder.DropColumn(
                name: "SneakValue",
                table: "GameVM");

            migrationBuilder.DropColumn(
                name: "SonarValue",
                table: "GameVM");

            migrationBuilder.DropColumn(
                name: "South",
                table: "GameVM");

            migrationBuilder.DropColumn(
                name: "Surface",
                table: "GameVM");

            migrationBuilder.DropColumn(
                name: "SystemsAttack",
                table: "GameVM");

            migrationBuilder.DropColumn(
                name: "SystemsDetect",
                table: "GameVM");

            migrationBuilder.DropColumn(
                name: "SystemsEvade",
                table: "GameVM");

            migrationBuilder.DropColumn(
                name: "SystemsReactor",
                table: "GameVM");

            migrationBuilder.DropColumn(
                name: "TorpedoValue",
                table: "GameVM");

            migrationBuilder.DropColumn(
                name: "West",
                table: "GameVM");

            migrationBuilder.AddColumn<bool>(
                name: "IsConfirmed",
                table: "PlayerFriends",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "Torpedo",
                table: "GameVM",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "Sonar",
                table: "GameVM",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "Sneak",
                table: "GameVM",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "Mines",
                table: "GameVM",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "Health",
                table: "GameVM",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "Drones",
                table: "GameVM",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
