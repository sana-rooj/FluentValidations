using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeFacilitationPortal.DataRepository.Migrations
{
    public partial class removeNotMappedFromEmployeeInTrainingRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmployeeName",
                table: "TrainingRequests",
                maxLength: 100,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeeName",
                table: "TrainingRequests");
        }
    }
}
