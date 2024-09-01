﻿// <auto-generated />
using System;
using EmployeesTransportManagement.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EmployeesTransportManagement.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240901101703_mssql.local_migration_980")]
    partial class mssqllocal_migration_980
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EmployeesTransportManagement.Models.Coordinator", b =>
                {
                    b.Property<Guid>("CoordinatorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CoordinatorId");

                    b.ToTable("Coordinators");
                });

            modelBuilder.Entity("EmployeesTransportManagement.Models.Employee", b =>
                {
                    b.Property<Guid>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EmployeeId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("EmployeesTransportManagement.Models.Project", b =>
                {
                    b.Property<Guid>("ProjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CoordinatorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ProjectName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProjectId");

                    b.HasIndex("CoordinatorId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("EmployeesTransportManagement.Models.Settlement", b =>
                {
                    b.Property<Guid>("SettlementId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("DateSubmitted")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsApproved")
                        .HasColumnType("bit");

                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("SettlementId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("ProjectId");

                    b.ToTable("Settlements");
                });

            modelBuilder.Entity("EmployeesTransportManagement.Models.Project", b =>
                {
                    b.HasOne("EmployeesTransportManagement.Models.Coordinator", "Coordinator")
                        .WithMany("Projects")
                        .HasForeignKey("CoordinatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Coordinator");
                });

            modelBuilder.Entity("EmployeesTransportManagement.Models.Settlement", b =>
                {
                    b.HasOne("EmployeesTransportManagement.Models.Employee", "Employee")
                        .WithMany("Settlements")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EmployeesTransportManagement.Models.Project", "Project")
                        .WithMany("Settlements")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("EmployeesTransportManagement.Models.Coordinator", b =>
                {
                    b.Navigation("Projects");
                });

            modelBuilder.Entity("EmployeesTransportManagement.Models.Employee", b =>
                {
                    b.Navigation("Settlements");
                });

            modelBuilder.Entity("EmployeesTransportManagement.Models.Project", b =>
                {
                    b.Navigation("Settlements");
                });
#pragma warning restore 612, 618
        }
    }
}
