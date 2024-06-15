using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityTutorial.Migrations
{
    public partial class inGamesTableChangedIslandCoordsToIslandCoordinates2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            //commented these out b/c EF Core was being a big freaking nerd about it for some reason


            //migrationBuilder.RenameColumn(
            //    name: "IslandCoords",
            //    table: "Games",
            //    newName: "IslandCoordinates");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.RenameColumn(
            //    name: "IslandCoordinates",
            //    table: "Games",
            //    newName: "IslandCoords");
        }
    }
}
