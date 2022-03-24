using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop.Migrations
{
    public partial class InitialCreate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "CategoryId", "Description", "Image", "Name", "Price", "ShortDesc" },
                values: new object[] { 1, 1, null, "https://video.rozetka.com.ua/img_superportal/kompyutery_i_noutbuki/noutbuki.png", "ПК Х123434", 436765.0, null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
