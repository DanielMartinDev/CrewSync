using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shift_Planner___API.Migrations
{
    /// <inheritdoc />
    public partial class EmployeeShiftRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Shifts_EmployeeID",
                table: "Shifts",
                column: "EmployeeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Shifts_Employees_EmployeeID",
                table: "Shifts",
                column: "EmployeeID",
                principalTable: "Employees",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shifts_Employees_EmployeeID",
                table: "Shifts");

            migrationBuilder.DropIndex(
                name: "IX_Shifts_EmployeeID",
                table: "Shifts");
        }
    }
}
