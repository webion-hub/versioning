using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Webion.Versioning.Tool.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "apps",
                columns: table => new
                {
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Major = table.Column<uint>(type: "INTEGER", nullable: false),
                    Minor = table.Column<uint>(type: "INTEGER", nullable: false),
                    BuildDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    BuildCount = table.Column<uint>(type: "INTEGER", nullable: false),
                    UniqueId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_apps", x => x.Name);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "apps");
        }
    }
}
