using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HoopHub.Modules.UserFeatures.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FanUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_player_performance_reviews",
                schema: "user_features",
                table: "player_performance_reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_game_reviews",
                schema: "user_features",
                table: "game_reviews");

            migrationBuilder.AddColumn<int>(
                name: "DownVotes",
                schema: "user_features",
                table: "fans",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FanBadge",
                schema: "user_features",
                table: "fans",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UpVotes",
                schema: "user_features",
                table: "fans",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_player_performance_reviews",
                schema: "user_features",
                table: "player_performance_reviews",
                columns: new[] { "HomeTeamId", "VisitorTeamId", "PlayerId", "Date", "FanId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_game_reviews",
                schema: "user_features",
                table: "game_reviews",
                columns: new[] { "HomeTeamId", "VisitorTeamId", "Date", "FanId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_player_performance_reviews",
                schema: "user_features",
                table: "player_performance_reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_game_reviews",
                schema: "user_features",
                table: "game_reviews");

            migrationBuilder.DropColumn(
                name: "DownVotes",
                schema: "user_features",
                table: "fans");

            migrationBuilder.DropColumn(
                name: "FanBadge",
                schema: "user_features",
                table: "fans");

            migrationBuilder.DropColumn(
                name: "UpVotes",
                schema: "user_features",
                table: "fans");

            migrationBuilder.AddPrimaryKey(
                name: "PK_player_performance_reviews",
                schema: "user_features",
                table: "player_performance_reviews",
                columns: new[] { "HomeTeamId", "VisitorTeamId", "Date" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_game_reviews",
                schema: "user_features",
                table: "game_reviews",
                columns: new[] { "HomeTeamId", "VisitorTeamId", "Date" });
        }
    }
}
