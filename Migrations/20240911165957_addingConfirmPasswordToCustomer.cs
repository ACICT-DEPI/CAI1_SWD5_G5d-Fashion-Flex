using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fashion_Flex.Migrations
{
    /// <inheritdoc />
    public partial class addingConfirmPasswordToCustomer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Confirm_Password",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Confirm_Password",
                table: "Customers");
        }
    }
}
