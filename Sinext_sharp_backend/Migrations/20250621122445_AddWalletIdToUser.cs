﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sinext_sharp_backend.Migrations
{
    /// <inheritdoc />
    public partial class AddWalletIdToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wallets_Users_Id",
                table: "Wallets");

            migrationBuilder.AddColumn<Guid>(
                name: "WalletId",
                table: "Users",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Users_WalletId",
                table: "Users",
                column: "WalletId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Wallets_WalletId",
                table: "Users",
                column: "WalletId",
                principalTable: "Wallets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Wallets_WalletId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_WalletId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "WalletId",
                table: "Users");

            migrationBuilder.AddForeignKey(
                name: "FK_Wallets_Users_Id",
                table: "Wallets",
                column: "Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
