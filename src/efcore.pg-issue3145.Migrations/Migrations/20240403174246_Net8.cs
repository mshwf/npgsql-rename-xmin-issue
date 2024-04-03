using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace efcore.pgissue3145.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class Net8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "xmin",
                schema: "public",
                table: "Settings",
                newName: "version");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "version",
                schema: "public",
                table: "Settings",
                newName: "xmin");
        }
    }
}
