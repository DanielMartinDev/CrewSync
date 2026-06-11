using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shift_Planner___API.Migrations
{
    /// <inheritdoc />
    public partial class RemoveShiftDay : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Day",
                table: "Shifts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Day",
                table: "Shifts",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
