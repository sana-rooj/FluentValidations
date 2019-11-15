using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeFacilitationPortal.DataRepository.Migrations
{
    public partial class updatedpagemodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PageUrl",
                table: "Pages",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PageUrl",
                table: "Pages");
        }
    }
}
