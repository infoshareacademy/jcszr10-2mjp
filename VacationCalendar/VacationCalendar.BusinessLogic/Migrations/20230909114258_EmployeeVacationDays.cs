using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VacationCalendar.BusinessLogic.Migrations
{
    /// <inheritdoc />
    public partial class EmployeeVacationDays : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NumberOfVacationDays",
                table: "Employees",
                newName: "VacationDays");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VacationDays",
                table: "Employees",
                newName: "NumberOfVacationDays");
        }
    }
}
