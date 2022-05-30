using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VendingMachineAPI.Migrations
{
    public partial class entireTransactionRefundable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefundRequested",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "RefundDate",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "InsertDate",
                table: "Transactions",
                newName: "RefundDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "PurchaseDate",
                table: "Transactions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PurchaseDate",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "RefundDate",
                table: "Transactions",
                newName: "InsertDate");

            migrationBuilder.AddColumn<bool>(
                name: "RefundRequested",
                table: "Transactions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefundDate",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
