using BarberShop.Modules.SystemReservation.Api.Entities;
using FastEndpoints;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BarberShop.Modules.SystemReservation.Api.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ReplaceTypeNumberPhone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberPhone",
                schema: "BarberShop.SystemReservation",
                table: "Visits");

            migrationBuilder.AddColumn<DateTime>(
                name: "NumberPhone",
                schema: "BarberShop.SystemReservation",
                table: "Visits",
                type: "nvarchar(max)",
                nullable: false
            );

        }
        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberPhone",
                schema: "BarberShop.SystemReservation",
                table: "Visits");
        }
    }
}
