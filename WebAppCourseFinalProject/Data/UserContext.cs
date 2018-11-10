using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAppCourseFinalProject.Models;

namespace WebAppCourseFinalProject.Models
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {
        }

        public DbSet<WebAppCourseFinalProject.Models.User> User { get; set; }

        public DbSet<WebAppCourseFinalProject.Models.Post> Post { get; set; }

        public DbSet<WebAppCourseFinalProject.Models.Writer> Writer { get; set; }

        public DbSet<WebAppCourseFinalProject.Models.Category> Category { get; set; }
        public DbSet<WebAppCourseFinalProject.Models.Branch> Branch { get; set; }
        public DbSet<WebAppCourseFinalProject.Models.PostCategory> PostCategory { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PostCategory>()
                .HasKey(t => new { t.PostId, t.CategoryId });

            modelBuilder.Entity<PostCategory>()
                .HasOne(pt => pt.Post)
                .WithMany("PostTags");

            modelBuilder.Entity<PostCategory>()
                .HasOne(pt => pt.Category)
                .WithMany("PostTags");
        }
    }
}
