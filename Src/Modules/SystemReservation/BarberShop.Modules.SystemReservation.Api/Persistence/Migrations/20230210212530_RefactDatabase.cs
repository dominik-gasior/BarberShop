using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BarberShop.Modules.SystemReservation.Api.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RefactDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberPhone",
                schema: "BarberShop.SystemReservation",
                table: "Visits");

            migrationBuilder.RenameColumn(
                name: "UserId",
                schema: "BarberShop.SystemReservation",
                table: "Visits",
                newName: "ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_Visits_UserId",
                schema: "BarberShop.SystemReservation",
                table: "Visits",
                newName: "IX_Visits_ClientId");

            migrationBuilder.CreateTable(
                name: "Clients",
                schema: "BarberShop.SystemReservation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fullname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberPhone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                schema: "BarberShop.SystemReservation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fullname = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Visits_Clients_ClientId",
                schema: "BarberShop.SystemReservation",
                table: "Visits",
                column: "ClientId",
                principalSchema: "BarberShop.SystemReservation",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Visits_Employees_EmployeeId",
                schema: "BarberShop.SystemReservation",
                table: "Visits",
                column: "EmployeeId",
                principalSchema: "BarberShop.SystemReservation",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Visits_Clients_ClientId",
                schema: "BarberShop.SystemReservation",
                table: "Visits");

            migrationBuilder.DropForeignKey(
                name: "FK_Visits_Employees_EmployeeId",
                schema: "BarberShop.SystemReservation",
                table: "Visits");

            migrationBuilder.DropTable(
                name: "Clients",
                schema: "BarberShop.SystemReservation");

            migrationBuilder.DropTable(
                name: "Employees",
                schema: "BarberShop.SystemReservation");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                schema: "BarberShop.SystemReservation",
                table: "Visits",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Visits_ClientId",
                schema: "BarberShop.SystemReservation",
                table: "Visits",
                newName: "IX_Visits_UserId");

            migrationBuilder.AddColumn<string>(
                name: "NumberPhone",
                schema: "BarberShop.SystemReservation",
                table: "Visits",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
