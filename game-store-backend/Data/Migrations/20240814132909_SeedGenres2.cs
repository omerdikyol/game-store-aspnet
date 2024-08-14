using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace game_store.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedGenres2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Decimal",
                table: "Games",
                newName: "Price");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Games",
                newName: "Decimal");
        }
    }
}
