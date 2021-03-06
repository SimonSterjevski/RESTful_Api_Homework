﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SEDC.NotesApp.Domain;

namespace SEDC.NotesApp.Domain.Migrations
{
    [DbContext(typeof(NotesAppDbContext))]
    partial class NotesAppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SEDC.NotesApp.Domain.Models.Note", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Color")
                        .HasMaxLength(30);

                    b.Property<int>("TagId");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("TagId");

                    b.HasIndex("UserId");

                    b.ToTable("Notes");

                    b.HasData(
                        new { Id = 1, Color = "Red", TagId = 1, Text = "Stop drinking", UserId = 2 },
                        new { Id = 2, Color = "Yellow", TagId = 2, Text = "Be focused", UserId = 1 },
                        new { Id = 3, Color = "Yellow", TagId = 1, Text = "Drink lemon juice", UserId = 2 }
                    );
                });

            modelBuilder.Entity("SEDC.NotesApp.Domain.Models.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.ToTable("Tags");

                    b.HasData(
                        new { Id = 1, Type = "Health" },
                        new { Id = 2, Type = "Work" }
                    );
                });

            modelBuilder.Entity("SEDC.NotesApp.Domain.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasMaxLength(150);

                    b.Property<string>("FirstName")
                        .HasMaxLength(50);

                    b.Property<string>("LastName")
                        .HasMaxLength(50);

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new { Id = 1, Address = "Adress1", FirstName = "Mark", LastName = "Smith", UserName = "MSmith" },
                        new { Id = 2, Address = "Adress1", FirstName = "Ema", LastName = "Smith", UserName = "ESmith" }
                    );
                });

            modelBuilder.Entity("SEDC.NotesApp.Domain.Models.Note", b =>
                {
                    b.HasOne("SEDC.NotesApp.Domain.Models.Tag", "Tag")
                        .WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SEDC.NotesApp.Domain.Models.User", "User")
                        .WithMany("Notes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
