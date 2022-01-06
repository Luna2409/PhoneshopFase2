using Microsoft.EntityFrameworkCore.Migrations;

namespace Phoneshop.Business.Migrations
{
    public partial class ChangedNameBrandIDToId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BrandID",
                table: "Brands",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Brands",
                newName: "BrandID");
        }
    }
}
