using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HoopHub.Modules.UserFeatures.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AuditableChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "user_features",
                table: "team_threads");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                schema: "user_features",
                table: "team_threads");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "user_features",
                table: "team_follow_entries");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                schema: "user_features",
                table: "team_follow_entries");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "user_features",
                table: "player_performance_reviews");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                schema: "user_features",
                table: "player_performance_reviews");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "user_features",
                table: "player_follow_entries");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                schema: "user_features",
                table: "player_follow_entries");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "user_features",
                table: "notifications");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                schema: "user_features",
                table: "notifications");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "user_features",
                table: "game_threads");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                schema: "user_features",
                table: "game_threads");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "user_features",
                table: "game_reviews");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                schema: "user_features",
                table: "game_reviews");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "user_features",
                table: "fans");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                schema: "user_features",
                table: "fans");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "user_features",
                table: "comments");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                schema: "user_features",
                table: "comments");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "user_features",
                table: "comment_votes");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                schema: "user_features",
                table: "comment_votes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "user_features",
                table: "team_threads",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                schema: "user_features",
                table: "team_threads",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "user_features",
                table: "team_follow_entries",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                schema: "user_features",
                table: "team_follow_entries",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "user_features",
                table: "player_performance_reviews",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                schema: "user_features",
                table: "player_performance_reviews",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "user_features",
                table: "player_follow_entries",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                schema: "user_features",
                table: "player_follow_entries",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "user_features",
                table: "notifications",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                schema: "user_features",
                table: "notifications",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "user_features",
                table: "game_threads",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                schema: "user_features",
                table: "game_threads",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "user_features",
                table: "game_reviews",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                schema: "user_features",
                table: "game_reviews",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "user_features",
                table: "fans",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                schema: "user_features",
                table: "fans",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "user_features",
                table: "comments",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                schema: "user_features",
                table: "comments",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "user_features",
                table: "comment_votes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                schema: "user_features",
                table: "comment_votes",
                type: "text",
                nullable: true);
        }
    }
}
