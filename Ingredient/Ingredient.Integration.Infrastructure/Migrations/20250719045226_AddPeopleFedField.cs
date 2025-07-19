using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ingredient.Integration.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPeopleFedField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PeopleFed",
                table: "Recipes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PeopleFed",
                table: "Recipes");
        }
    }
}
