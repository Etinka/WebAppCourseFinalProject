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

        public SearchViewModel SearchViewModel { get; set; }

        public HomeViewModel(IEnumerable<Post> _Posts, IEnumerable<Writer> _Writers, IEnumerable<Category> _Categories)
        {
            Posts = _Posts;
            SearchViewModel = new SearchViewModel(_Writers, _Categories);
        }
    }
}
