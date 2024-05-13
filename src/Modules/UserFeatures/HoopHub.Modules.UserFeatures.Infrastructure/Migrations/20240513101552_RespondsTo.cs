using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HoopHub.Modules.UserFeatures.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RespondsTo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RespondsToId",
                schema: "user_features",
                table: "comments",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_comments_RespondsToId",
                schema: "user_features",
                table: "comments",
                column: "RespondsToId");

            migrationBuilder.AddForeignKey(
                name: "FK_comments_fans_RespondsToId",
                schema: "user_features",
                table: "comments",
                column: "RespondsToId",
                principalSchema: "user_features",
                principalTable: "fans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_comments_fans_RespondsToId",
                schema: "user_features",
                table: "comments");

            migrationBuilder.DropIndex(
                name: "IX_comments_RespondsToId",
                schema: "user_features",
                table: "comments");

            migrationBuilder.DropColumn(
                name: "RespondsToId",
                schema: "user_features",
                table: "comments");
        }
    }
}
