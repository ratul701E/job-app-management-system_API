using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace jobappmanagementsystem.api.Migrations
{
    /// <inheritdoc />
    public partial class addedNegotiableColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsNegotiable",
                table: "Applications",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsNegotiable",
                table: "Applications");
        }
    }
}
