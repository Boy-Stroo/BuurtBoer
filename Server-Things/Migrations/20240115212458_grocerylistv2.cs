using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server_Things.Migrations
{
    /// <inheritdoc />
    public partial class grocerylistv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Groceries",
                table: "Groceries");

            migrationBuilder.RenameTable(
                name: "Groceries",
                newName: "GroceryList");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "GroceryList",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "Amount",
                table: "GroceryList",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyID",
                table: "GroceryList",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroceryList",
                table: "GroceryList",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_GroceryList_CompanyID",
                table: "GroceryList",
                column: "CompanyID");

            migrationBuilder.AddForeignKey(
                name: "FK_GroceryList_Companies_CompanyID",
                table: "GroceryList",
                column: "CompanyID",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroceryList_Companies_CompanyID",
                table: "GroceryList");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GroceryList",
                table: "GroceryList");

            migrationBuilder.DropIndex(
                name: "IX_GroceryList_CompanyID",
                table: "GroceryList");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "GroceryList");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "GroceryList");

            migrationBuilder.DropColumn(
                name: "CompanyID",
                table: "GroceryList");

            migrationBuilder.RenameTable(
                name: "GroceryList",
                newName: "Groceries");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Groceries",
                table: "Groceries",
                column: "Name");
        }
    }
}
