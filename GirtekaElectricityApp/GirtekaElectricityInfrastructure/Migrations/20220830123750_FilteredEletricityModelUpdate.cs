using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GirtekaElectricityInfrastructure.Migrations
{
    public partial class FilteredEletricityModelUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "GeneratedAndConsumedDifference",
                table: "FilteredElectricity",
                type: "float",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GeneratedAndConsumedDifference",
                table: "FilteredElectricity");
        }
    }
}
