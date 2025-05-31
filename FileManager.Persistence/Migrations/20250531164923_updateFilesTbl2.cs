using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FileManager.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updateFilesTbl2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UploadedBy",
                table: "Files",
                newName: "ModifiedBy");

            migrationBuilder.RenameColumn(
                name: "UploadedAt",
                table: "Files",
                newName: "ModifiedUtcDate");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Files",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedUtcDate",
                table: "Files",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Files",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "CreatedUtcDate",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Files");

            migrationBuilder.RenameColumn(
                name: "ModifiedUtcDate",
                table: "Files",
                newName: "UploadedAt");

            migrationBuilder.RenameColumn(
                name: "ModifiedBy",
                table: "Files",
                newName: "UploadedBy");
        }
    }
}
