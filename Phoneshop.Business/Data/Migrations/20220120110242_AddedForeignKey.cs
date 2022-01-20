using Microsoft.EntityFrameworkCore.Migrations;

namespace Phoneshop.Business.Migrations
{
    public partial class AddedForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BrandName",
                table: "Brands",
                newName: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Phones_BrandID",
                table: "Phones",
                column: "BrandID");

            migrationBuilder.AddForeignKey(
                name: "FK_Phones_Brands_BrandID",
                table: "Phones",
                column: "BrandID",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Phones_Brands_BrandID",
                table: "Phones");

            migrationBuilder.DropIndex(
                name: "IX_Phones_BrandID",
                table: "Phones");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Brands",
                newName: "BrandName");
        }
    }
}
