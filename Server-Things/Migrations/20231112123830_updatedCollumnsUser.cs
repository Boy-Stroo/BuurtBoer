using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server_Things.Migrations
{
    /// <inheritdoc />
    public partial class updatedCollumnsUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DaysAtOfficeId",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DaysAtOfficeId",
                table: "Users",
                type: "uuid",
                nullable: true);
        }
    }
}
