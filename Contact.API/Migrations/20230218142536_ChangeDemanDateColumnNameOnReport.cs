using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Contact.API.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDemanDateColumnNameOnReport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DemandDate",
                table: "Report",
                newName: "DemandDateUtc");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DemandDateUtc",
                table: "Report",
                newName: "DemandDate");
        }
    }
}
