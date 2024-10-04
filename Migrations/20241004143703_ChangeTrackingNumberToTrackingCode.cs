using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fashion_Flex.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTrackingNumberToTrackingCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tracking_Number",
                table: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "Tracking_Code",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tracking_Code",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "Tracking_Number",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
