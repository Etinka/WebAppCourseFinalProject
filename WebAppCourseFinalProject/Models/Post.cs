using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppCourseFinalProject.Models
{
    public class Post
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Mandatory")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Mandatory")]
        public string Content { get; set; }

        public string VideoLink { get; set; }

        public Writer Writer { get; set; }

        public ICollection<Category> Categories { get; set; }//many2many - many category, many products

    }
}
