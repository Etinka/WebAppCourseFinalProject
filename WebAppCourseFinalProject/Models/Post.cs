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

        public string ContentTrimmed
        {
            get
            {
                if (Content.Length > 200)
                {
                    return Content.Substring(0, 200) + "...";
                }
                return Content;
            }
        }

        public string VideoLink { get; set; }

        public Writer Writer { get; set; }

        public ICollection<Category> Categories { get; set; }//many2many - many category, many products

        [Required()]
        public DateTime CreatedAt { get; set; }

        public Post()
        {
            CreatedAt = DateTime.Now;
        }

    }
}
