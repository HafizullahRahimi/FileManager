using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FileManager.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addBlogPostsTbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FileType",
                table: "Files",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(13)",
                oldMaxLength: 13);

            migrationBuilder.AddColumn<Guid>(
                name: "BlogPostId",
                table: "Files",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Files",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BlogPosts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogPosts", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Files_BlogPostId",
                table: "Files",
                column: "BlogPostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_BlogPosts_BlogPostId",
                table: "Files",
                column: "BlogPostId",
                principalTable: "BlogPosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_BlogPosts_BlogPostId",
                table: "Files");

            migrationBuilder.DropTable(
                name: "BlogPosts");

            migrationBuilder.DropIndex(
                name: "IX_Files_BlogPostId",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "BlogPostId",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Files");

            migrationBuilder.AlterColumn<string>(
                name: "FileType",
                table: "Files",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(21)",
                oldMaxLength: 21);
        }
    }
}
