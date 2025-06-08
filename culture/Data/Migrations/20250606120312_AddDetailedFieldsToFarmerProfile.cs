using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace culture.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDetailedFieldsToFarmerProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Region",
                table: "Farmers",
                newName: "TownCity");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Farmers",
                newName: "StreetAddress");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "Farmers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FarmName",
                table: "Farmers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "FarmSize",
                table: "Farmers",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FarmingType",
                table: "Farmers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Farmers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Farmers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "NumberOfWorkers",
                table: "Farmers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Farmers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "Farmers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Province",
                table: "Farmers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "YearsOfExperience",
                table: "Farmers",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "Farmers");

            migrationBuilder.DropColumn(
                name: "FarmName",
                table: "Farmers");

            migrationBuilder.DropColumn(
                name: "FarmSize",
                table: "Farmers");

            migrationBuilder.DropColumn(
                name: "FarmingType",
                table: "Farmers");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Farmers");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Farmers");

            migrationBuilder.DropColumn(
                name: "NumberOfWorkers",
                table: "Farmers");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Farmers");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "Farmers");

            migrationBuilder.DropColumn(
                name: "Province",
                table: "Farmers");

            migrationBuilder.DropColumn(
                name: "YearsOfExperience",
                table: "Farmers");

            migrationBuilder.RenameColumn(
                name: "TownCity",
                table: "Farmers",
                newName: "Region");

            migrationBuilder.RenameColumn(
                name: "StreetAddress",
                table: "Farmers",
                newName: "Name");
        }
    }
}
