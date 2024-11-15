using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace task2API.Migrations
{
    /// <inheritdoc />
    public partial class editPrice2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "price",
                table: "Products",
                newName: "Price");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Products",
                newName: "price");
        }
    }
}
