using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HoopHub.Modules.UserFeatures.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedReviews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "game_reviews",
                schema: "user_features",
                columns: table => new
                {
                    HomeTeamId = table.Column<Guid>(type: "uuid", nullable: false),
                    VisitorTeamId = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<string>(type: "text", nullable: false),
                    FanId = table.Column<string>(type: "text", nullable: false),
                    Rating = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_game_reviews", x => new { x.HomeTeamId, x.VisitorTeamId, x.Date });
                    table.ForeignKey(
                        name: "FK_game_reviews_fans_FanId",
                        column: x => x.FanId,
                        principalSchema: "user_features",
                        principalTable: "fans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "player_performance_reviews",
                schema: "user_features",
                columns: table => new
                {
                    HomeTeamId = table.Column<Guid>(type: "uuid", nullable: false),
                    VisitorTeamId = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<string>(type: "text", nullable: false),
                    FanId = table.Column<string>(type: "text", nullable: false),
                    Rating = table.Column<decimal>(type: "numeric", nullable: false),
                    PlayerId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_player_performance_reviews", x => new { x.HomeTeamId, x.VisitorTeamId, x.Date });
                    table.ForeignKey(
                        name: "FK_player_performance_reviews_fans_FanId",
                        column: x => x.FanId,
                        principalSchema: "user_features",
                        principalTable: "fans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_game_reviews_FanId",
                schema: "user_features",
                table: "game_reviews",
                column: "FanId");

            migrationBuilder.CreateIndex(
                name: "IX_player_performance_reviews_FanId",
                schema: "user_features",
                table: "player_performance_reviews",
                column: "FanId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "game_reviews",
                schema: "user_features");

            migrationBuilder.DropTable(
                name: "player_performance_reviews",
                schema: "user_features");
        }
    }
}
