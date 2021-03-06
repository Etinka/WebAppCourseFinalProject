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

            //create an anonymous user as writer in order to be able to delete a writer.
            //when we delete a writer- all the posts that he wrote changes to a writer named "anonymous"
            var anonymousWriter = await _context.Writer.Where(w => w.DisplayName == Consts.ANONYMOUS_USER_NAME).FirstOrDefaultAsync();
            if (anonymousWriter == null)
            {
                var anonymousUser = await _context.User.Where(w => w.FirstName == Consts.ANONYMOUS_USER_NAME).FirstOrDefaultAsync();

                if (anonymousUser == null)
                {
                    anonymousUser = new User();
                    anonymousUser.FirstName = Consts.ANONYMOUS_USER_NAME;
                    anonymousUser.LastName = Consts.ANONYMOUS_USER_NAME;
                    anonymousUser.Email = "anonymous@gmail.com";
                    anonymousUser.Password = "123456789";
                    anonymousUser.IsAdmin = true;
                }

                anonymousWriter = new Writer();
                anonymousWriter.DisplayName = Consts.ANONYMOUS_USER_NAME;
                anonymousWriter.User = anonymousUser;
                _context.Add(anonymousWriter);
                await _context.SaveChangesAsync();
            }

            return View(viewModel);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "This is an AWESOME blog about Rubber Duckies!";
            ViewBag.Current = "About";

            return View();
        }

        public JsonResult AboutInfo()
        {
            About about = new About("this is an awesome blog about rubber duckies!", "College of management", "ducks@duckesrule.com");
            return Json(about);
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

            return View(await _context.Branch.ToListAsync());
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

            IQueryable<Post> query = _context.Post.Include("PostTags.Category");

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
                    query = query.Where(x => x.PostTags.Any(t => t.CategoryId == categoryId));
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
