using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FileManager.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updateBlogPostsTbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Files_BlogPostId",
                table: "Files");

            migrationBuilder.CreateIndex(
                name: "IX_Files_BlogPostId",
                table: "Files",
                column: "BlogPostId",
                unique: true,
                filter: "[BlogPostId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Files_BlogPostId",
                table: "Files");

            migrationBuilder.CreateIndex(
                name: "IX_Files_BlogPostId",
                table: "Files",
                column: "BlogPostId");
        }
    }
}
