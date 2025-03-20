﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DLL.Migrations
{
    /// <inheritdoc />
    public partial class ProductClicksOnProductCascadeDeleteAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductClicks_Products_ProductId",
                table: "ProductClicks");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductClicks_Products_ProductId",
                table: "ProductClicks",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductClicks_Products_ProductId",
                table: "ProductClicks");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductClicks_Products_ProductId",
                table: "ProductClicks",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
