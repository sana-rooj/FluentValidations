using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeFacilitationPortal.DataRepository.Migrations
{
    public partial class updatedloginmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Samples");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Logins");

            migrationBuilder.AlterColumn<string>(
                name: "Detail",
                table: "TrainingRequests",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BussinessJustification",
                table: "TrainingRequests",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "EncryptedPassword",
                table: "Logins",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EncryptedPassword",
                table: "Logins");

            migrationBuilder.AlterColumn<string>(
                name: "Detail",
                table: "TrainingRequests",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BussinessJustification",
                table: "TrainingRequests",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Logins",
                maxLength: 255,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Samples",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Age = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Samples", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Samples",
                columns: new[] { "Id", "Age", "Name" },
                values: new object[,]
                {
                    { 1, 24, "sample-1" },
                    { 2, 23, "sample-2" },
                    { 3, 25, "sample-3" },
                    { 4, 26, "sample-4" }
                });
        }
    }
}
