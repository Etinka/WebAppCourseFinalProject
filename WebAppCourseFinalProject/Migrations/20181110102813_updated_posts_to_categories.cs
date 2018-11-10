using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAppCourseFinalProject.Migrations
{
    public partial class updated_posts_to_categories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_Post_PostId",
                table: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Category_PostId",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "PostId",
                table: "Category");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PostId",
                table: "Category",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Category_PostId",
                table: "Category",
                column: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_Post_PostId",
                table: "Category",
                column: "PostId",
                principalTable: "Post",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
