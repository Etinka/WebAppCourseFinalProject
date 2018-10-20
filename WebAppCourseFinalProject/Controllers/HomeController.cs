﻿using System;
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

            return RedirectToAction("Login", "Users");

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
