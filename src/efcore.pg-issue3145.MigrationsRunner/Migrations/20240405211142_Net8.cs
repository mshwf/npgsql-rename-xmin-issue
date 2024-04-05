using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MigrationsRunner.Migrations
{
    /// <inheritdoc />
    public partial class Net8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "xmin",
                table: "Settings",
                newName: "version");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "version",
                table: "Settings",
                newName: "xmin");
        }
    }
}
