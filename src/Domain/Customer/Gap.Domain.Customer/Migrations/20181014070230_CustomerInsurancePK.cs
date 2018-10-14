using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Gap.Domain.Customer.Migrations
{
    public partial class CustomerInsurancePK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerInsurance",
                schema: "Customer",
                table: "CustomerInsurance");

            migrationBuilder.CreateSequence(
                name: "customerinsurance_seq",
                schema: "Customer",
                incrementBy: 10);

            migrationBuilder.AddColumn<int>(
                name: "CustomerInsuranceID",
                schema: "Customer",
                table: "CustomerInsurance",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerInsurance",
                schema: "Customer",
                table: "CustomerInsurance",
                column: "CustomerInsuranceID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerInsurance",
                schema: "Customer",
                table: "CustomerInsurance");

            migrationBuilder.DropSequence(
                name: "customerinsurance_seq",
                schema: "Customer");

            migrationBuilder.DropColumn(
                name: "CustomerInsuranceID",
                schema: "Customer",
                table: "CustomerInsurance");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerInsurance",
                schema: "Customer",
                table: "CustomerInsurance",
                columns: new[] { "InsuranceId", "CustomerId" });
        }
    }
}
