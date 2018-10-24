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
            return View(await _context.Post.ToListAsync());
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

        public IActionResult Error()
        {
            return View();
        }
    }
}
