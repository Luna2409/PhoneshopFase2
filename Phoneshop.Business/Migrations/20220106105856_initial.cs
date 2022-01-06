using Microsoft.EntityFrameworkCore.Migrations;

namespace Phoneshop.Business.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    BrandID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrandName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.BrandID);
                });

            migrationBuilder.CreateTable(
                name: "Phones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrandID = table.Column<int>(type: "int", nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PriceWithTax = table.Column<double>(type: "float", nullable: false),
                    PriceWithoutTax = table.Column<double>(type: "float", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phones", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "BrandID", "BrandName" },
                values: new object[,]
                {
                    { 1, "Huawei" },
                    { 2, "Samsung" },
                    { 3, "Apple" },
                    { 4, "Google" },
                    { 5, "Xiaomi" }
                });

            migrationBuilder.InsertData(
                table: "Phones",
                columns: new[] { "Id", "Brand", "BrandID", "Description", "PriceWithTax", "PriceWithoutTax", "Stock", "Type" },
                values: new object[,]
                {
                    { 1, null, 1, "6.47\" FHD+ (2340x1080) OLED, \nKirin 980 Octa - Core(2x Cortex - A76 2.6GHz + 2x Cortex - A76 1.92GHz + 4x Cortex - A55 1.8GHz), \n8GB RAM, 128GB ROM, 40 + 20 + 8 + TOF / 32MP, \nDual SIM, 4200mAh, Android 9.0 + EMUI 9.1", 697.0, 0.0, 34, "P30" },
                    { 2, null, 2, "64 megapixel camera, 4k videokwaliteit \n6.5 inch AMOLED scherm \n128 GB opslaggeheugen (Uitbreidbaar met Micro-sd) \nWater- en stofbestendig (IP67)", 399.0, 0.0, 23, "Galaxy A52" },
                    { 3, null, 3, "Met de dubbele camera schiet je in elke situatie een perfecte foto of video \nDe krachtige A13-chipset zorgt voor razendsnelle prestaties \nMet Face ID hoef je enkel en alleen naar je toestel te kijken om te ontgrendelen \nHet toestel heeft een lange accuduur dankzij een energiezuinige processor", 619.0, 0.0, 39, "IPhone 11" },
                    { 4, null, 4, "12.2 megapixel camera, 4k videokwaliteit \n5.81 inch OLED scherm \n128 GB opslaggeheugen \n3140 mAh accucapaciteit", 411.0, 0.0, 21, "Pixel 4a" },
                    { 5, null, 5, "108 megapixel camera, 4k videokwaliteit \n6.67 inch AMOLED scherm \n128 GB opslaggeheugen (Uitbreidbaar met Micro-sd) \nWater- en stofbestendig (IP53)", 298.0, 0.0, 98, "Redmi Note 10 Pro" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "Phones");
        }
    }
}
