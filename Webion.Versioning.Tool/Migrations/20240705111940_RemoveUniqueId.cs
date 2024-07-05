using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Webion.Versioning.Tool.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUniqueId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UniqueId",
                table: "apps");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UniqueId",
                table: "apps",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
