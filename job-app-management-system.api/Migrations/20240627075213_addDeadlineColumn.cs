using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace jobappmanagementsystem.api.Migrations
{
    /// <inheritdoc />
    public partial class addDeadlineColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Deadline",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: DateTime.Today);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deadline",
                table: "Applications");
        }
    }
}
