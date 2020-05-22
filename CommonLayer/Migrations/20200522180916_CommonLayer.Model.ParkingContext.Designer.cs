﻿// <auto-generated />
using System;
using CommonLayer.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CommonLayer.Migrations
{
    [DbContext(typeof(ParkingContext))]
    [Migration("20200522180916_CommonLayer.Model.ParkingContext")]
    partial class CommonLayerModelParkingContext
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CommonLayer.Model.ParkingPortal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Charges");

                    b.Property<DateTime>("InTime");

                    b.Property<DateTime>("OutTime");

                    b.Property<string>("OwnerName")
                        .IsRequired();

                    b.Property<string>("Slot")
                        .IsRequired();

                    b.Property<string>("VehicleNumber")
                        .IsRequired();

                    b.Property<string>("VehicleType")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("parkingPortals");
                });
#pragma warning restore 612, 618
        }
    }
}
