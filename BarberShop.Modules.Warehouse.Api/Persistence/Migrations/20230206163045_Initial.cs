using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BarberShop.Modules.Warehouse.Api.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "BarberShop.Warehouse");

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
                name: "Orders",
                schema: "BarberShop.Warehouse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DeliveryTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PriceProducts",
                schema: "BarberShop.Warehouse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LastPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceProducts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                schema: "BarberShop.Warehouse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PriceProductId = table.Column<int>(type: "int", nullable: false),
                    AmountProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_AmountProducts_AmountProductId",
                        column: x => x.AmountProductId,
                        principalSchema: "BarberShop.Warehouse",
                        principalTable: "AmountProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_PriceProducts_PriceProductId",
                        column: x => x.PriceProductId,
                        principalSchema: "BarberShop.Warehouse",
                        principalTable: "PriceProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderProduct",
                schema: "BarberShop.Warehouse",
                columns: table => new
                {
                    OrdersId = table.Column<int>(type: "int", nullable: false),
                    ProductsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProduct", x => new { x.OrdersId, x.ProductsId });
                    table.ForeignKey(
                        name: "FK_OrderProduct_Orders_OrdersId",
                        column: x => x.OrdersId,
                        principalSchema: "BarberShop.Warehouse",
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderProduct_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalSchema: "BarberShop.Warehouse",
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderProduct_ProductsId",
                schema: "BarberShop.Warehouse",
                table: "OrderProduct",
                column: "ProductsId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ClientId",
                schema: "BarberShop.Warehouse",
                table: "Orders",
                column: "ClientId");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderProduct",
                schema: "BarberShop.Warehouse");

            migrationBuilder.DropTable(
                name: "Orders",
                schema: "BarberShop.Warehouse");

            migrationBuilder.DropTable(
                name: "Products",
                schema: "BarberShop.Warehouse");

            migrationBuilder.DropTable(
                name: "AmountProducts",
                schema: "BarberShop.Warehouse");

            migrationBuilder.DropTable(
                name: "PriceProducts",
                schema: "BarberShop.Warehouse");
        }
    }
}
