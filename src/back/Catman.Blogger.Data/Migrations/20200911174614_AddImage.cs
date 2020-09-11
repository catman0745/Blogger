using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Catman.Blogger.Data.Migrations
{
    public partial class AddImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "images",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    file_name = table.Column<string>(maxLength: 250, nullable: false),
                    content_type = table.Column<string>(maxLength: 127, nullable: false),
                    owner = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_images", x => x.id);
                    table.ForeignKey(
                        name: "FK_images_users_owner",
                        column: x => x.owner,
                        principalTable: "users",
                        principalColumn: "username",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_images_file_name",
                table: "images",
                column: "file_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_images_owner",
                table: "images",
                column: "owner");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "images");
        }
    }
}
