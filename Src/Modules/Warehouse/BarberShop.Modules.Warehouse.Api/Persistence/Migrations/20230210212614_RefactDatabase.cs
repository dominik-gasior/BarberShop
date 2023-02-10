using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BarberShop.Modules.Warehouse.Api.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RefactDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Products_AmountProductId",
                schema: "BarberShop.Warehouse",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_PriceProductId",
                schema: "BarberShop.Warehouse",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "AmountProductId",
                schema: "BarberShop.Warehouse",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PriceProductId",
                schema: "BarberShop.Warehouse",
                table: "Products");

            migrationBuilder.CreateTable(
                name: "Clients",
                schema: "BarberShop.Warehouse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Clients_ClientId",
                schema: "BarberShop.Warehouse",
                table: "Orders",
                column: "ClientId",
                principalSchema: "BarberShop.Warehouse",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Clients_ClientId",
                schema: "BarberShop.Warehouse",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "Clients",
                schema: "BarberShop.Warehouse");

            migrationBuilder.AddColumn<int>(
                name: "AmountProductId",
                schema: "BarberShop.Warehouse",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PriceProductId",
                schema: "BarberShop.Warehouse",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Products_AmountProductId",
                schema: "BarberShop.Warehouse",
                table: "Products",
                column: "AmountProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_PriceProductId",
                schema: "BarberShop.Warehouse",
                table: "Products",
                column: "PriceProductId");
        }
    }
}
