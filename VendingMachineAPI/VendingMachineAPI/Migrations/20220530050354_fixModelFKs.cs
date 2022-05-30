using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VendingMachineAPI.Migrations
{
    public partial class fixModelFKs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Transactions_TransactionId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_CreditCardsVerification_CCVerificationId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_CCVerificationId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "CCVerificationId",
                table: "Transactions");

            migrationBuilder.AddColumn<int>(
                name: "CreditCardVerificationId",
                table: "Transactions",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TransactionId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CreditCardVerificationId",
                table: "Transactions",
                column: "CreditCardVerificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Transactions_TransactionId",
                table: "Products",
                column: "TransactionId",
                principalTable: "Transactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_CreditCardsVerification_CreditCardVerificationId",
                table: "Transactions",
                column: "CreditCardVerificationId",
                principalTable: "CreditCardsVerification",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Transactions_TransactionId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_CreditCardsVerification_CreditCardVerificationId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_CreditCardVerificationId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "CreditCardVerificationId",
                table: "Transactions");

            migrationBuilder.AddColumn<int>(
                name: "CCVerificationId",
                table: "Transactions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "TransactionId",
                table: "Products",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CCVerificationId",
                table: "Transactions",
                column: "CCVerificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Transactions_TransactionId",
                table: "Products",
                column: "TransactionId",
                principalTable: "Transactions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_CreditCardsVerification_CCVerificationId",
                table: "Transactions",
                column: "CCVerificationId",
                principalTable: "CreditCardsVerification",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
