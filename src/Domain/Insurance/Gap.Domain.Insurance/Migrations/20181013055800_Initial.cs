using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Gap.Domain.Insurance.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Insurance");

            migrationBuilder.CreateSequence(
                name: "insurance_seq",
                schema: "Insurance",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "CoverageTypes",
                schema: "Insurance",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false, defaultValue: 1),
                    Description = table.Column<string>(maxLength: 500, nullable: true),
                    Name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoverageTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Insurances",
                schema: "Insurance",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Cost = table.Column<double>(nullable: false),
                    CoveragePeriod = table.Column<int>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    CustomerId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    Risk = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Insurances", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InsuranceCoverage",
                schema: "Insurance",
                columns: table => new
                {
                    InsuranceId = table.Column<int>(nullable: false),
                    CoverageId = table.Column<int>(nullable: false),
                    Percentage = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsuranceCoverage", x => new { x.InsuranceId, x.CoverageId });
                    table.ForeignKey(
                        name: "FK_InsuranceCoverage_CoverageTypes_CoverageId",
                        column: x => x.CoverageId,
                        principalSchema: "Insurance",
                        principalTable: "CoverageTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InsuranceCoverage_Insurances_InsuranceId",
                        column: x => x.InsuranceId,
                        principalSchema: "Insurance",
                        principalTable: "Insurances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceCoverage_CoverageId",
                schema: "Insurance",
                table: "InsuranceCoverage",
                column: "CoverageId");

            migrationBuilder.CreateIndex(
                name: "IX_Insurances_CreationDate",
                schema: "Insurance",
                table: "Insurances",
                column: "CreationDate",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Insurances_CustomerId",
                schema: "Insurance",
                table: "Insurances",
                column: "CustomerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Insurances_Description",
                schema: "Insurance",
                table: "Insurances",
                column: "Description");

            migrationBuilder.CreateIndex(
                name: "IX_Insurances_StartDate",
                schema: "Insurance",
                table: "Insurances",
                column: "StartDate",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InsuranceCoverage",
                schema: "Insurance");

            migrationBuilder.DropTable(
                name: "CoverageTypes",
                schema: "Insurance");

            migrationBuilder.DropTable(
                name: "Insurances",
                schema: "Insurance");

            migrationBuilder.DropSequence(
                name: "insurance_seq",
                schema: "Insurance");
        }
    }
}
