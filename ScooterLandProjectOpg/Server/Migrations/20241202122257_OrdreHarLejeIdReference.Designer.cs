﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ScooterLandProjectOpg.Server.Context;

#nullable disable

namespace ScooterLandProjectOpg.Server.Migrations
{
    [DbContext(typeof(ScooterLandContext))]
    [Migration("20241202122257_OrdreHarLejeIdReference")]
    partial class OrdreHarLejeIdReference
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ScooterLandProjectOpg.Shared.Models.Betaling", b =>
                {
                    b.Property<int>("BetalingsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BetalingsId"));

                    b.Property<double?>("Beløb")
                        .HasColumnType("float");

                    b.Property<DateTime?>("BetalingsDato")
                        .HasColumnType("datetime2");

                    b.Property<int?>("BetalingsMetode")
                        .HasColumnType("int");

                    b.Property<bool>("Betalt")
                        .HasColumnType("bit");

                    b.Property<int>("OrdreId")
                        .HasColumnType("int");

                    b.HasKey("BetalingsId");

                    b.HasIndex("OrdreId");

                    b.ToTable("Betalinger");
                });

            modelBuilder.Entity("ScooterLandProjectOpg.Shared.Models.Kunde", b =>
                {
                    b.Property<int>("KundeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("KundeId"));

                    b.Property<string>("Adresse")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Navn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Telefonnummer")
                        .HasColumnType("int");

                    b.HasKey("KundeId");

                    b.ToTable("Kunder");
                });

            modelBuilder.Entity("ScooterLandProjectOpg.Shared.Models.KundeScooter", b =>
                {
                    b.Property<int>("ScooterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ScooterId"));

                    b.Property<int>("KundeId")
                        .HasColumnType("int");

                    b.Property<string>("Maerke")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ProduktionsAar")
                        .HasColumnType("int");

                    b.Property<string>("RegistreringsNummer")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ScooterId");

                    b.HasIndex("KundeId");

                    b.ToTable("KunderScootere");
                });

            modelBuilder.Entity("ScooterLandProjectOpg.Shared.Models.LejeAftale", b =>
                {
                    b.Property<int>("LejeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LejeId"));

                    b.Property<double>("DagligLeje")
                        .HasColumnType("float");

                    b.Property<double>("ForsikringsPris")
                        .HasColumnType("float");

                    b.Property<double>("KilometerPris")
                        .HasColumnType("float");

                    b.Property<int?>("KortKilometer")
                        .HasColumnType("int");

                    b.Property<int>("KundeId")
                        .HasColumnType("int");

                    b.Property<double>("Selvrisiko")
                        .HasColumnType("float");

                    b.Property<DateTime>("SlutDato")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartDato")
                        .HasColumnType("datetime2");

                    b.HasKey("LejeId");

                    b.HasIndex("KundeId");

                    b.ToTable("LejeAftaler");
                });

            modelBuilder.Entity("ScooterLandProjectOpg.Shared.Models.LejeScooter", b =>
                {
                    b.Property<int>("LejeScooterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LejeScooterId"));

                    b.Property<bool>("ErTilgængelig")
                        .HasColumnType("bit");

                    b.Property<int?>("LejeId")
                        .HasColumnType("int");

                    b.Property<string>("RegistreringsNummer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ScooterMaerke")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ScooterModel")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("SlutDato")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("StartDato")
                        .HasColumnType("datetime2");

                    b.HasKey("LejeScooterId");

                    b.HasIndex("LejeId");

                    b.ToTable("LejeScootere");
                });

            modelBuilder.Entity("ScooterLandProjectOpg.Shared.Models.Mekaniker", b =>
                {
                    b.Property<int>("MekanikerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MekanikerId"));

                    b.Property<string>("Navn")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Speciale")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Telefonnummer")
                        .HasColumnType("int");

                    b.HasKey("MekanikerId");

                    b.ToTable("Mekanikere");
                });

            modelBuilder.Entity("ScooterLandProjectOpg.Shared.Models.Ordre", b =>
                {
                    b.Property<int>("OrdreId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrdreId"));

                    b.Property<DateTime?>("Dato")
                        .HasColumnType("datetime2");

                    b.Property<int>("KundeId")
                        .HasColumnType("int");

                    b.Property<int?>("LejeId")
                        .HasColumnType("int");

                    b.Property<int?>("Status")
                        .HasColumnType("int");

                    b.Property<double?>("TotalPris")
                        .HasColumnType("float");

                    b.HasKey("OrdreId");

                    b.HasIndex("KundeId");

                    b.HasIndex("LejeId");

                    b.ToTable("Ordrer");
                });

            modelBuilder.Entity("ScooterLandProjectOpg.Shared.Models.OrdreYdelse", b =>
                {
                    b.Property<int>("OrdreYdelseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrdreYdelseId"));

                    b.Property<double?>("AftaltPris")
                        .HasColumnType("float");

                    b.Property<DateTime?>("Dato")
                        .HasColumnType("datetime2");

                    b.Property<int?>("MekanikerId")
                        .HasColumnType("int");

                    b.Property<int>("OrdreId")
                        .HasColumnType("int");

                    b.Property<int?>("ScooterId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("SlutDato")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("StartDato")
                        .HasColumnType("datetime2");

                    b.Property<double?>("Timer")
                        .HasColumnType("float");

                    b.Property<int>("YdelseId")
                        .HasColumnType("int");

                    b.HasKey("OrdreYdelseId");

                    b.HasIndex("MekanikerId");

                    b.HasIndex("OrdreId");

                    b.HasIndex("ScooterId");

                    b.HasIndex("YdelseId");

                    b.ToTable("OrdreYdelser");
                });

            modelBuilder.Entity("ScooterLandProjectOpg.Shared.Models.Ydelse", b =>
                {
                    b.Property<int>("YdelseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("YdelseId"));

                    b.Property<string>("Navn")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("StandardPris")
                        .HasColumnType("float");

                    b.HasKey("YdelseId");

                    b.ToTable("Ydelser");
                });

            modelBuilder.Entity("ScooterLandProjectOpg.Shared.Models.Betaling", b =>
                {
                    b.HasOne("ScooterLandProjectOpg.Shared.Models.Ordre", "Ordre")
                        .WithMany("Betalinger")
                        .HasForeignKey("OrdreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ordre");
                });

            modelBuilder.Entity("ScooterLandProjectOpg.Shared.Models.KundeScooter", b =>
                {
                    b.HasOne("ScooterLandProjectOpg.Shared.Models.Kunde", "Kunde")
                        .WithMany("KundeScooter")
                        .HasForeignKey("KundeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Kunde");
                });

            modelBuilder.Entity("ScooterLandProjectOpg.Shared.Models.LejeAftale", b =>
                {
                    b.HasOne("ScooterLandProjectOpg.Shared.Models.Kunde", "Kunde")
                        .WithMany("LejeAftale")
                        .HasForeignKey("KundeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Kunde");
                });

            modelBuilder.Entity("ScooterLandProjectOpg.Shared.Models.LejeScooter", b =>
                {
                    b.HasOne("ScooterLandProjectOpg.Shared.Models.LejeAftale", "LejeAftale")
                        .WithMany("LejeScooter")
                        .HasForeignKey("LejeId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("LejeAftale");
                });

            modelBuilder.Entity("ScooterLandProjectOpg.Shared.Models.Ordre", b =>
                {
                    b.HasOne("ScooterLandProjectOpg.Shared.Models.Kunde", "Kunde")
                        .WithMany("Ordre")
                        .HasForeignKey("KundeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ScooterLandProjectOpg.Shared.Models.LejeAftale", "LejeAftale")
                        .WithMany()
                        .HasForeignKey("LejeId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Kunde");

                    b.Navigation("LejeAftale");
                });

            modelBuilder.Entity("ScooterLandProjectOpg.Shared.Models.OrdreYdelse", b =>
                {
                    b.HasOne("ScooterLandProjectOpg.Shared.Models.Mekaniker", "Mekaniker")
                        .WithMany()
                        .HasForeignKey("MekanikerId");

                    b.HasOne("ScooterLandProjectOpg.Shared.Models.Ordre", "Ordre")
                        .WithMany("OrdreYdelse")
                        .HasForeignKey("OrdreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ScooterLandProjectOpg.Shared.Models.KundeScooter", "Scooter")
                        .WithMany()
                        .HasForeignKey("ScooterId");

                    b.HasOne("ScooterLandProjectOpg.Shared.Models.Ydelse", "Ydelse")
                        .WithMany("OrdreYdelse")
                        .HasForeignKey("YdelseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Mekaniker");

                    b.Navigation("Ordre");

                    b.Navigation("Scooter");

                    b.Navigation("Ydelse");
                });

            modelBuilder.Entity("ScooterLandProjectOpg.Shared.Models.Kunde", b =>
                {
                    b.Navigation("KundeScooter");

                    b.Navigation("LejeAftale");

                    b.Navigation("Ordre");
                });

            modelBuilder.Entity("ScooterLandProjectOpg.Shared.Models.LejeAftale", b =>
                {
                    b.Navigation("LejeScooter");
                });

            modelBuilder.Entity("ScooterLandProjectOpg.Shared.Models.Ordre", b =>
                {
                    b.Navigation("Betalinger");

                    b.Navigation("OrdreYdelse");
                });

            modelBuilder.Entity("ScooterLandProjectOpg.Shared.Models.Ydelse", b =>
                {
                    b.Navigation("OrdreYdelse");
                });
#pragma warning restore 612, 618
        }
    }
}
