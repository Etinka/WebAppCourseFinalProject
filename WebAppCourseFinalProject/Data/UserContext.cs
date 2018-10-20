using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebAppCourseFinalProject.Models
{
    public class UserContext : DbContext
    {
        public UserContext (DbContextOptions<UserContext> options)
            : base(options)
        {
        }

        public DbSet<WebAppCourseFinalProject.Models.User> User { get; set; }
    }
}
