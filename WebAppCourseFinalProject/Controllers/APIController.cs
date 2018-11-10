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
           //Get info from DB and convert CategoryID to Name 
           var postCountPerCategory =  _context.PostCategory.GroupBy(x => x.Category.Name)
                   .Select(g => new { g.Key, Count = g.Count() });

           var postCountPerCategoryToDic = postCountPerCategory.ToDictionary(x => x.Key, x => x.Count);

            return Json(postCountPerCategoryToDic);
        }
    }
}