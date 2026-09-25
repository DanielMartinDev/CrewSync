using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shift_Planner___API.Migrations
{
    /// <inheritdoc />
    public partial class HolidayAllowance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HolidayAllowance",
                table: "Employees",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HolidayAllowance",
                table: "Employees");
        }
    }
}
