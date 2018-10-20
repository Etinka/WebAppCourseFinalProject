﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebAppCourseFinalProject.Models;

namespace WebAppCourseFinalProject.Migrations
{
    [DbContext(typeof(UserContext))]
    [Migration("20181020130229_adding_posts_writers")]
    partial class adding_posts_writers
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebAppCourseFinalProject.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<int?>("PostId");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("WebAppCourseFinalProject.Models.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content");

                    b.Property<string>("Title");

                    b.Property<string>("VideoLink");

                    b.Property<int?>("WriterId");

                    b.HasKey("Id");

                    b.HasIndex("WriterId");

                    b.ToTable("Post");
                });

            modelBuilder.Entity("WebAppCourseFinalProject.Models.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<bool>("IsAdmin");

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<string>("Password");

                    b.HasKey("ID");

                    b.ToTable("User");
                });

            modelBuilder.Entity("WebAppCourseFinalProject.Models.Writer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<string>("DisplayName");

                    b.Property<int?>("UserID");

                    b.HasKey("Id");

                    b.HasIndex("UserID");

                    b.ToTable("Writer");
                });

            modelBuilder.Entity("WebAppCourseFinalProject.Models.Category", b =>
                {
                    b.HasOne("WebAppCourseFinalProject.Models.Post")
                        .WithMany("Categories")
                        .HasForeignKey("PostId");
                });

            modelBuilder.Entity("WebAppCourseFinalProject.Models.Post", b =>
                {
                    b.HasOne("WebAppCourseFinalProject.Models.Writer", "Writer")
                        .WithMany("Posts")
                        .HasForeignKey("WriterId");
                });

            modelBuilder.Entity("WebAppCourseFinalProject.Models.Writer", b =>
                {
                    b.HasOne("WebAppCourseFinalProject.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID");
                });
#pragma warning restore 612, 618
        }
    }
}
