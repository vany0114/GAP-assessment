using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Gap.Domain.Customer.Migrations
{
    public partial class CancellationDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AssigningDate",
                schema: "Customer",
                table: "CustomerInsurance",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CancellationDate",
                schema: "Customer",
                table: "CustomerInsurance",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssigningDate",
                schema: "Customer",
                table: "CustomerInsurance");

            migrationBuilder.DropColumn(
                name: "CancellationDate",
                schema: "Customer",
                table: "CustomerInsurance");
        }
    }
}
