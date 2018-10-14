using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Gap.Domain.Insurance.Migrations
{
    public partial class HasActiveCustomers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasActiveCustomers",
                schema: "Insurance",
                table: "Insurances",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasActiveCustomers",
                schema: "Insurance",
                table: "Insurances");
        }
    }
}
