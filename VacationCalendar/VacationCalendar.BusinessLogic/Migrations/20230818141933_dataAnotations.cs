using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VacationCalendar.BusinessLogic.Migrations
{
    /// <inheritdoc />
    public partial class dataAnotations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Managers",
                type: "VARCHAR(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Managers",
                type: "VARCHAR(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Employees",
                type: "VARCHAR(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Employees",
                type: "VARCHAR(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Managers",
                type: "varchar(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Managers",
                type: "varchar(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Employees",
                type: "varchar(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Employees",
                type: "varchar(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "VARCHAR(100)",
                oldMaxLength: 100);
        }
    }
}
