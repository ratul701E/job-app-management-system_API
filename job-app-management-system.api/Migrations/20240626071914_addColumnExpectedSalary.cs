using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace jobappmanagementsystem.api.Migrations
{
    /// <inheritdoc />
    public partial class addColumnExpectedSalary : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "ExpectedSalary",
                table: "JobApplications",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpectedSalary",
                table: "JobApplications");
        }
    }
}
