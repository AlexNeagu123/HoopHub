using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HoopHub.Modules.UserFeatures.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SoftDeleteUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedOnUtc",
                schema: "user_features",
                table: "comment_votes");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "user_features",
                table: "comment_votes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOnUtc",
                schema: "user_features",
                table: "comment_votes",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "user_features",
                table: "comment_votes",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
