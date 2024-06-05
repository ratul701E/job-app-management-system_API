using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace jobappmanagementsystem.api.Migrations
{
    /// <inheritdoc />
    public partial class addedJobApplicationAndRelatedTables1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JobApplications",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dob = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsAiubian = table.Column<bool>(type: "bit", nullable: false),
                    IsBscCompleted = table.Column<bool>(type: "bit", nullable: false),
                    IsMscCompleted = table.Column<bool>(type: "bit", nullable: false),
                    AiubId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BscUniversity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BscDepartment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BscCGPA = table.Column<double>(type: "float", nullable: true),
                    BscAdmissionYear = table.Column<int>(type: "int", nullable: true),
                    BscGraduationYear = table.Column<int>(type: "int", nullable: true),
                    MscUniversity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MscDepartment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MscCGPA = table.Column<double>(type: "float", nullable: true),
                    MscAdmissionYear = table.Column<int>(type: "int", nullable: true),
                    MscGraduationYear = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobApplications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JobApplicationId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Skills_JobApplications_JobApplicationId",
                        column: x => x.JobApplicationId,
                        principalTable: "JobApplications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Skills_JobApplicationId",
                table: "Skills",
                column: "JobApplicationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "JobApplications");
        }
    }
}
