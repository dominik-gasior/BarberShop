using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BarberShop.Modules.Users.Api.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Repair : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoleId",
                schema: "BarberShop.Users",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                schema: "BarberShop.Users",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
