using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server_Things.Migrations
{
    /// <inheritdoc />
    public partial class UniqueConstraintsUserEmailandCompanyName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "User",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Companies_Name",
                table: "Companies",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Email",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_Companies_Name",
                table: "Companies");
        }
    }
}
