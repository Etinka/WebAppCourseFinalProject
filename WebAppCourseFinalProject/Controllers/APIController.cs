using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAppCourseFinalProject.Models;

namespace WebAppCourseFinalProject.Controllers
{
    [Route("api/")]
    [ApiController]
    public class APIController : BaseController
    {
        public APIController(UserContext context) : base(context) { }

        [HttpGet("post-count")]
        public async Task<IActionResult> GetCategoryPostsCount()
        {
            //TODO: Get data from DB
            // var posts = await _context.Post.Include(p => p.Categories).ToListAsync();        

            var dic = new Dictionary<string, int>
            {
                { "red-duck", 250 },  // category name ,  post count in category
                { "white-duck", 100 },
                { "black-duck", 220 }
            };

            return Json(dic);
        }
    }
}