using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CabinBookingWebApp.Migrations
{
    public partial class AddBookingStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Booking",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Booking");
        }
    }
}
