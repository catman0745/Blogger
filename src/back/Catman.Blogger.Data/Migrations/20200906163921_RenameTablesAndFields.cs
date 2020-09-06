namespace Catman.Blogger.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class RenameTablesAndFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_Users_OwnerUsername",
                table: "Blogs");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Blogs_BlogId",
                table: "Posts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Posts",
                table: "Posts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Blogs",
                table: "Blogs");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "users");

            migrationBuilder.RenameTable(
                name: "Posts",
                newName: "posts");

            migrationBuilder.RenameTable(
                name: "Blogs",
                newName: "blogs");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "users",
                newName: "password");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "users",
                newName: "username");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "users",
                newName: "full_name");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "posts",
                newName: "title");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "posts",
                newName: "content");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "posts",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "LastUpdate",
                table: "posts",
                newName: "last_update");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "posts",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "BlogId",
                table: "posts",
                newName: "blog_id");

            migrationBuilder.RenameIndex(
                name: "IX_Posts_Title_BlogId",
                table: "posts",
                newName: "IX_posts_title_blog_id");

            migrationBuilder.RenameIndex(
                name: "IX_Posts_BlogId",
                table: "posts",
                newName: "IX_posts_blog_id");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "blogs",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "blogs",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "OwnerUsername",
                table: "blogs",
                newName: "owner");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "blogs",
                newName: "created_at");

            migrationBuilder.RenameIndex(
                name: "IX_Blogs_Name",
                table: "blogs",
                newName: "IX_blogs_name");

            migrationBuilder.RenameIndex(
                name: "IX_Blogs_OwnerUsername",
                table: "blogs",
                newName: "IX_blogs_owner");

            migrationBuilder.AddPrimaryKey(
                name: "PK_users",
                table: "users",
                column: "username");

            migrationBuilder.AddPrimaryKey(
                name: "PK_posts",
                table: "posts",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_blogs",
                table: "blogs",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_blogs_users_owner",
                table: "blogs",
                column: "owner",
                principalTable: "users",
                principalColumn: "username",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_posts_blogs_blog_id",
                table: "posts",
                column: "blog_id",
                principalTable: "blogs",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_blogs_users_owner",
                table: "blogs");

            migrationBuilder.DropForeignKey(
                name: "FK_posts_blogs_blog_id",
                table: "posts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_users",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_posts",
                table: "posts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_blogs",
                table: "blogs");

            migrationBuilder.RenameTable(
                name: "users",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "posts",
                newName: "Posts");

            migrationBuilder.RenameTable(
                name: "blogs",
                newName: "Blogs");

            migrationBuilder.RenameColumn(
                name: "password",
                table: "Users",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "username",
                table: "Users",
                newName: "Username");

            migrationBuilder.RenameColumn(
                name: "full_name",
                table: "Users",
                newName: "FullName");

            migrationBuilder.RenameColumn(
                name: "title",
                table: "Posts",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "content",
                table: "Posts",
                newName: "Content");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Posts",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "last_update",
                table: "Posts",
                newName: "LastUpdate");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Posts",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "blog_id",
                table: "Posts",
                newName: "BlogId");

            migrationBuilder.RenameIndex(
                name: "IX_posts_title_blog_id",
                table: "Posts",
                newName: "IX_Posts_Title_BlogId");

            migrationBuilder.RenameIndex(
                name: "IX_posts_blog_id",
                table: "Posts",
                newName: "IX_Posts_BlogId");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Blogs",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Blogs",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "owner",
                table: "Blogs",
                newName: "OwnerUsername");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Blogs",
                newName: "CreatedAt");

            migrationBuilder.RenameIndex(
                name: "IX_blogs_name",
                table: "Blogs",
                newName: "IX_Blogs_Name");

            migrationBuilder.RenameIndex(
                name: "IX_blogs_owner",
                table: "Blogs",
                newName: "IX_Blogs_OwnerUsername");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Username");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Posts",
                table: "Posts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Blogs",
                table: "Blogs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_Users_OwnerUsername",
                table: "Blogs",
                column: "OwnerUsername",
                principalTable: "Users",
                principalColumn: "Username",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Blogs_BlogId",
                table: "Posts",
                column: "BlogId",
                principalTable: "Blogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
