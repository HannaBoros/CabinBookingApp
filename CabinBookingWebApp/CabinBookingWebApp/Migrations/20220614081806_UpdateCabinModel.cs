using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CabinBookingWebApp.Migrations
{
    public partial class UpdateCabinModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
     

            migrationBuilder.AddColumn<int>(
                name: "PersonCapability",
                table: "Cabins",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PersonCapability",
                table: "Cabins");

          
        }
    }
}
