﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DarkClicker.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0");

            modelBuilder.Entity("Score", b =>
                {
                    b.Property<int>("scoreId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("playerName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<float>("time")
                        .HasColumnType("REAL");

                    b.HasKey("scoreId");

                    b.ToTable("MaxScores");
                });
#pragma warning restore 612, 618
        }
    }
}