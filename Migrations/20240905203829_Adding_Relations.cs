using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fashion_Flex.Migrations
{
    /// <inheritdoc />
    public partial class Adding_Relations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Product_Name",
                table: "Products",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Date_Added",
                table: "Products",
                newName: "Added_Date");

            migrationBuilder.RenameColumn(
                name: "Beand_Id",
                table: "Favorite_Brands",
                newName: "Brand_Id");

            migrationBuilder.RenameColumn(
                name: "is_active",
                table: "Customers",
                newName: "Is_Active");

            migrationBuilder.AlterColumn<double>(
                name: "Discount",
                table: "Products",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "Brand_Id",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Transaction_Id",
                table: "Payment",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "Total_Amount",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "Payment_Id",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<bool>(
                name: "Is_Active",
                table: "Customers",
                type: "bit",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Phone_Number",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "Date_Of_Birth",
                table: "Customers",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "Logo",
                table: "Brands",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_Customer_Id",
                table: "Reviews",
                column: "Customer_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_Product_Id",
                table: "Reviews",
                column: "Product_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Brand_Id",
                table: "Products",
                column: "Brand_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_Order_Id",
                table: "Payment",
                column: "Order_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_Customer_Id",
                table: "Orders",
                column: "Customer_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_Payment_Id",
                table: "Orders",
                column: "Payment_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Order_Items_Order_Id",
                table: "Order_Items",
                column: "Order_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Order_Items_Product_Id",
                table: "Order_Items",
                column: "Product_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Favorite_Brands_Brand_Id",
                table: "Favorite_Brands",
                column: "Brand_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Favorite_Brands_Customer_Id",
                table: "Favorite_Brands",
                column: "Customer_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Favorite_Brands_Brands_Brand_Id",
                table: "Favorite_Brands",
                column: "Brand_Id",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Favorite_Brands_Customers_Customer_Id",
                table: "Favorite_Brands",
                column: "Customer_Id",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Items_Orders_Order_Id",
                table: "Order_Items",
                column: "Order_Id",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Items_Products_Product_Id",
                table: "Order_Items",
                column: "Product_Id",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Customers_Customer_Id",
                table: "Orders",
                column: "Customer_Id",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Payment_Payment_Id",
                table: "Orders",
                column: "Payment_Id",
                principalTable: "Payment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_Orders_Order_Id",
                table: "Payment",
                column: "Order_Id",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Brands_Brand_Id",
                table: "Products",
                column: "Brand_Id",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Customers_Customer_Id",
                table: "Reviews",
                column: "Customer_Id",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Products_Product_Id",
                table: "Reviews",
                column: "Product_Id",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Favorite_Brands_Brands_Brand_Id",
                table: "Favorite_Brands");

            migrationBuilder.DropForeignKey(
                name: "FK_Favorite_Brands_Customers_Customer_Id",
                table: "Favorite_Brands");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Items_Orders_Order_Id",
                table: "Order_Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Items_Products_Product_Id",
                table: "Order_Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Customers_Customer_Id",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Payment_Payment_Id",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Payment_Orders_Order_Id",
                table: "Payment");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Brands_Brand_Id",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Customers_Customer_Id",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Products_Product_Id",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_Customer_Id",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_Product_Id",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Products_Brand_Id",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Payment_Order_Id",
                table: "Payment");

            migrationBuilder.DropIndex(
                name: "IX_Orders_Customer_Id",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_Payment_Id",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Order_Items_Order_Id",
                table: "Order_Items");

            migrationBuilder.DropIndex(
                name: "IX_Order_Items_Product_Id",
                table: "Order_Items");

            migrationBuilder.DropIndex(
                name: "IX_Favorite_Brands_Brand_Id",
                table: "Favorite_Brands");

            migrationBuilder.DropIndex(
                name: "IX_Favorite_Brands_Customer_Id",
                table: "Favorite_Brands");

            migrationBuilder.DropColumn(
                name: "Brand_Id",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Payment_Id",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Products",
                newName: "Product_Name");

            migrationBuilder.RenameColumn(
                name: "Added_Date",
                table: "Products",
                newName: "Date_Added");

            migrationBuilder.RenameColumn(
                name: "Brand_Id",
                table: "Favorite_Brands",
                newName: "Beand_Id");

            migrationBuilder.RenameColumn(
                name: "Is_Active",
                table: "Customers",
                newName: "is_active");

            migrationBuilder.AlterColumn<int>(
                name: "Discount",
                table: "Products",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<int>(
                name: "Transaction_Id",
                table: "Payment",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "Total_Amount",
                table: "Orders",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<int>(
                name: "Phone_Number",
                table: "Customers",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "is_active",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date_Of_Birth",
                table: "Customers",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<int>(
                name: "Logo",
                table: "Brands",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
