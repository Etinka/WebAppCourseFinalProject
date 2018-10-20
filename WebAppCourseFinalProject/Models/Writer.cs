using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppCourseFinalProject.Models
{
    public class Writer
    {
        public int ID { get; set; }

        public int UserId { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }

        public ICollection<Post> Posts { get; set; }//one2many - one writer, many Posts

    }
}
