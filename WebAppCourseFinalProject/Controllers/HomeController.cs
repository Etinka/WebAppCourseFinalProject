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
            //viewModel.Writers = allPosts.ToSelectListItems(selectedId);

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

        public IActionResult Login()
        {
            ViewBag.Current = "Login";

            return RedirectToAction("Login", "Users");

        }

        [HttpPost]
        public string Search(IEnumerable<string> SelectedCategories, int? SelectedWriter, DateTime? start_date, DateTime? end_date)
        {
            //TODO add the actual search
            if (SelectedCategories == null)
            {
                return "No cities are selected";
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("You selected – " + string.Join(",", SelectedCategories));
                return sb.ToString();
            }
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
