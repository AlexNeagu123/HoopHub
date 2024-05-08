using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HoopHub.Modules.UserFeatures.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ThreadVote : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DownVotes",
                schema: "user_features",
                table: "team_threads",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UpVotes",
                schema: "user_features",
                table: "team_threads",
                type: "integer",
                nullable: false,
                defaultValue: 0);

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "team_thread_votes",
                schema: "user_features");

            migrationBuilder.DropColumn(
                name: "DownVotes",
                schema: "user_features",
                table: "team_threads");

            migrationBuilder.DropColumn(
                name: "UpVotes",
                schema: "user_features",
                table: "team_threads");
        }
    }
}
