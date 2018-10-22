using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using WebAppCourseFinalProject.Models;
using Microsoft.EntityFrameworkCore;

namespace WebAppCourseFinalProject.Controllers
{
    public abstract class BaseController : Controller
    {
        protected readonly UserContext _context;

        public BaseController(UserContext context)
        {
            _context = context;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string logginTab = "Login";
            if (isLoggedIn())
            {
                logginTab = getUserFirstName();
            }
            this.ViewData["Login"] = logginTab;
        }

        protected void setUserLoggedIn(int id, string name, bool admin)
        {
            HttpContext.Session.SetInt32("UserId", id);
            HttpContext.Session.SetInt32("LoggedIn", 1);
            HttpContext.Session.SetString("UserName", name);
            HttpContext.Session.SetInt32("Admin", admin?1:0);
        }

        protected async Task<Writer> getWriterAsync(User user = null)
        {
            var id = 0;
            if (user == null)
            {
                id = (int)getUserId();
            }
            else
            {
                id = user.ID;
            }
            var writer = await _context.Writer.Include(w => w.User).FirstOrDefaultAsync(m => m.User.ID == id);

            if(writer == null && user.IsAdmin)
            {
                writer = new Writer();
                writer.DisplayName = user.FirstName;
                writer.User = user;
                _context.Add(writer);
                await _context.SaveChangesAsync();
            }
            return writer;

        }
        protected int? getUserId()
        {
            return HttpContext.Session.GetInt32("UserId");
        }

        protected string getUserFirstName()
        {
            return HttpContext.Session.GetString("UserName");
        }

        protected bool isLoggedIn()
        {
            if (HttpContext != null)
            {
                int? loggedIn = HttpContext.Session.GetInt32("LoggedIn");
                return loggedIn != null && loggedIn == 1;
            }
            return false;
        }

        protected bool isAdmin()
        {
            if (HttpContext != null)
            {
                int? admin = HttpContext.Session.GetInt32("Admin");
                return admin != null && admin == 1;
            }
            return false;
        }
    }
}