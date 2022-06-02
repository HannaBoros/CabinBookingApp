using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CabinBookingWebApp.Migrations
{
    public partial class AddNewFieldsToCabin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MainImageUrl",
                table: "Cabins",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MainImageUrl",
                table: "Cabins");
        }
    }
}
