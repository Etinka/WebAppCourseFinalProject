using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppCourseFinalProject.Models
{
    public class Post
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string VideoLink { get; set; }

        public Writer Writer { get; set; }

        public ICollection<Category> Categories { get; set; }//many2many - many category, many products

    }
}
