using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VacationCalendar.BusinessLogic.Migrations
{
    /// <inheritdoc />
    public partial class NumberOfVacationDaysInEmployeeAndVacatinDaysInRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VacationDays",
                table: "VacationRequests",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NumberOfVacationDays",
                table: "Employees",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VacationDays",
                table: "VacationRequests");

            migrationBuilder.AlterColumn<int>(
                name: "NumberOfVacationDays",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
