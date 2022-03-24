using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop.Migrations
{
    public partial class InitialCreate4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "CategoryId", "Description", "Image", "Name", "Price", "ShortDesc" },
                values: new object[,]
                {
                    { 2, 2, "Ми представляємо вам найпотужнішу, саму оснащену, ударотривкий та найефективнішу версію смартфона 2021 року від румунської компанії iHunt .", "https://content2.rozetka.com.ua/goods/images/big/186736679.jpg", "iHunt Titan P13000 PRO 2021", 13940.0, "figna" },
                    { 3, 3, "Холодильники з системою NeoFrost ", "https://content1.rozetka.com.ua/goods/images/big/175324656.jpg", "BEKO CNA295K20XP", 10999.0, "figna" },
                    { 4, 4, "Ланцюгова пила Bosch UniversalChain ", "https://content.rozetka.com.ua/goods/images/big/144435754.jpg", "Bosch UniversalChain 40", 3958.0, "figna" },
                    { 5, 5, "Велосипед Champion Spark 29 ", "https://content2.rozetka.com.ua/goods/images/big/44538732.jpg", "Champion Spark 29 19.5 Black-neon yellow-white", 5460.0, "figna" },
                    { 6, 6, "ВНабір паперу офісного Zoom Stora Enso А4 80 г/м2 клас С + 5 пачок по 500 аркушів Біла ", "https://content2.rozetka.com.ua/goods/images/big/252699020.jpg", "Zoom Stora Enso А4 80 г/м2 клас С + 5 пачок по 500 аркушів Біла", 1199.0, "figna" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 6);
        }
    }
}
