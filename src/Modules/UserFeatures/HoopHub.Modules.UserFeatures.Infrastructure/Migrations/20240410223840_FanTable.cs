using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HoopHub.Modules.UserFeatures.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FanTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Fans",
                schema: "user_features",
                table: "Fans");

            migrationBuilder.RenameTable(
                name: "Fans",
                schema: "user_features",
                newName: "fans",
                newSchema: "user_features");

            migrationBuilder.AddPrimaryKey(
                name: "PK_fans",
                schema: "user_features",
                table: "fans",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_fans",
                schema: "user_features",
                table: "fans");

            migrationBuilder.RenameTable(
                name: "fans",
                schema: "user_features",
                newName: "Fans",
                newSchema: "user_features");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Fans",
                schema: "user_features",
                table: "Fans",
                column: "Id");
        }
    }
}
