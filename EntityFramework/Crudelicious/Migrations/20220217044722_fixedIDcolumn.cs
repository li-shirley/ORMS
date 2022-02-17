using Microsoft.EntityFrameworkCore.Migrations;

namespace Crudelicious.Migrations
{
    public partial class fixedIDcolumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PostId",
                table: "Dishes",
                newName: "DishId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DishId",
                table: "Dishes",
                newName: "PostId");
        }
    }
}
