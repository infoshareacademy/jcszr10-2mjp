using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VacationCalendar.BusinessLogic.Migrations
{
    /// <inheritdoc />
    public partial class RequestStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RequestStatus",
                table: "VacationRequests",
                newName: "RequestStatusId");

            migrationBuilder.CreateTable(
                name: "RequestStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestStatusName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestStatuses", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VacationRequests_RequestStatusId",
                table: "VacationRequests",
                column: "RequestStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_VacationRequests_RequestStatuses_RequestStatusId",
                table: "VacationRequests",
                column: "RequestStatusId",
                principalTable: "RequestStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VacationRequests_RequestStatuses_RequestStatusId",
                table: "VacationRequests");

            migrationBuilder.DropTable(
                name: "RequestStatuses");

            migrationBuilder.DropIndex(
                name: "IX_VacationRequests_RequestStatusId",
                table: "VacationRequests");

            migrationBuilder.RenameColumn(
                name: "RequestStatusId",
                table: "VacationRequests",
                newName: "RequestStatus");
        }
    }
}
