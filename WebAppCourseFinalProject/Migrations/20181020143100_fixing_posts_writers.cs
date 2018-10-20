using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAppCourseFinalProject.Migrations
{
    public partial class fixing_posts_writers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Post_Writer_WriterId",
                table: "Post");

            migrationBuilder.AlterColumn<int>(
                name: "WriterId",
                table: "Post",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Post",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Post",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Post_Writer_WriterId",
                table: "Post",
                column: "WriterId",
                principalTable: "Writer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Post_Writer_WriterId",
                table: "Post");

            migrationBuilder.AlterColumn<int>(
                name: "WriterId",
                table: "Post",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Post",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Post",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddForeignKey(
                name: "FK_Post_Writer_WriterId",
                table: "Post",
                column: "WriterId",
                principalTable: "Writer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
