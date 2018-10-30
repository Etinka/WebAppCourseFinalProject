using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppCourseFinalProject.Models
{
    public class HomeViewModel
    {
        public IEnumerable<Post> Posts { get; set; }
        public IEnumerable<Writer> Writers { get; set; }
        public IEnumerable<Category> Categories { get; set; }

        public int? SelectedWriter { get; set; }
        public IEnumerable<int> SelectedCategories { get; set; }

        public HomeViewModel(IEnumerable<Post> _Posts, IEnumerable<Writer> _Writers, IEnumerable<Category> _Categories)
        {
            Posts = _Posts;
            Writers = _Writers;
            Categories = _Categories;
        }
    }
}
