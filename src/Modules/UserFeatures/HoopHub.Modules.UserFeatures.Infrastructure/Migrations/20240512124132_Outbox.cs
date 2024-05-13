using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HoopHub.Modules.UserFeatures.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Outbox : Migration
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
                    UpVotes = table.Column<int>(type: "integer", nullable: false),
                    DownVotes = table.Column<int>(type: "integer", nullable: false),
                    FanBadge = table.Column<int>(type: "integer", nullable: false),
                    AvatarPhotoUrl = table.Column<string>(type: "text", nullable: true),
                    FavouriteTeamId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
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
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CommentsCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_game_threads", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OutboxMessages",
                schema: "user_features",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    OccuredOnUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ProcessedOnUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Error = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutboxMessages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "game_reviews",
                schema: "user_features",
                columns: table => new
                {
                    HomeTeamId = table.Column<Guid>(type: "uuid", nullable: false),
                    VisitorTeamId = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<string>(type: "text", nullable: false),
                    FanId = table.Column<string>(type: "text", nullable: false),
                    Rating = table.Column<decimal>(type: "numeric", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_game_reviews", x => new { x.HomeTeamId, x.VisitorTeamId, x.Date, x.FanId });
                    table.ForeignKey(
                        name: "FK_game_reviews_fans_FanId",
                        column: x => x.FanId,
                        principalSchema: "user_features",
                        principalTable: "fans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "notifications",
                schema: "user_features",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RecipientId = table.Column<string>(type: "text", nullable: false),
                    SenderId = table.Column<string>(type: "text", nullable: true),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    IsRead = table.Column<bool>(type: "boolean", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    AttachedImageUrl = table.Column<string>(type: "text", nullable: true),
                    AttachedNavigationData = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_notifications_fans_RecipientId",
                        column: x => x.RecipientId,
                        principalSchema: "user_features",
                        principalTable: "fans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_notifications_fans_SenderId",
                        column: x => x.SenderId,
                        principalSchema: "user_features",
                        principalTable: "fans",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "player_follow_entries",
                schema: "user_features",
                columns: table => new
                {
                    FanId = table.Column<string>(type: "text", nullable: false),
                    PlayerId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_player_follow_entries", x => new { x.FanId, x.PlayerId });
                    table.ForeignKey(
                        name: "FK_player_follow_entries_fans_FanId",
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
                    PlayerId = table.Column<Guid>(type: "uuid", nullable: false),
                    Rating = table.Column<decimal>(type: "numeric", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_player_performance_reviews", x => new { x.HomeTeamId, x.VisitorTeamId, x.PlayerId, x.Date, x.FanId });
                    table.ForeignKey(
                        name: "FK_player_performance_reviews_fans_FanId",
                        column: x => x.FanId,
                        principalSchema: "user_features",
                        principalTable: "fans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "team_follow_entries",
                schema: "user_features",
                columns: table => new
                {
                    FanId = table.Column<string>(type: "text", nullable: false),
                    TeamId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_team_follow_entries", x => new { x.FanId, x.TeamId });
                    table.ForeignKey(
                        name: "FK_team_follow_entries_fans_FanId",
                        column: x => x.FanId,
                        principalSchema: "user_features",
                        principalTable: "fans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    UpVotes = table.Column<int>(type: "integer", nullable: false),
                    DownVotes = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CommentsCount = table.Column<int>(type: "integer", nullable: false)
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
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
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
                name: "team_thread_votes",
                schema: "user_features",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TeamThreadId = table.Column<Guid>(type: "uuid", nullable: false),
                    FanId = table.Column<string>(type: "text", nullable: false),
                    IsUpVote = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_team_thread_votes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_team_thread_votes_fans_FanId",
                        column: x => x.FanId,
                        principalSchema: "user_features",
                        principalTable: "fans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_team_thread_votes_team_threads_TeamThreadId",
                        column: x => x.TeamThreadId,
                        principalSchema: "user_features",
                        principalTable: "team_threads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "comment_votes",
                schema: "user_features",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CommentId = table.Column<Guid>(type: "uuid", nullable: false),
                    FanId = table.Column<string>(type: "text", nullable: false),
                    IsUpVote = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_comment_votes", x => x.Id);
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
                name: "IX_comment_votes_CommentId",
                schema: "user_features",
                table: "comment_votes",
                column: "CommentId");

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
                name: "IX_game_reviews_FanId",
                schema: "user_features",
                table: "game_reviews",
                column: "FanId");

            migrationBuilder.CreateIndex(
                name: "IX_notifications_RecipientId",
                schema: "user_features",
                table: "notifications",
                column: "RecipientId");

            migrationBuilder.CreateIndex(
                name: "IX_notifications_SenderId",
                schema: "user_features",
                table: "notifications",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_player_performance_reviews_FanId",
                schema: "user_features",
                table: "player_performance_reviews",
                column: "FanId");

            migrationBuilder.CreateIndex(
                name: "IX_team_thread_votes_FanId",
                schema: "user_features",
                table: "team_thread_votes",
                column: "FanId");

            migrationBuilder.CreateIndex(
                name: "IX_team_thread_votes_TeamThreadId",
                schema: "user_features",
                table: "team_thread_votes",
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
                name: "game_reviews",
                schema: "user_features");

            migrationBuilder.DropTable(
                name: "notifications",
                schema: "user_features");

            migrationBuilder.DropTable(
                name: "OutboxMessages",
                schema: "user_features");

            migrationBuilder.DropTable(
                name: "player_follow_entries",
                schema: "user_features");

            migrationBuilder.DropTable(
                name: "player_performance_reviews",
                schema: "user_features");

            migrationBuilder.DropTable(
                name: "team_follow_entries",
                schema: "user_features");

            migrationBuilder.DropTable(
                name: "team_thread_votes",
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
