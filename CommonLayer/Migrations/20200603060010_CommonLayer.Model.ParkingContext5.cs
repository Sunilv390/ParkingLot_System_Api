using Microsoft.EntityFrameworkCore.Migrations;

namespace CommonLayer.Migrations
{
    public partial class CommonLayerModelParkingContext5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Handicap",
                table: "parkingPortals",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Handicap",
                table: "parkingPortals");
        }
    }
}
