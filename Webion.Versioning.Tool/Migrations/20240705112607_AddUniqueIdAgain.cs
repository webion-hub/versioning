using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Webion.Versioning.Tool.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueIdAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UniqueId",
                table: "apps",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UniqueId",
                table: "apps");
        }
    }
}
