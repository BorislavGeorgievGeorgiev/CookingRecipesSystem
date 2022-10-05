using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CookingRecipesSystem.Infrastructure.Migrations
{
    public partial class RecipeTaskSetPositionUnique_Remove : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RecipeTasks_Position",
                table: "RecipeTasks");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_RecipeTasks_Position",
                table: "RecipeTasks",
                column: "Position",
                unique: true);
        }
    }
}
