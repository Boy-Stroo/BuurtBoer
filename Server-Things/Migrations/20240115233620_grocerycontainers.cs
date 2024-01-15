using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server_Things.Migrations
{
    /// <inheritdoc />
    public partial class grocerycontainers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Container",
                table: "GroceryList",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Container",
                table: "GroceryList");
        }
    }
}
