using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "CurrentAirHumidity",
                table: "Devices",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "CurrentSoilHumidity",
                table: "Devices",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "CurrentTemperature",
                table: "Devices",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TargetAirHumidity",
                table: "Devices",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TargetSoilHumidity",
                table: "Devices",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TargetTemperature",
                table: "Devices",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentAirHumidity",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "CurrentSoilHumidity",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "CurrentTemperature",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "TargetAirHumidity",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "TargetSoilHumidity",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "TargetTemperature",
                table: "Devices");
        }
    }
}
