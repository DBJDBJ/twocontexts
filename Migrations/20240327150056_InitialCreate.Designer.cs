﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ipan;

#nullable disable

namespace simpletwocontexts.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240327150056_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.3");

            modelBuilder.Entity("ipan.Service", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ClientID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("OperatorID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("OutgoingInvoiceID")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.ToTable("Services", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}