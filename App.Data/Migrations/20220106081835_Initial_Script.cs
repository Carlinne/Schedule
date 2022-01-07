using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using System;

namespace App.Data.Migrations
{
    public partial class Initial_Script : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "tr");

            migrationBuilder.EnsureSchema(
                name: "tb");

            migrationBuilder.CreateTable(
                name: "Statuses",
                schema: "tb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Properties",
                schema: "tr",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Disabled_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Created_At = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Updated_At = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    StatusId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Properties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Properties_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalSchema: "tb",
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Activities",
                schema: "tr",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Property_Id = table.Column<int>(type: "integer", nullable: false),
                    Schedule = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Created_At = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Updated_At = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    StatusId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Activities_Properties_Property_Id",
                        column: x => x.Property_Id,
                        principalSchema: "tr",
                        principalTable: "Properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Activities_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalSchema: "tb",
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Surveys",
                schema: "tr",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Activity_Id = table.Column<int>(type: "integer", nullable: false),
                    Answers = table.Column<string>(type: "text", nullable: false),
                    Created_At = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Updated_At = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    StatusId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Surveys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Surveys_Activities_Activity_Id",
                        column: x => x.Activity_Id,
                        principalSchema: "tr",
                        principalTable: "Activities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Surveys_Statuses_StatusId",
                        column: x => x.StatusId,
                        principalSchema: "tb",
                        principalTable: "Statuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activities_Property_Id",
                schema: "tr",
                table: "Activities",
                column: "Property_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_StatusId",
                schema: "tr",
                table: "Activities",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_StatusId",
                schema: "tr",
                table: "Properties",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Surveys_Activity_Id",
                schema: "tr",
                table: "Surveys",
                column: "Activity_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Surveys_StatusId",
                schema: "tr",
                table: "Surveys",
                column: "StatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Surveys",
                schema: "tr");

            migrationBuilder.DropTable(
                name: "Activities",
                schema: "tr");

            migrationBuilder.DropTable(
                name: "Properties",
                schema: "tr");

            migrationBuilder.DropTable(
                name: "Statuses",
                schema: "tb");
        }
    }
}
