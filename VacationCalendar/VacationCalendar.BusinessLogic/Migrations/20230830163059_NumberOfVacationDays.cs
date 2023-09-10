using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VacationCalendar.BusinessLogic.Migrations
{
    /// <inheritdoc />
    public partial class NumberOfVacationDays : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfDays",
                table: "VacationRequests");

            migrationBuilder.AddColumn<int>(
                name: "NumberOfVacationDays",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfVacationDays",
                table: "Employees");

            migrationBuilder.AddColumn<int>(
                name: "NumberOfDays",
                table: "VacationRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
