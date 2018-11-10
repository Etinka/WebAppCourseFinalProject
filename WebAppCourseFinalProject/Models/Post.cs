using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        public ICollection<PostCategory> PostTags { get; } = new List<PostCategory>();

        [Required()]
        public DateTime CreatedAt { get; set; }

        [NotMapped]
        public SelectList DropDownList { get; set; }

        [NotMapped]
        public IEnumerable<Category> Categories => PostTags.Select(e => e.Category);

        public Post()
        {
            CreatedAt = DateTime.Now;
        }

    }
}
