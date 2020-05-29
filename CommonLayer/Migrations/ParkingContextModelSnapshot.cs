﻿// <auto-generated />
using System;
using CommonLayer.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CommonLayer.Migrations
{
    [DbContext(typeof(ParkingContext))]
    partial class ParkingContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CommonLayer.Model.ParkingPortal", b =>
                {
                    b.Property<int>("ReceiptNo")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Brand")
                        .IsRequired();

                    b.Property<string>("DriverName")
                        .IsRequired();

                    b.Property<DateTime>("ParkingDate");

                    b.Property<string>("Slot")
                        .IsRequired();

                    b.Property<string>("Status");

                    b.Property<string>("VehicleColor")
                        .IsRequired();

                    b.Property<string>("VehicleNumber")
                        .IsRequired();

                    b.HasKey("ReceiptNo");

                    b.ToTable("parkingPortals");
                });

            modelBuilder.Entity("CommonLayer.Model.ParkingStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Charges");

                    b.Property<DateTime>("Date");

                    b.Property<int>("ReceiptNo");

                    b.HasKey("Id");

                    b.ToTable("ParkingStatuses");
                });

            modelBuilder.Entity("CommonLayer.Model.UserDetail", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("UserName")
                        .IsRequired();

                    b.Property<string>("UserType")
                        .IsRequired();

                    b.HasKey("UserId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("UserDetails");
                });
#pragma warning restore 612, 618
        }
    }
}