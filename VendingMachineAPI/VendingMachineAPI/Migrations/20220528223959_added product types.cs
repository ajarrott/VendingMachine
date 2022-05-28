using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VendingMachineAPI.Migrations
{
    public partial class addedproducttypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "Id", "Cost", "Type" },
                values: new object[] { 1, 0.95m, "Soda" });

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "Id", "Cost", "Type" },
                values: new object[] { 2, 0.60m, "Candy Bar" });

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "Id", "Cost", "Type" },
                values: new object[] { 3, 0.99m, "Chips" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
