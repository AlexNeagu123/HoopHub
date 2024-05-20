using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HoopHub.Modules.NBAData.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedPlayerTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Players",
                table: "Players");

            migrationBuilder.EnsureSchema(
                name: "nba_data");

            migrationBuilder.RenameTable(
                name: "Players",
                newName: "players",
                newSchema: "nba_data");

            migrationBuilder.RenameColumn(
                name: "LastName",
                schema: "nba_data",
                table: "players",
                newName: "last_name");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                schema: "nba_data",
                table: "players",
                newName: "first_name");

            migrationBuilder.RenameColumn(
                name: "PlayerId",
                schema: "nba_data",
                table: "players",
                newName: "id");

            migrationBuilder.AddColumn<int>(
                name: "api_id",
                schema: "nba_data",
                table: "players",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "average_rating",
                schema: "nba_data",
                table: "players",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "college",
                schema: "nba_data",
                table: "players",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "country",
                schema: "nba_data",
                table: "players",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "current_team_id",
                schema: "nba_data",
                table: "players",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "draft_number",
                schema: "nba_data",
                table: "players",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "draft_round",
                schema: "nba_data",
                table: "players",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "draft_year",
                schema: "nba_data",
                table: "players",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "height",
                schema: "nba_data",
                table: "players",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "image_url",
                schema: "nba_data",
                table: "players",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_active",
                schema: "nba_data",
                table: "players",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "jersey_number",
                schema: "nba_data",
                table: "players",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "position",
                schema: "nba_data",
                table: "players",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "weight",
                schema: "nba_data",
                table: "players",
                type: "text",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_players",
                schema: "nba_data",
                table: "players",
                column: "id");

            migrationBuilder.CreateTable(
                name: "seasons",
                schema: "nba_data",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    season = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_seasons", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "teams",
                schema: "nba_data",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    api_id = table.Column<int>(type: "integer", nullable: false),
                    full_name = table.Column<string>(type: "text", nullable: false),
                    abbreviation = table.Column<string>(type: "text", nullable: false),
                    city = table.Column<string>(type: "text", nullable: true),
                    conference = table.Column<string>(type: "text", nullable: true),
                    division = table.Column<string>(type: "text", nullable: true),
                    image_url = table.Column<string>(type: "text", nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_teams", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "player_team_season",
                schema: "nba_data",
                columns: table => new
                {
                    player_id = table.Column<Guid>(type: "uuid", nullable: false),
                    team_id = table.Column<Guid>(type: "uuid", nullable: false),
                    season_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_player_team_season", x => new { x.player_id, x.team_id, x.season_id });
                    table.ForeignKey(
                        name: "FK_player_team_season_players_player_id",
                        column: x => x.player_id,
                        principalSchema: "nba_data",
                        principalTable: "players",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_player_team_season_seasons_season_id",
                        column: x => x.season_id,
                        principalSchema: "nba_data",
                        principalTable: "seasons",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_player_team_season_teams_team_id",
                        column: x => x.team_id,
                        principalSchema: "nba_data",
                        principalTable: "teams",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "playoff_series",
                schema: "nba_data",
                columns: table => new
                {
                    season_id = table.Column<Guid>(type: "uuid", nullable: false),
                    winning_team_id = table.Column<Guid>(type: "uuid", nullable: false),
                    losing_team_id = table.Column<Guid>(type: "uuid", nullable: false),
                    winning_team_wins = table.Column<int>(type: "integer", nullable: false),
                    losing_team_wins = table.Column<int>(type: "integer", nullable: false),
                    stage = table.Column<string>(type: "text", nullable: false),
                    winning_team_rank = table.Column<int>(type: "integer", nullable: false),
                    losing_team_rank = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_playoff_series", x => new { x.season_id, x.winning_team_id, x.losing_team_id });
                    table.ForeignKey(
                        name: "FK_playoff_series_seasons_season_id",
                        column: x => x.season_id,
                        principalSchema: "nba_data",
                        principalTable: "seasons",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_playoff_series_teams_losing_team_id",
                        column: x => x.losing_team_id,
                        principalSchema: "nba_data",
                        principalTable: "teams",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_playoff_series_teams_winning_team_id",
                        column: x => x.winning_team_id,
                        principalSchema: "nba_data",
                        principalTable: "teams",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "standings",
                schema: "nba_data",
                columns: table => new
                {
                    season_id = table.Column<Guid>(type: "uuid", nullable: false),
                    team_id = table.Column<Guid>(type: "uuid", nullable: false),
                    rank = table.Column<int>(type: "integer", nullable: false),
                    overall = table.Column<string>(type: "text", nullable: false),
                    home = table.Column<string>(type: "text", nullable: false),
                    road = table.Column<string>(type: "text", nullable: false),
                    eastern_record = table.Column<string>(type: "text", nullable: false),
                    western_record = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_standings", x => new { x.season_id, x.team_id });
                    table.ForeignKey(
                        name: "FK_standings_seasons_season_id",
                        column: x => x.season_id,
                        principalSchema: "nba_data",
                        principalTable: "seasons",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_standings_teams_team_id",
                        column: x => x.team_id,
                        principalSchema: "nba_data",
                        principalTable: "teams",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "team_latest",
                schema: "nba_data",
                columns: table => new
                {
                    team_id = table.Column<Guid>(type: "uuid", nullable: false),
                    latest_index = table.Column<int>(type: "integer", nullable: false),
                    game_date = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_team_latest", x => new { x.team_id, x.latest_index });
                    table.ForeignKey(
                        name: "FK_team_latest_teams_team_id",
                        column: x => x.team_id,
                        principalSchema: "nba_data",
                        principalTable: "teams",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "teams_bio",
                schema: "nba_data",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    team_id = table.Column<Guid>(type: "uuid", nullable: false),
                    season_id = table.Column<Guid>(type: "uuid", nullable: false),
                    win_count = table.Column<int>(type: "integer", nullable: false),
                    loss_count = table.Column<int>(type: "integer", nullable: false),
                    win_loss_ratio = table.Column<double>(type: "double precision", nullable: false),
                    finish = table.Column<string>(type: "text", nullable: false),
                    srs = table.Column<double>(type: "double precision", nullable: false),
                    pace = table.Column<double>(type: "double precision", nullable: false),
                    rel_pace = table.Column<double>(type: "double precision", nullable: false),
                    o_rtg = table.Column<double>(type: "double precision", nullable: false),
                    d_rtg = table.Column<double>(type: "double precision", nullable: false),
                    playoffs = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_teams_bio", x => x.id);
                    table.ForeignKey(
                        name: "FK_teams_bio_seasons_season_id",
                        column: x => x.season_id,
                        principalSchema: "nba_data",
                        principalTable: "seasons",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_teams_bio_teams_team_id",
                        column: x => x.team_id,
                        principalSchema: "nba_data",
                        principalTable: "teams",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_players_current_team_id",
                schema: "nba_data",
                table: "players",
                column: "current_team_id");

            migrationBuilder.CreateIndex(
                name: "IX_player_team_season_season_id",
                schema: "nba_data",
                table: "player_team_season",
                column: "season_id");

            migrationBuilder.CreateIndex(
                name: "IX_player_team_season_team_id",
                schema: "nba_data",
                table: "player_team_season",
                column: "team_id");

            migrationBuilder.CreateIndex(
                name: "IX_playoff_series_losing_team_id",
                schema: "nba_data",
                table: "playoff_series",
                column: "losing_team_id");

            migrationBuilder.CreateIndex(
                name: "IX_playoff_series_winning_team_id",
                schema: "nba_data",
                table: "playoff_series",
                column: "winning_team_id");

            migrationBuilder.CreateIndex(
                name: "IX_standings_team_id",
                schema: "nba_data",
                table: "standings",
                column: "team_id");

            migrationBuilder.CreateIndex(
                name: "IX_teams_bio_season_id",
                schema: "nba_data",
                table: "teams_bio",
                column: "season_id");

            migrationBuilder.CreateIndex(
                name: "IX_teams_bio_team_id",
                schema: "nba_data",
                table: "teams_bio",
                column: "team_id");

            migrationBuilder.AddForeignKey(
                name: "FK_players_teams_current_team_id",
                schema: "nba_data",
                table: "players",
                column: "current_team_id",
                principalSchema: "nba_data",
                principalTable: "teams",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_players_teams_current_team_id",
                schema: "nba_data",
                table: "players");

            migrationBuilder.DropTable(
                name: "player_team_season",
                schema: "nba_data");

            migrationBuilder.DropTable(
                name: "playoff_series",
                schema: "nba_data");

            migrationBuilder.DropTable(
                name: "standings",
                schema: "nba_data");

            migrationBuilder.DropTable(
                name: "team_latest",
                schema: "nba_data");

            migrationBuilder.DropTable(
                name: "teams_bio",
                schema: "nba_data");

            migrationBuilder.DropTable(
                name: "seasons",
                schema: "nba_data");

            migrationBuilder.DropTable(
                name: "teams",
                schema: "nba_data");

            migrationBuilder.DropPrimaryKey(
                name: "PK_players",
                schema: "nba_data",
                table: "players");

            migrationBuilder.DropIndex(
                name: "IX_players_current_team_id",
                schema: "nba_data",
                table: "players");

            migrationBuilder.DropColumn(
                name: "api_id",
                schema: "nba_data",
                table: "players");

            migrationBuilder.DropColumn(
                name: "average_rating",
                schema: "nba_data",
                table: "players");

            migrationBuilder.DropColumn(
                name: "college",
                schema: "nba_data",
                table: "players");

            migrationBuilder.DropColumn(
                name: "country",
                schema: "nba_data",
                table: "players");

            migrationBuilder.DropColumn(
                name: "current_team_id",
                schema: "nba_data",
                table: "players");

            migrationBuilder.DropColumn(
                name: "draft_number",
                schema: "nba_data",
                table: "players");

            migrationBuilder.DropColumn(
                name: "draft_round",
                schema: "nba_data",
                table: "players");

            migrationBuilder.DropColumn(
                name: "draft_year",
                schema: "nba_data",
                table: "players");

            migrationBuilder.DropColumn(
                name: "height",
                schema: "nba_data",
                table: "players");

            migrationBuilder.DropColumn(
                name: "image_url",
                schema: "nba_data",
                table: "players");

            migrationBuilder.DropColumn(
                name: "is_active",
                schema: "nba_data",
                table: "players");

            migrationBuilder.DropColumn(
                name: "jersey_number",
                schema: "nba_data",
                table: "players");

            migrationBuilder.DropColumn(
                name: "position",
                schema: "nba_data",
                table: "players");

            migrationBuilder.DropColumn(
                name: "weight",
                schema: "nba_data",
                table: "players");

            migrationBuilder.RenameTable(
                name: "players",
                schema: "nba_data",
                newName: "Players");

            migrationBuilder.RenameColumn(
                name: "last_name",
                table: "Players",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "first_name",
                table: "Players",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Players",
                newName: "PlayerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Players",
                table: "Players",
                column: "PlayerId");
        }
    }
}
