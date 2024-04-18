using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HoopHub.Modules.UserFeatures.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class OneMore : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "user_features");

            migrationBuilder.CreateTable(
                name: "fans",
                schema: "user_features",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    AvatarPhotoUrl = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "game_threads",
                schema: "user_features",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    HomeTeamId = table.Column<Guid>(type: "uuid", nullable: false),
                    VisitorTeamId = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<string>(type: "text", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_game_threads", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "team_threads",
                schema: "user_features",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TeamId = table.Column<Guid>(type: "uuid", nullable: false),
                    FanId = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_team_threads", x => x.Id);
                    table.ForeignKey(
                        name: "FK_team_threads_fans_FanId",
                        column: x => x.FanId,
                        principalSchema: "user_features",
                        principalTable: "fans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "comments",
                schema: "user_features",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ParentId = table.Column<Guid>(type: "uuid", nullable: true),
                    Content = table.Column<string>(type: "text", nullable: false),
                    TeamThreadId = table.Column<Guid>(type: "uuid", nullable: true),
                    GameThreadId = table.Column<Guid>(type: "uuid", nullable: true),
                    FanId = table.Column<string>(type: "text", nullable: false),
                    UpVotes = table.Column<int>(type: "integer", nullable: false),
                    DownVotes = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_comments_fans_FanId",
                        column: x => x.FanId,
                        principalSchema: "user_features",
                        principalTable: "fans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_comments_game_threads_GameThreadId",
                        column: x => x.GameThreadId,
                        principalSchema: "user_features",
                        principalTable: "game_threads",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_comments_team_threads_TeamThreadId",
                        column: x => x.TeamThreadId,
                        principalSchema: "user_features",
                        principalTable: "team_threads",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "comment_votes",
                schema: "user_features",
                columns: table => new
                {
                    CommentId = table.Column<Guid>(type: "uuid", nullable: false),
                    FanId = table.Column<string>(type: "text", nullable: false),
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IsUpVote = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_comment_votes", x => new { x.CommentId, x.FanId });
                    table.ForeignKey(
                        name: "FK_comment_votes_comments_CommentId",
                        column: x => x.CommentId,
                        principalSchema: "user_features",
                        principalTable: "comments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_comment_votes_fans_FanId",
                        column: x => x.FanId,
                        principalSchema: "user_features",
                        principalTable: "fans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_comment_votes_FanId",
                schema: "user_features",
                table: "comment_votes",
                column: "FanId");

            migrationBuilder.CreateIndex(
                name: "IX_comments_FanId",
                schema: "user_features",
                table: "comments",
                column: "FanId");

            migrationBuilder.CreateIndex(
                name: "IX_comments_GameThreadId",
                schema: "user_features",
                table: "comments",
                column: "GameThreadId");

            migrationBuilder.CreateIndex(
                name: "IX_comments_TeamThreadId",
                schema: "user_features",
                table: "comments",
                column: "TeamThreadId");

            migrationBuilder.CreateIndex(
                name: "IX_team_threads_FanId",
                schema: "user_features",
                table: "team_threads",
                column: "FanId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "comment_votes",
                schema: "user_features");

            migrationBuilder.DropTable(
                name: "comments",
                schema: "user_features");

            migrationBuilder.DropTable(
                name: "game_threads",
                schema: "user_features");

            migrationBuilder.DropTable(
                name: "team_threads",
                schema: "user_features");

            migrationBuilder.DropTable(
                name: "fans",
                schema: "user_features");
        }
    }
}
