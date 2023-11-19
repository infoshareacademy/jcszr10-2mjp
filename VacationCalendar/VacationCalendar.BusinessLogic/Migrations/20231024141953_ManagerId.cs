using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VacationCalendar.BusinessLogic.Migrations
{
    /// <inheritdoc />
    public partial class ManagerId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ManagerId",
                table: "Employees",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ManagerId",
                table: "Employees");
        }
    }
}
