using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sinext_sharp_backend.Migrations
{
    /// <inheritdoc />
    public partial class AddGuaranteedIncomeAndStock : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GuaranteedIncome_Wallets_WalletId",
                table: "GuaranteedIncome");

            migrationBuilder.DropForeignKey(
                name: "FK_Stock_Wallets_WalletId",
                table: "Stock");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Stock",
                table: "Stock");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GuaranteedIncome",
                table: "GuaranteedIncome");

            migrationBuilder.RenameTable(
                name: "Stock",
                newName: "Stocks");

            migrationBuilder.RenameTable(
                name: "GuaranteedIncome",
                newName: "GuaranteedIncomes");

            migrationBuilder.RenameIndex(
                name: "IX_Stock_WalletId",
                table: "Stocks",
                newName: "IX_Stocks_WalletId");

            migrationBuilder.RenameIndex(
                name: "IX_GuaranteedIncome_WalletId",
                table: "GuaranteedIncomes",
                newName: "IX_GuaranteedIncomes_WalletId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stocks",
                table: "Stocks",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GuaranteedIncomes",
                table: "GuaranteedIncomes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GuaranteedIncomes_Wallets_WalletId",
                table: "GuaranteedIncomes",
                column: "WalletId",
                principalTable: "Wallets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Wallets_WalletId",
                table: "Stocks",
                column: "WalletId",
                principalTable: "Wallets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GuaranteedIncomes_Wallets_WalletId",
                table: "GuaranteedIncomes");

            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Wallets_WalletId",
                table: "Stocks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Stocks",
                table: "Stocks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GuaranteedIncomes",
                table: "GuaranteedIncomes");

            migrationBuilder.RenameTable(
                name: "Stocks",
                newName: "Stock");

            migrationBuilder.RenameTable(
                name: "GuaranteedIncomes",
                newName: "GuaranteedIncome");

            migrationBuilder.RenameIndex(
                name: "IX_Stocks_WalletId",
                table: "Stock",
                newName: "IX_Stock_WalletId");

            migrationBuilder.RenameIndex(
                name: "IX_GuaranteedIncomes_WalletId",
                table: "GuaranteedIncome",
                newName: "IX_GuaranteedIncome_WalletId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stock",
                table: "Stock",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GuaranteedIncome",
                table: "GuaranteedIncome",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GuaranteedIncome_Wallets_WalletId",
                table: "GuaranteedIncome",
                column: "WalletId",
                principalTable: "Wallets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stock_Wallets_WalletId",
                table: "Stock",
                column: "WalletId",
                principalTable: "Wallets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
