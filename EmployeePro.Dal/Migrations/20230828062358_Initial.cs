using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeePro.Dal.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DepartmentEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LanguageEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Language = table.Column<string>(type: "text", nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanguageEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SkillEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Skill = table.Column<string>(type: "text", nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkillEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Fullname = table.Column<string>(type: "text", nullable: false),
                    DateOfBirthday = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    Summary = table.Column<string>(type: "text", nullable: true),
                    DepartmentId = table.Column<Guid>(type: "uuid", nullable: true),
                    DepartmentEntityId = table.Column<Guid>(type: "uuid", nullable: true),
                    ProfilePicUrl = table.Column<string>(type: "text", nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeEntities_DepartmentEntities_DepartmentEntityId",
                        column: x => x.DepartmentEntityId,
                        principalTable: "DepartmentEntities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EducationEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SchoolName = table.Column<string>(type: "text", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: false),
                    EmployeeEntityId = table.Column<Guid>(type: "uuid", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EducationEntities_EmployeeEntities_EmployeeEntityId",
                        column: x => x.EmployeeEntityId,
                        principalTable: "EmployeeEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeLanguageEntity",
                columns: table => new
                {
                    EmployeeId = table.Column<Guid>(type: "uuid", nullable: false),
                    LanguageId = table.Column<Guid>(type: "uuid", nullable: false),
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeLanguageEntity", x => new { x.EmployeeId, x.LanguageId });
                    table.ForeignKey(
                        name: "FK_EmployeeLanguageEntity_EmployeeEntities_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "EmployeeEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeLanguageEntity_LanguageEntity_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "LanguageEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeesSkills",
                columns: table => new
                {
                    EmployeeId = table.Column<Guid>(type: "uuid", nullable: false),
                    SkillId = table.Column<Guid>(type: "uuid", nullable: false),
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeesSkills", x => new { x.EmployeeId, x.SkillId });
                    table.ForeignKey(
                        name: "FK_EmployeesSkills_EmployeeEntities_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "EmployeeEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeesSkills_SkillEntities_SkillId",
                        column: x => x.SkillId,
                        principalTable: "SkillEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExperienceEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    JobTitle = table.Column<string>(type: "text", nullable: true),
                    CompanyName = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    EmployeeEntityId = table.Column<Guid>(type: "uuid", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExperienceEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExperienceEntities_EmployeeEntities_EmployeeEntityId",
                        column: x => x.EmployeeEntityId,
                        principalTable: "EmployeeEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EducationEntities_EmployeeEntityId",
                table: "EducationEntities",
                column: "EmployeeEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeEntities_DepartmentEntityId",
                table: "EmployeeEntities",
                column: "DepartmentEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeLanguageEntity_LanguageId",
                table: "EmployeeLanguageEntity",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeesSkills_SkillId",
                table: "EmployeesSkills",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_ExperienceEntities_EmployeeEntityId",
                table: "ExperienceEntities",
                column: "EmployeeEntityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EducationEntities");

            migrationBuilder.DropTable(
                name: "EmployeeLanguageEntity");

            migrationBuilder.DropTable(
                name: "EmployeesSkills");

            migrationBuilder.DropTable(
                name: "ExperienceEntities");

            migrationBuilder.DropTable(
                name: "LanguageEntity");

            migrationBuilder.DropTable(
                name: "SkillEntities");

            migrationBuilder.DropTable(
                name: "EmployeeEntities");

            migrationBuilder.DropTable(
                name: "DepartmentEntities");
        }
    }
}
