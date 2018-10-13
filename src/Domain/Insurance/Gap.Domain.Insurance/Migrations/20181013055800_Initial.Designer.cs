﻿// <auto-generated />
using Gap.Domain.Insurance.Model;
using Gap.Domain.Insurance.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace Gap.Domain.Insurance.Migrations
{
    [DbContext(typeof(InsuranceContext))]
    [Migration("20181013055800_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("Relational:Sequence:Insurance.insurance_seq", "'insurance_seq', 'Insurance', '1', '10', '', '', 'Int64', 'False'")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Gap.Domain.Insurance.Model.CoverageType", b =>
                {
                    b.Property<int>("Id")
                        .HasDefaultValue(1);

                    b.Property<string>("Description")
                        .HasMaxLength(500);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("CoverageTypes","Insurance");
                });

            modelBuilder.Entity("Gap.Domain.Insurance.Model.Insurance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:HiLoSequenceName", "insurance_seq")
                        .HasAnnotation("SqlServer:HiLoSequenceSchema", "Insurance")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.SequenceHiLo);

                    b.Property<double>("Cost");

                    b.Property<int>("CoveragePeriod");

                    b.Property<DateTime>("CreationDate");

                    b.Property<int>("CustomerId");

                    b.Property<string>("Description");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("Risk");

                    b.Property<DateTime>("StartDate");

                    b.HasKey("Id");

                    b.HasIndex("CreationDate")
                        .IsUnique();

                    b.HasIndex("CustomerId")
                        .IsUnique();

                    b.HasIndex("Description");

                    b.HasIndex("StartDate")
                        .IsUnique();

                    b.ToTable("Insurances","Insurance");
                });

            modelBuilder.Entity("Gap.Domain.Insurance.Model.InsuranceCoverage", b =>
                {
                    b.Property<int>("InsuranceId");

                    b.Property<int>("CoverageId");

                    b.Property<decimal>("Percentage");

                    b.HasKey("InsuranceId", "CoverageId");

                    b.HasIndex("CoverageId");

                    b.ToTable("InsuranceCoverage","Insurance");
                });

            modelBuilder.Entity("Gap.Domain.Insurance.Model.InsuranceCoverage", b =>
                {
                    b.HasOne("Gap.Domain.Insurance.Model.CoverageType", "Coverage")
                        .WithMany("InsuranceCoverages")
                        .HasForeignKey("CoverageId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Gap.Domain.Insurance.Model.Insurance", "Insurance")
                        .WithMany("Coverages")
                        .HasForeignKey("InsuranceId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
