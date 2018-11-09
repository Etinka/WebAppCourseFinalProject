using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using WebAppCourseFinalProject.Models;

namespace WebAppCourseFinalProject.Migrations
{
    [DbContext(typeof(UserContext))]
    [Migration("20181109103457_adding_branch")]
    partial class adding_branch
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebAppCourseFinalProject.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Title")
                        .IsRequired();

                    b.Property<string>("Subtitle")
                        .IsRequired();


                    b.Property<double>("Longtitude")
                        .IsRequired();

                    b.Property<double>("Latitude")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Branch");
                });
        }
    }
}
