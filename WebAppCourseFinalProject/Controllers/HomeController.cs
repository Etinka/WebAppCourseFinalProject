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
    public class HomeController : Controller
    {
        User loggedInUser = null;
        private readonly UserContext _context;
        public HomeController(UserContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            string logginTab = "Login";

            if (isLoggedIn())
            {
                var user = await _context.User.SingleOrDefaultAsync(m => m.ID == userId());
                if (user != null)
                {
                    logginTab = user.FirstName;
                    loggedInUser = user;

                }

            }
            ViewData["Login"] = logginTab;
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "This is an AWESOME blog about Rubber Duckies!";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Do you want to ask something? Suggest something? Please contact our support :)";

            return View();
        }

        public IActionResult CreateUser()
        {
            ViewData["Message"] = "These are the users";

            return View();
        }

        public IActionResult Login()
        {
            if (isLoggedIn())
            {
                return RedirectToAction("UserPage", "Users", new { id = HttpContext.Session.GetInt32("UserId") });
            }
            else
            {
                return RedirectToAction("Login", "Users");
            }
        }

        public IActionResult Error()
        {
            return View();
        }                                             

        private int? userId()
        {
           return HttpContext.Session.GetInt32("UserId");
        }

        private bool isLoggedIn()
        {
            return HttpContext.Session.GetInt32("LoggedIn") == 1;
        }
    }
}
