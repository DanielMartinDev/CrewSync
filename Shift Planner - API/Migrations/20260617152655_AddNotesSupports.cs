using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shift_Planner___API.Migrations
{
    /// <inheritdoc />
    public partial class AddNotesSupports : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Shifts",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ManagerNotes",
                table: "HolidayRequests",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Shifts");

            migrationBuilder.DropColumn(
                name: "ManagerNotes",
                table: "HolidayRequests");
        }
    }
}
