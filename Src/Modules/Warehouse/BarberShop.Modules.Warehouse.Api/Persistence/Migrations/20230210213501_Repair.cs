using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BarberShop.Modules.Warehouse.Api.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Repair : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "LastPrice",
                schema: "BarberShop.Warehouse",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastPrice",
                schema: "BarberShop.Warehouse",
                table: "Products");
        }
    }
}
