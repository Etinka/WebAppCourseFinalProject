using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppCourseFinalProject.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string NameForDisplay { get { return "#" + Name.Trim() + " "; } }

        private ICollection<PostCategory> PostTags { get; } = new List<PostCategory>();

        [NotMapped]
        public IEnumerable<Post> Posts => PostTags.Select(e => e.Post);
    }


}
