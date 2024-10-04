using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fashion_Flex.Migrations
{
    /// <inheritdoc />
    public partial class refactor_Payment_Customer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Payment");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Customers",
                newName: "Street_Name");

            migrationBuilder.AlterColumn<string>(
                name: "Transaction_Id",
                table: "Payment",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Building_No",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Governorate",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Building_No",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Governorate",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "Street_Name",
                table: "Customers",
                newName: "Address");

            migrationBuilder.AlterColumn<string>(
                name: "Transaction_Id",
                table: "Payment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                table: "Payment",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
