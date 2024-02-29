﻿// <auto-generated />
using System;
using HealthManagerServer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HealthManagerServer.Migrations
{
    [DbContext(typeof(DataBaseContext))]
    [Migration("20240229073504_ActivitiesRepair")]
    partial class ActivitiesRepair
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("HealthManagerServer.Model.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("Weight")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("UserName")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Nutrition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("Calories")
                        .HasColumnType("float");

                    b.Property<double>("Carbohydrates_total_g")
                        .HasColumnType("float");

                    b.Property<double>("Cholesterol_mg")
                        .HasColumnType("float");

                    b.Property<double>("Fat_saturated_g")
                        .HasColumnType("float");

                    b.Property<double>("Fat_total_g")
                        .HasColumnType("float");

                    b.Property<double>("Fiber_g")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("Potassium_mg")
                        .HasColumnType("float");

                    b.Property<double>("Protein_g")
                        .HasColumnType("float");

                    b.Property<double>("Serving_size_g")
                        .HasColumnType("float");

                    b.Property<double>("Sodium_mg")
                        .HasColumnType("float");

                    b.Property<double>("Sugar_g")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("Name", "Serving_size_g")
                        .IsUnique();

                    b.ToTable("Nutritions");
                });
#pragma warning restore 612, 618
        }
    }
}
