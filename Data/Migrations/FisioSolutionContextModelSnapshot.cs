﻿// <auto-generated />
using System;
using FisioSolution.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FisioSolution.Data.Migrations
{
    [DbContext(typeof(FisioSolutionContext))]
    partial class FisioSolutionContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("FisioSolution.Models.Patient", b =>
                {
                    b.Property<int>("PatientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PatientId"), 1L, 1);

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("date");

                    b.Property<string>("Dni")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Height")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("Insurance")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Weight")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("PatientId");

                    b.ToTable("Patients");

                    b.HasData(
                        new
                        {
                            PatientId = 1,
                            BirthDate = new DateTime(1980, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Dni = "730151515",
                            Height = 180.0m,
                            Insurance = true,
                            Name = "John Doe",
                            Password = "1234",
                            Weight = 80.5m
                        },
                        new
                        {
                            PatientId = 2,
                            BirthDate = new DateTime(1993, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Dni = "730203040",
                            Height = 172.0m,
                            Insurance = true,
                            Name = "Pedro Martínez",
                            Password = "pass123",
                            Weight = 70.5m
                        });
                });

            modelBuilder.Entity("FisioSolution.Models.Physio", b =>
                {
                    b.Property<int>("PhysioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PhysioId"), 1L, 1);

                    b.Property<bool>("Availeable")
                        .HasColumnType("bit");

                    b.Property<TimeSpan>("ClosingTime")
                        .HasColumnType("time");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan>("OpeningTime")
                        .HasColumnType("time");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PatientId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("RegistrationNumber")
                        .HasColumnType("int");

                    b.HasKey("PhysioId");

                    b.HasIndex("PatientId");

                    b.ToTable("Physios");

                    b.HasData(
                        new
                        {
                            PhysioId = 1,
                            Availeable = true,
                            ClosingTime = new TimeSpan(0, 18, 0, 0, 0),
                            Name = "Daniel",
                            OpeningTime = new TimeSpan(0, 8, 0, 0, 0),
                            Password = "1234",
                            Price = 50.00m,
                            RegistrationNumber = 1783
                        },
                        new
                        {
                            PhysioId = 2,
                            Availeable = false,
                            ClosingTime = new TimeSpan(0, 17, 0, 0, 0),
                            Name = "Maria",
                            OpeningTime = new TimeSpan(0, 9, 0, 0, 0),
                            Password = "futbol3",
                            Price = 60.00m,
                            RegistrationNumber = 1700
                        },
                        new
                        {
                            PhysioId = 3,
                            Availeable = true,
                            ClosingTime = new TimeSpan(0, 17, 0, 0, 0),
                            Name = "Jaime",
                            OpeningTime = new TimeSpan(0, 9, 0, 0, 0),
                            Password = "cocacola27",
                            Price = 60.00m,
                            RegistrationNumber = 1600
                        });
                });

            modelBuilder.Entity("FisioSolution.Models.Treatment", b =>
                {
                    b.Property<int>("TreatmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TreatmentId"), 1L, 1);

                    b.Property<bool>("MoreSessionsNeeded")
                        .HasColumnType("bit");

                    b.Property<int>("PatientId")
                        .HasColumnType("int");

                    b.Property<int>("PhysioId")
                        .HasColumnType("int");

                    b.Property<string>("TreatmentCause")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TreatmentDate")
                        .HasColumnType("date");

                    b.HasKey("TreatmentId");

                    b.HasIndex("PatientId");

                    b.HasIndex("PhysioId");

                    b.ToTable("Treatments");

                    b.HasData(
                        new
                        {
                            TreatmentId = 1,
                            MoreSessionsNeeded = true,
                            PatientId = 1,
                            PhysioId = 1,
                            TreatmentCause = "Dolor de espalda",
                            TreatmentDate = new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("FisioSolution.Models.Physio", b =>
                {
                    b.HasOne("FisioSolution.Models.Patient", null)
                        .WithMany("AssignedPhysios")
                        .HasForeignKey("PatientId");
                });

            modelBuilder.Entity("FisioSolution.Models.Treatment", b =>
                {
                    b.HasOne("FisioSolution.Models.Patient", "Patient")
                        .WithMany("MyTreatments")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FisioSolution.Models.Physio", "Physio")
                        .WithMany("MyTreatments")
                        .HasForeignKey("PhysioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Patient");

                    b.Navigation("Physio");
                });

            modelBuilder.Entity("FisioSolution.Models.Patient", b =>
                {
                    b.Navigation("AssignedPhysios");

                    b.Navigation("MyTreatments");
                });

            modelBuilder.Entity("FisioSolution.Models.Physio", b =>
                {
                    b.Navigation("MyTreatments");
                });
#pragma warning restore 612, 618
        }
    }
}
