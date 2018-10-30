using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppCourseFinalProject.Models
{
    public class SearchViewModel
    {

        public IEnumerable<Writer> Writers { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Post> Posts { get; set; }
        public List<IGrouping<string, Post>> GroupedPosts { get; set; }

        public int? SelectedWriter { get; set; }
        public IEnumerable<int> SelectedCategories { get; set; }

        public SearchViewModel() { }
        public SearchViewModel(IEnumerable<Writer> _Writers, IEnumerable<Category> _Categories, IEnumerable<Post> _Posts)
        {
            Writers = _Writers;
            Categories = _Categories;
            Posts = _Posts;
        }

        public SearchViewModel(IEnumerable<Writer> _Writers, IEnumerable<Category> _Categories) : this(_Writers, _Categories, new List<Post>()) { }

        public SearchViewModel(IEnumerable<Writer> _Writers, IEnumerable<Category> _Categories, List<IGrouping<string, Post>> _GroupedPosts) : this(_Writers, _Categories)
        {
            this.GroupedPosts = _GroupedPosts;
        }
    }
}
