using Microsoft.EntityFrameworkCore.Migrations;

namespace Phoneshop.Business.Migrations
{
    public partial class changedPropInPhones : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Brand",
                table: "Phones");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Brand",
                table: "Phones",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
