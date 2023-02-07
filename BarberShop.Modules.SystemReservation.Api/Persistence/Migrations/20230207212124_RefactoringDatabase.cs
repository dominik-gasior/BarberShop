using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BarberShop.Modules.SystemReservation.Api.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RefactoringDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VisitVisitTime",
                schema: "BarberShop.SystemReservation");

            migrationBuilder.DropTable(
                name: "VisitTimes",
                schema: "BarberShop.SystemReservation");

            migrationBuilder.DropIndex(
                name: "IX_Visits_VisitTimeId",
                schema: "BarberShop.SystemReservation",
                table: "Visits");

            migrationBuilder.DropColumn(
                name: "VisitTimeId",
                schema: "BarberShop.SystemReservation",
                table: "Visits");

            migrationBuilder.AddColumn<DateTime>(
                name: "Time",
                schema: "BarberShop.SystemReservation",
                table: "Visits",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Time",
                schema: "BarberShop.SystemReservation",
                table: "Visits");

            migrationBuilder.AddColumn<int>(
                name: "VisitTimeId",
                schema: "BarberShop.SystemReservation",
                table: "Visits",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "VisitTimes",
                schema: "BarberShop.SystemReservation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisitTimes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VisitVisitTime",
                schema: "BarberShop.SystemReservation",
                columns: table => new
                {
                    VisitTimesId = table.Column<int>(type: "int", nullable: false),
                    VisitsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisitVisitTime", x => new { x.VisitTimesId, x.VisitsId });
                    table.ForeignKey(
                        name: "FK_VisitVisitTime_VisitTimes_VisitTimesId",
                        column: x => x.VisitTimesId,
                        principalSchema: "BarberShop.SystemReservation",
                        principalTable: "VisitTimes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VisitVisitTime_Visits_VisitsId",
                        column: x => x.VisitsId,
                        principalSchema: "BarberShop.SystemReservation",
                        principalTable: "Visits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Visits_VisitTimeId",
                schema: "BarberShop.SystemReservation",
                table: "Visits",
                column: "VisitTimeId");

            migrationBuilder.CreateIndex(
                name: "IX_VisitVisitTime_VisitsId",
                schema: "BarberShop.SystemReservation",
                table: "VisitVisitTime",
                column: "VisitsId");
        }
    }
}
