using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppCourseFinalProject.Models
{
    public class CreatePostViewModel
    {
        public Post Post { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<int> SelectedCategories { get; set; }

        [Display(Name = "Add new categories (seperated by commas)")]
        public string NewCategories { get; set; }

        public CreatePostViewModel(IEnumerable<Category> _Categories)
        {
            Categories = _Categories;
        }
    }
}
