using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Gap.Domain.Customer.Migrations
{
    public partial class CustomerInsurance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Email",
                schema: "Customer",
                table: "Customer",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.CreateTable(
                name: "CustomerInsurance",
                schema: "Customer",
                columns: table => new
                {
                    InsuranceId = table.Column<int>(nullable: false),
                    CustomerId = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerInsurance", x => new { x.InsuranceId, x.CustomerId });
                    table.ForeignKey(
                        name: "FK_CustomerInsurance_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "Customer",
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerInsurance_CustomerId",
                schema: "Customer",
                table: "CustomerInsurance",
                column: "CustomerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerInsurance",
                schema: "Customer");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                schema: "Customer",
                table: "Customer",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
