using Microsoft.EntityFrameworkCore.Migrations;

namespace Catman.Blogger.Data.Migrations
{
    public partial class AddUserAvatar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "avatar",
                table: "users",
                maxLength: 2084,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "avatar",
                table: "users");
        }
    }
}
