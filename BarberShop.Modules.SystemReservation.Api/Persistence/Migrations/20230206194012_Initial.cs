﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BarberShop.Modules.SystemReservation.Api.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "BarberShop.SystemReservation");

            migrationBuilder.CreateTable(
                name: "ServiceIndustries",
                schema: "BarberShop.SystemReservation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Time = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceIndustries", x => x.Id);
                });

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
                name: "Visits",
                schema: "BarberShop.SystemReservation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    VisitTimeId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ServiceIndustryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Visits_ServiceIndustries_ServiceIndustryId",
                        column: x => x.ServiceIndustryId,
                        principalSchema: "BarberShop.SystemReservation",
                        principalTable: "ServiceIndustries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_Visits_EmployeeId",
                schema: "BarberShop.SystemReservation",
                table: "Visits",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Visits_ServiceIndustryId",
                schema: "BarberShop.SystemReservation",
                table: "Visits",
                column: "ServiceIndustryId");

            migrationBuilder.CreateIndex(
                name: "IX_Visits_UserId",
                schema: "BarberShop.SystemReservation",
                table: "Visits",
                column: "UserId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VisitVisitTime",
                schema: "BarberShop.SystemReservation");

            migrationBuilder.DropTable(
                name: "VisitTimes",
                schema: "BarberShop.SystemReservation");

            migrationBuilder.DropTable(
                name: "Visits",
                schema: "BarberShop.SystemReservation");

            migrationBuilder.DropTable(
                name: "ServiceIndustries",
                schema: "BarberShop.SystemReservation");
        }
    }
}