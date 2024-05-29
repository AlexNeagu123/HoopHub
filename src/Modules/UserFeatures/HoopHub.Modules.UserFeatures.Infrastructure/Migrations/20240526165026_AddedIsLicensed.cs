using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HoopHub.Modules.UserFeatures.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedIsLicensed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsLicensed",
                schema: "user_features",
                table: "fans",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLicensed",
                schema: "user_features",
                table: "fans");
        }
    }
}
