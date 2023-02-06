using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BarberShop.Modules.SystemReservation.Api.Migrations
{
    /// <inheritdoc />
    public partial class RoleEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Roles_RoleId",
                schema: "BarberShop.SystemReservation",
                table: "Employees");

            migrationBuilder.DropTable(
                name: "Roles",
                schema: "BarberShop.SystemReservation");

            migrationBuilder.AddColumn<int>(
                name: "Role",
                schema: "BarberShop.SystemReservation",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                schema: "BarberShop.SystemReservation",
                table: "Employees");

            migrationBuilder.CreateTable(
                name: "Roles",
                schema: "BarberShop.SystemReservation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Roles_RoleId",
                schema: "BarberShop.SystemReservation",
                table: "Employees",
                column: "RoleId",
                principalSchema: "BarberShop.SystemReservation",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
