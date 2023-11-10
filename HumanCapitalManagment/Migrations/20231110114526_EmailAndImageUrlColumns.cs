using Microsoft.EntityFrameworkCore.Migrations;

namespace HumanCapitalManagment.Migrations
{
    public partial class EmailAndImageUrlColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmailAddress",
                table: "HRSpecialists",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "HRSpecialists",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailAddress",
                table: "HRSpecialists");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "HRSpecialists");
        }
    }
}
