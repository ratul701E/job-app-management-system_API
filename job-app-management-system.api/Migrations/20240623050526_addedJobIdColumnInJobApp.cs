using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace jobappmanagementsystem.api.Migrations
{
    /// <inheritdoc />
    public partial class addedJobIdColumnInJobApp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ApplicationId",
                table: "JobApplications",
                type: "bigint",
                nullable: false,
                defaultValue: 5L);

            migrationBuilder.CreateIndex(
                name: "IX_JobApplications_ApplicationId",
                table: "JobApplications",
                column: "ApplicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobApplications_Applications_ApplicationId",
                table: "JobApplications",
                column: "ApplicationId",
                principalTable: "Applications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobApplications_Applications_ApplicationId",
                table: "JobApplications");

            migrationBuilder.DropIndex(
                name: "IX_JobApplications_ApplicationId",
                table: "JobApplications");

            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "JobApplications");
        }
    }
}
