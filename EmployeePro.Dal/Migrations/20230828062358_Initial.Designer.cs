﻿// <auto-generated />
using System;
using EmployeePro.Dal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EmployeePro.Dal.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20230828062358_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("EmployeePro.Dal.Entities.BridgeTables.EmployeeLanguageEntity", b =>
                {
                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("LanguageId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreationDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.HasKey("EmployeeId", "LanguageId");

                    b.HasIndex("LanguageId");

                    b.ToTable("EmployeeLanguageEntity");
                });

            modelBuilder.Entity("EmployeePro.Dal.Entities.BridgeTables.EmployeeSkillEntity", b =>
                {
                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("SkillId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreationDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.HasKey("EmployeeId", "SkillId");

                    b.HasIndex("SkillId");

                    b.ToTable("EmployeesSkills");
                });

            modelBuilder.Entity("EmployeePro.Dal.Entities.DepartmentEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreationDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("DepartmentEntities");
                });

            modelBuilder.Entity("EmployeePro.Dal.Entities.EducationEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreationDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("EmployeeEntityId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("SchoolName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeEntityId");

                    b.ToTable("EducationEntities");
                });

            modelBuilder.Entity("EmployeePro.Dal.Entities.EmployeeEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreationDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DateOfBirthday")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("DepartmentEntityId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("DepartmentId")
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Fullname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ProfilePicUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Summary")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentEntityId");

                    b.ToTable("EmployeeEntities");
                });

            modelBuilder.Entity("EmployeePro.Dal.Entities.ExperienceEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreationDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<Guid>("EmployeeEntityId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("JobTitle")
                        .HasColumnType("text");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeEntityId");

                    b.ToTable("ExperienceEntities");
                });

            modelBuilder.Entity("EmployeePro.Dal.Entities.LanguageEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreationDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Language")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("LanguageEntity");
                });

            modelBuilder.Entity("EmployeePro.Dal.Entities.SkillEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreationDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Skill")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("SkillEntities");
                });

            modelBuilder.Entity("EmployeePro.Dal.Entities.BridgeTables.EmployeeLanguageEntity", b =>
                {
                    b.HasOne("EmployeePro.Dal.Entities.EmployeeEntity", "EmployeeEntity")
                        .WithMany("EmployeeLanguages")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EmployeePro.Dal.Entities.LanguageEntity", "LanguageEntity")
                        .WithMany("EmployeeLanguages")
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EmployeeEntity");

                    b.Navigation("LanguageEntity");
                });

            modelBuilder.Entity("EmployeePro.Dal.Entities.BridgeTables.EmployeeSkillEntity", b =>
                {
                    b.HasOne("EmployeePro.Dal.Entities.EmployeeEntity", "EmployeeEntity")
                        .WithMany("EmployeesSkills")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EmployeePro.Dal.Entities.SkillEntity", "SkillEntity")
                        .WithMany("EmployeesSkills")
                        .HasForeignKey("SkillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EmployeeEntity");

                    b.Navigation("SkillEntity");
                });

            modelBuilder.Entity("EmployeePro.Dal.Entities.EducationEntity", b =>
                {
                    b.HasOne("EmployeePro.Dal.Entities.EmployeeEntity", "EmployeeEntity")
                        .WithMany()
                        .HasForeignKey("EmployeeEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EmployeeEntity");
                });

            modelBuilder.Entity("EmployeePro.Dal.Entities.EmployeeEntity", b =>
                {
                    b.HasOne("EmployeePro.Dal.Entities.DepartmentEntity", "DepartmentEntity")
                        .WithMany()
                        .HasForeignKey("DepartmentEntityId");

                    b.Navigation("DepartmentEntity");
                });

            modelBuilder.Entity("EmployeePro.Dal.Entities.ExperienceEntity", b =>
                {
                    b.HasOne("EmployeePro.Dal.Entities.EmployeeEntity", "EmployeeEntity")
                        .WithMany()
                        .HasForeignKey("EmployeeEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EmployeeEntity");
                });

            modelBuilder.Entity("EmployeePro.Dal.Entities.EmployeeEntity", b =>
                {
                    b.Navigation("EmployeeLanguages");

                    b.Navigation("EmployeesSkills");
                });

            modelBuilder.Entity("EmployeePro.Dal.Entities.LanguageEntity", b =>
                {
                    b.Navigation("EmployeeLanguages");
                });

            modelBuilder.Entity("EmployeePro.Dal.Entities.SkillEntity", b =>
                {
                    b.Navigation("EmployeesSkills");
                });
#pragma warning restore 612, 618
        }
    }
}
