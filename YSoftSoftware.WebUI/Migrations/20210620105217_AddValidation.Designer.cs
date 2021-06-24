﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using YSoftSoftware.Data.Concrete.ef;

namespace YSoftSoftware.WebUI.Migrations
{
    [DbContext(typeof(YSoftContext))]
    [Migration("20210620105217_AddValidation")]
    partial class AddValidation
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PersonelProject", b =>
                {
                    b.Property<int>("PersonelsPersonelId")
                        .HasColumnType("int");

                    b.Property<int>("ProjectsProjectId")
                        .HasColumnType("int");

                    b.HasKey("PersonelsPersonelId", "ProjectsProjectId");

                    b.HasIndex("ProjectsProjectId");

                    b.ToTable("PersonelProject");
                });

            modelBuilder.Entity("YSoftSoftware.Entity.AccountingProgram", b =>
                {
                    b.Property<int>("AccountingProgramId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AccountingProgramId");

                    b.ToTable("AccountingProgram");
                });

            modelBuilder.Entity("YSoftSoftware.Entity.Compensation", b =>
                {
                    b.Property<int>("compensationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Money")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("PersonelId")
                        .HasColumnType("int");

                    b.HasKey("compensationId");

                    b.HasIndex("PersonelId")
                        .IsUnique();

                    b.ToTable("Compensations");
                });

            modelBuilder.Entity("YSoftSoftware.Entity.Department", b =>
                {
                    b.Property<int>("DepartmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DepartmentName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DepartmentId");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("YSoftSoftware.Entity.Personel", b =>
                {
                    b.Property<int>("PersonelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccountingProgramId")
                        .HasColumnType("int");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DismissalDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<decimal>("Salary")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("PersonelId");

                    b.HasIndex("AccountingProgramId");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Personels");
                });

            modelBuilder.Entity("YSoftSoftware.Entity.Project", b =>
                {
                    b.Property<int>("ProjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MaxPersonel")
                        .HasColumnType("int");

                    b.Property<int>("MinPersonel")
                        .HasColumnType("int");

                    b.Property<string>("ProjectName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("ProjectId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("PersonelProject", b =>
                {
                    b.HasOne("YSoftSoftware.Entity.Personel", null)
                        .WithMany()
                        .HasForeignKey("PersonelsPersonelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("YSoftSoftware.Entity.Project", null)
                        .WithMany()
                        .HasForeignKey("ProjectsProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("YSoftSoftware.Entity.Compensation", b =>
                {
                    b.HasOne("YSoftSoftware.Entity.Personel", "Personel")
                        .WithOne("Compensation")
                        .HasForeignKey("YSoftSoftware.Entity.Compensation", "PersonelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Personel");
                });

            modelBuilder.Entity("YSoftSoftware.Entity.Personel", b =>
                {
                    b.HasOne("YSoftSoftware.Entity.AccountingProgram", "AccountingProgram")
                        .WithMany("Personels")
                        .HasForeignKey("AccountingProgramId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("YSoftSoftware.Entity.Department", "Department")
                        .WithMany("Personels")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AccountingProgram");

                    b.Navigation("Department");
                });

            modelBuilder.Entity("YSoftSoftware.Entity.AccountingProgram", b =>
                {
                    b.Navigation("Personels");
                });

            modelBuilder.Entity("YSoftSoftware.Entity.Department", b =>
                {
                    b.Navigation("Personels");
                });

            modelBuilder.Entity("YSoftSoftware.Entity.Personel", b =>
                {
                    b.Navigation("Compensation");
                });
#pragma warning restore 612, 618
        }
    }
}