using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CommonLayer.Migrations
{
    public partial class CommonLayerModelParkingContext1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Charges",
                table: "parkingPortals");

            migrationBuilder.DropColumn(
                name: "InTime",
                table: "parkingPortals");

            migrationBuilder.RenameColumn(
                name: "VehicleType",
                table: "parkingPortals",
                newName: "VehicleColor");

            migrationBuilder.RenameColumn(
                name: "OwnerName",
                table: "parkingPortals",
                newName: "DriverName");

            migrationBuilder.RenameColumn(
                name: "OutTime",
                table: "parkingPortals",
                newName: "ParkingDate");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "parkingPortals",
                newName: "ReceiptNo");

            migrationBuilder.AddColumn<string>(
                name: "Brand",
                table: "parkingPortals",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Brand",
                table: "parkingPortals");

            migrationBuilder.RenameColumn(
                name: "VehicleColor",
                table: "parkingPortals",
                newName: "VehicleType");

            migrationBuilder.RenameColumn(
                name: "ParkingDate",
                table: "parkingPortals",
                newName: "OutTime");

            migrationBuilder.RenameColumn(
                name: "DriverName",
                table: "parkingPortals",
                newName: "OwnerName");

            migrationBuilder.RenameColumn(
                name: "ReceiptNo",
                table: "parkingPortals",
                newName: "Id");

            migrationBuilder.AddColumn<int>(
                name: "Charges",
                table: "parkingPortals",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "InTime",
                table: "parkingPortals",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
