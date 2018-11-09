using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppCourseFinalProject.Models;
using System.Collections;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;

namespace WebAppCourseFinalProject.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(UserContext context) : base(context)
        {
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Current = "Index";
            var allPosts = await _context.Post.ToListAsync();
            var viewModel = new HomeViewModel(await _context.Post.OrderByDescending(i => i.CreatedAt).Take(4).ToListAsync(),
                await _context.Writer.ToListAsync(), await _context.Category.ToListAsync());

            return View(viewModel);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "This is an AWESOME blog about Rubber Duckies!";
            ViewBag.Current = "About";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Do you want to ask something? Suggest something? Please contact our support :)";
            ViewBag.Current = "Contact";
            return View();
        }

        public async Task<IActionResult> Find()
        {
            ViewData["Message"] = "Search For your favorite ducks store!";

            List<Branch> branches = await _context.Branch.ToListAsync();
            var branchesLocations = new float[1];
            var branchesNames = new String[branches.Count * 2];

            for(var i = 0; i<branches.Count; i++)
            {
                branchesLocations.Append(branches[i].Latitude);
                branchesLocations.Append(branches[i].Longtitude);

                branchesNames.Append(branches[i].Title);
                branchesNames.Append(branches[i].Subtitle);
            }

            ViewData["Locations"] = branchesLocations;
            ViewData["Names"] = branchesNames;

            return View();
        }

        public IActionResult Login()
        {
            ViewBag.Current = "Login";

            return RedirectToAction("Login", "Users");

        }

        [HttpPost]
        public async Task<IActionResult> Search(IEnumerable<int> SelectedCategories, int? SelectedWriter,
            DateTime? StartDate, DateTime? EndDate, string FreeSearchText)
        {

            IQueryable<Post> query = _context.Post;

            //Dates
            DateTime start = StartDate ?? new DateTime(1900, 01, 01);
            DateTime end = EndDate ?? DateTime.Now;
            query = query.Where(x => x.CreatedAt.Date <= end.Date && x.CreatedAt >= start.Date);

            //Writer
            if (SelectedWriter != null)
            {
                query = query.Where(x => x.Writer.Id == SelectedWriter);
            }

            //Categories
            if (SelectedCategories != null)
            {
                foreach (var categoryId in SelectedCategories)
                {
                    //TODO: check if we can do Join here
                    var categories = _context.Category.Where(x => x.Id == categoryId).FirstOrDefault();
                    query = query.Where(x => x.Categories.Contains(categories));
                }

            }

            //Free text
            if (FreeSearchText != null)
            {
                query = query.Where(p => p.Title.ToLower().Contains(FreeSearchText.ToLower()) || p.Content.ToLower().Contains(FreeSearchText.ToLower()));
            }

            var viewModel = new SearchViewModel();

            //Add group by 
            if (SelectedWriter == null && SelectedCategories == null)
            {
                var groupedPosts = await query.GroupBy(p => p.Writer.DisplayName).ToListAsync();
                viewModel.GroupedPosts = groupedPosts;
            }
            else
            {
                var posts = await query.OrderByDescending(i => i.CreatedAt).ToListAsync();
                viewModel.Posts = posts;
            }

            viewModel.Writers = await _context.Writer.ToListAsync();
            viewModel.Categories = await _context.Category.ToListAsync();

            return View(viewModel);

        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
