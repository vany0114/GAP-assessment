using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Gap.Domain.Insurance.Migrations
{
    public partial class DropClientId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Insurances_CustomerId",
                schema: "Insurance",
                table: "Insurances");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                schema: "Insurance",
                table: "Insurances");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                schema: "Insurance",
                table: "Insurances",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Insurances_CustomerId",
                schema: "Insurance",
                table: "Insurances",
                column: "CustomerId",
                unique: true);
        }
    }
}
