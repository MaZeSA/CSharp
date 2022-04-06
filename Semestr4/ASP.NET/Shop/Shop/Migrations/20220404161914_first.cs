using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop.Migrations
{
    public partial class first : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "DisplayOrder", "Name" },
                values: new object[,]
                {
                    { 1, 0, "Комп'ютери та ноутбуки" },
                    { 2, 0, "Смартфони" },
                    { 3, 0, "Побутова техніка" },
                    { 4, 0, "Дача, сад, город" },
                    { 5, 0, "Спорт і захоплення" },
                    { 6, 0, "Офіс, школа, книги" }
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "CategoryId", "Description", "Image", "Name", "Price", "ShortDescription" },
                values: new object[,]
                {
                    { 1, 1, "test", "https://video.rozetka.com.ua/img_superportal/kompyutery_i_noutbuki/noutbuki.png", "ПК Х123434", 436765, "figna" },
                    { 2, 2, "Ми представляємо вам найпотужнішу, саму оснащену, ударотривкий та найефективнішу версію смартфона 2021 року від румунської компанії iHunt .", "https://content2.rozetka.com.ua/goods/images/big/186736679.jpg", "iHunt Titan P13000 PRO 2021", 13940, "figna" },
                    { 3, 3, "Холодильники з системою NeoFrost ", "https://content1.rozetka.com.ua/goods/images/big/175324656.jpg", "BEKO CNA295K20XP", 10999, "figna" },
                    { 4, 4, "Ланцюгова пила Bosch UniversalChain ", "https://content.rozetka.com.ua/goods/images/big/144435754.jpg", "Bosch UniversalChain 40", 3958, "figna" },
                    { 5, 5, "Велосипед Champion Spark 29 ", "https://content2.rozetka.com.ua/goods/images/big/44538732.jpg", "Champion Spark 29 19.5 Black-neon yellow-white", 5460, "figna" },
                    { 6, 6, "ВНабір паперу офісного Zoom Stora Enso А4 80 г/м2 клас С + 5 пачок по 500 аркушів Біла ", "https://content2.rozetka.com.ua/goods/images/big/252699020.jpg", "Zoom Stora Enso А4 80 г/м2 клас С + 5 пачок по 500 аркушів Біла", 1199, "figna" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_CategoryId",
                table: "Product",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
