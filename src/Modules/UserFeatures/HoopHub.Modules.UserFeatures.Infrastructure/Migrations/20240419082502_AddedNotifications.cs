using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HoopHub.Modules.UserFeatures.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedNotifications : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FavouriteTeamId",
                schema: "user_features",
                table: "fans",
                type: "uuid",
                nullable: true);

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
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
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
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
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
                name: "team_follow_entries",
                schema: "user_features",
                columns: table => new
                {
                    FanId = table.Column<string>(type: "text", nullable: false),
                    TeamId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true),
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "notifications",
                schema: "user_features");

            migrationBuilder.DropTable(
                name: "player_follow_entries",
                schema: "user_features");

            migrationBuilder.DropTable(
                name: "team_follow_entries",
                schema: "user_features");

            migrationBuilder.DropColumn(
                name: "FavouriteTeamId",
                schema: "user_features",
                table: "fans");
        }
    }
}
