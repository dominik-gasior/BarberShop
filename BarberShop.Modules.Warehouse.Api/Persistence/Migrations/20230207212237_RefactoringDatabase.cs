using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BarberShop.Modules.Warehouse.Api.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RefactoringDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_AmountProducts_AmountProductId",
                schema: "BarberShop.Warehouse",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_PriceProducts_PriceProductId",
                schema: "BarberShop.Warehouse",
                table: "Products");

            migrationBuilder.DropTable(
                name: "AmountProducts",
                schema: "BarberShop.Warehouse");

            migrationBuilder.DropTable(
                name: "PriceProducts",
                schema: "BarberShop.Warehouse");

            migrationBuilder.DropIndex(
                name: "IX_Products_AmountProductId",
                schema: "BarberShop.Warehouse",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_PriceProductId",
                schema: "BarberShop.Warehouse",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "Amount",
                schema: "BarberShop.Warehouse",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                schema: "BarberShop.Warehouse",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "OrderStatus",
                schema: "BarberShop.Warehouse",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 1);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "Amount",
                schema: "BarberShop.Warehouse",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Price",
                schema: "BarberShop.Warehouse",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "OrderStatus",
                schema: "BarberShop.Warehouse",
                table: "Orders");

            migrationBuilder.CreateTable(
                name: "AmountProducts",
                schema: "BarberShop.Warehouse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AmountProducts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PriceProducts",
                schema: "BarberShop.Warehouse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceProducts", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_AmountProductId",
                schema: "BarberShop.Warehouse",
                table: "Products",
                column: "AmountProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_PriceProductId",
                schema: "BarberShop.Warehouse",
                table: "Products",
                column: "PriceProductId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_AmountProducts_AmountProductId",
                schema: "BarberShop.Warehouse",
                table: "Products",
                column: "AmountProductId",
                principalSchema: "BarberShop.Warehouse",
                principalTable: "AmountProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_PriceProducts_PriceProductId",
                schema: "BarberShop.Warehouse",
                table: "Products",
                column: "PriceProductId",
                principalSchema: "BarberShop.Warehouse",
                principalTable: "PriceProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
