using Microsoft.EntityFrameworkCore.Migrations;

namespace Catman.Blogger.Data.Migrations
{
    public partial class AddPostViews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "views",
                table: "posts",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "views",
                table: "posts");
        }
    }
}
