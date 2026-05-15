using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Receipt.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddProductId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                schema: "receipt",
                table: "receipt_lines",
                type: "uuid",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductId",
                schema: "receipt",
                table: "receipt_lines");
        }
    }
}
