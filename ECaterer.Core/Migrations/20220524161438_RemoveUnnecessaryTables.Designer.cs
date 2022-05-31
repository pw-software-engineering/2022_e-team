﻿// <auto-generated />
using System;
using ECaterer.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ECaterer.Core.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20220524161438_RemoveUnnecessaryTables")]
    partial class RemoveUnnecessaryTables
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.15")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ECaterer.Core.Models.Address", b =>
                {
                    b.Property<string>("AddressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ApartmentNumber")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("BuildingNumber")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PostCode")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("AddressId");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("ECaterer.Core.Models.Allergent", b =>
                {
                    b.Property<string>("AllergentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("MealId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("AllergentId");

                    b.HasIndex("MealId");

                    b.ToTable("Allergent");
                });

            modelBuilder.Entity("ECaterer.Core.Models.Client", b =>
                {
                    b.Property<string>("ClientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AddressId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("ClientId");

                    b.HasIndex("AddressId");

                    b.ToTable("Client");
                });

            modelBuilder.Entity("ECaterer.Core.Models.Complaint", b =>
                {
                    b.Property<string>("ComplaintId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Answer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("ComplaintId");

                    b.ToTable("Complaint");
                });

            modelBuilder.Entity("ECaterer.Core.Models.DeliveryDetails", b =>
                {
                    b.Property<string>("DeliveryDetailsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AddressId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CommentForDeliverer")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("DeliveryDetailsId");

                    b.HasIndex("AddressId");

                    b.ToTable("DeliveryDetails");
                });

            modelBuilder.Entity("ECaterer.Core.Models.Diet", b =>
                {
                    b.Property<string>("DietId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Calories")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OrderId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("Vegan")
                        .HasColumnType("bit");

                    b.HasKey("DietId");

                    b.HasIndex("OrderId");

                    b.ToTable("Diet");
                });

            modelBuilder.Entity("ECaterer.Core.Models.Ingredient", b =>
                {
                    b.Property<string>("IngredientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("MealId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("IngredientId");

                    b.HasIndex("MealId");

                    b.ToTable("Ingredient");
                });

            modelBuilder.Entity("ECaterer.Core.Models.Meal", b =>
                {
                    b.Property<string>("MealId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Calories")
                        .HasColumnType("int");

                    b.Property<string>("DietId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("Vegan")
                        .HasColumnType("bit");

                    b.HasKey("MealId");

                    b.HasIndex("DietId");

                    b.ToTable("Meal");
                });

            modelBuilder.Entity("ECaterer.Core.Models.Order", b =>
                {
                    b.Property<string>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ComplaintId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DeliveryDetailsId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("OrderId");

                    b.HasIndex("ComplaintId");

                    b.HasIndex("DeliveryDetailsId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("ECaterer.Core.Models.Allergent", b =>
                {
                    b.HasOne("ECaterer.Core.Models.Meal", "Meal")
                        .WithMany("AllergentList")
                        .HasForeignKey("MealId");

                    b.Navigation("Meal");
                });

            modelBuilder.Entity("ECaterer.Core.Models.Client", b =>
                {
                    b.HasOne("ECaterer.Core.Models.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId");

                    b.Navigation("Address");
                });

            modelBuilder.Entity("ECaterer.Core.Models.DeliveryDetails", b =>
                {
                    b.HasOne("ECaterer.Core.Models.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId");

                    b.Navigation("Address");
                });

            modelBuilder.Entity("ECaterer.Core.Models.Diet", b =>
                {
                    b.HasOne("ECaterer.Core.Models.Order", null)
                        .WithMany("Diets")
                        .HasForeignKey("OrderId");
                });

            modelBuilder.Entity("ECaterer.Core.Models.Ingredient", b =>
                {
                    b.HasOne("ECaterer.Core.Models.Meal", "Meal")
                        .WithMany("IngredientList")
                        .HasForeignKey("MealId");

                    b.Navigation("Meal");
                });

            modelBuilder.Entity("ECaterer.Core.Models.Meal", b =>
                {
                    b.HasOne("ECaterer.Core.Models.Diet", null)
                        .WithMany("Meals")
                        .HasForeignKey("DietId");
                });

            modelBuilder.Entity("ECaterer.Core.Models.Order", b =>
                {
                    b.HasOne("ECaterer.Core.Models.Complaint", "Complaint")
                        .WithMany()
                        .HasForeignKey("ComplaintId");

                    b.HasOne("ECaterer.Core.Models.DeliveryDetails", "DeliveryDetails")
                        .WithMany()
                        .HasForeignKey("DeliveryDetailsId");

                    b.Navigation("Complaint");

                    b.Navigation("DeliveryDetails");
                });

            modelBuilder.Entity("ECaterer.Core.Models.Diet", b =>
                {
                    b.Navigation("Meals");
                });

            modelBuilder.Entity("ECaterer.Core.Models.Meal", b =>
                {
                    b.Navigation("AllergentList");

                    b.Navigation("IngredientList");
                });

            modelBuilder.Entity("ECaterer.Core.Models.Order", b =>
                {
                    b.Navigation("Diets");
                });
#pragma warning restore 612, 618
        }
    }
}