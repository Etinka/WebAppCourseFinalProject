using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebAppCourseFinalProject.Models
{
    public class Writer
    {
        public int Id { get; set; }

        public User User { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }

        public ICollection<Post> Posts { get; set; }//one2many - one writer, many Posts

    }
}
