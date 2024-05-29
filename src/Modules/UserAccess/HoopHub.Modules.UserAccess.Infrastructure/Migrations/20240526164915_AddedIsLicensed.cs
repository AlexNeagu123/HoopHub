using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HoopHub.Modules.UserAccess.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedIsLicensed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsLicensed",
                schema: "user_access",
                table: "AspNetUsers",
                type: "boolean",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLicensed",
                schema: "user_access",
                table: "AspNetUsers");
        }
    }
}
