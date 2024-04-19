using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HoopHub.Modules.UserFeatures.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AllAuditable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "user_features",
                table: "player_performance_reviews",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                schema: "user_features",
                table: "player_performance_reviews",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                schema: "user_features",
                table: "player_performance_reviews",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                schema: "user_features",
                table: "player_performance_reviews",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "user_features",
                table: "game_reviews",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                schema: "user_features",
                table: "game_reviews",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                schema: "user_features",
                table: "game_reviews",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                schema: "user_features",
                table: "game_reviews",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "user_features",
                table: "fans",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                schema: "user_features",
                table: "fans",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                schema: "user_features",
                table: "fans",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                schema: "user_features",
                table: "fans",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "user_features",
                table: "player_performance_reviews");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                schema: "user_features",
                table: "player_performance_reviews");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                schema: "user_features",
                table: "player_performance_reviews");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                schema: "user_features",
                table: "player_performance_reviews");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "user_features",
                table: "game_reviews");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                schema: "user_features",
                table: "game_reviews");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                schema: "user_features",
                table: "game_reviews");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                schema: "user_features",
                table: "game_reviews");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "user_features",
                table: "fans");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                schema: "user_features",
                table: "fans");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                schema: "user_features",
                table: "fans");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                schema: "user_features",
                table: "fans");
        }
    }
}
