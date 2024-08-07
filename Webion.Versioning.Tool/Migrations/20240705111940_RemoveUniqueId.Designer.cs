﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Webion.Versioning.Tool.Contexts;

#nullable disable

namespace Webion.Versioning.Tool.Migrations
{
    [DbContext(typeof(VersioningDbContext))]
    [Migration("20240705111940_RemoveUniqueId")]
    partial class RemoveUniqueId
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.6");

            modelBuilder.Entity("Webion.Versioning.Tool.Data.AppDbo", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<uint>("BuildCount")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset>("BuildDate")
                        .HasColumnType("TEXT");

                    b.Property<uint>("Major")
                        .HasColumnType("INTEGER");

                    b.Property<uint>("Minor")
                        .HasColumnType("INTEGER");

                    b.HasKey("Name");

                    b.ToTable("apps", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
