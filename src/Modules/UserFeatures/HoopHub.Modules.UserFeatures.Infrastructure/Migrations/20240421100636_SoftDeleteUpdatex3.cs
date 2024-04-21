using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HoopHub.Modules.UserFeatures.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SoftDeleteUpdatex3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_comment_votes",
                schema: "user_features",
                table: "comment_votes");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                schema: "user_features",
                table: "comment_votes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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

            migrationBuilder.AddPrimaryKey(
                name: "PK_comment_votes",
                schema: "user_features",
                table: "comment_votes",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_comment_votes_CommentId",
                schema: "user_features",
                table: "comment_votes",
                column: "CommentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_comment_votes",
                schema: "user_features",
                table: "comment_votes");

            migrationBuilder.DropIndex(
                name: "IX_comment_votes_CommentId",
                schema: "user_features",
                table: "comment_votes");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "user_features",
                table: "comment_votes");

            migrationBuilder.DropColumn(
                name: "DeletedOnUtc",
                schema: "user_features",
                table: "comment_votes");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "user_features",
                table: "comment_votes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_comment_votes",
                schema: "user_features",
                table: "comment_votes",
                columns: new[] { "CommentId", "FanId" });
        }
    }
}
