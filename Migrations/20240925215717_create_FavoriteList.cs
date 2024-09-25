using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fashion_Flex.Migrations
{
    /// <inheritdoc />
    public partial class create_FavoriteList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Brand_Id",
                table: "Products");

            migrationBuilder.CreateTable(
                name: "FavoriteList",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Customer_Id = table.Column<int>(type: "int", nullable: false),
                    Product_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FavoriteList_Customers_Customer_Id",
                        column: x => x.Customer_Id,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FavoriteList_Products_Product_Id",
                        column: x => x.Product_Id,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteList_Customer_Id",
                table: "FavoriteList",
                column: "Customer_Id");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteList_Product_Id",
                table: "FavoriteList",
                column: "Product_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FavoriteList");

            migrationBuilder.AddColumn<int>(
                name: "Brand_Id",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
