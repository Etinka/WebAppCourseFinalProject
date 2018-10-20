using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAppCourseFinalProject.Models;
using Microsoft.AspNetCore.Http;
using WebAppCourseFinalProject.Controllers;

namespace WebAppCourseFinalProject
{
    public class UsersController : BaseController
    {

        public UsersController(UserContext context) : base(context)
        {
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _context.User.ToListAsync());
        }

        // GET: Users/Login
        public async Task<IActionResult> Login()
        {
            ViewData["Login"] = "Login";
            if (isLoggedIn())
            {
                var user = await _context.User.SingleOrDefaultAsync(m => m.ID == getUserId());

                if (user.IsAdmin)
                {
                    //Redirect to writer page
                    var writer = await _context.Writer.SingleOrDefaultAsync(m => m.User.ID == user.ID);
                    return RedirectToAction("Details", "Writers", new { id = writer.Id });
                }
                return RedirectToAction("UserPage", "Users", new { id = user.ID });
            }

            return View();
        }


        // GET: Users/Login
        public async Task<IActionResult> UserPage(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User.SingleOrDefaultAsync(m => m.ID == id);
            if (user != null)
            {
                ViewData["Login"] = user.FirstName;
                if (user.IsAdmin)
                {
                    RedirectToAction("AdminPage", "Users", new { id = id });

                }
            }
            return View(user);
        }


        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .SingleOrDefaultAsync(m => m.ID == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,FirstName,LastName,Password,Email")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User.SingleOrDefaultAsync(m => m.ID == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,FirstName,LastName,Email,Password,IsAdmin")] User user)
        {
            if (id != user.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .SingleOrDefaultAsync(m => m.ID == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.User.SingleOrDefaultAsync(m => m.ID == id);
            _context.User.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = await _context.User.SingleOrDefaultAsync(m => m.Email == email && m.Password == password);
            //if user is null -> doesn't exist - show error 
            if (user == null)
            {
                ViewData["Error"] = "User or email not found";
            }
            else
            {
                setUserLoggedIn(user.ID, user.FirstName);
                if (user.IsAdmin)
                {
                    //Redirect to writer page
                    var writer = await _context.Writer.SingleOrDefaultAsync(m => m.User.ID == user.ID);
                    return RedirectToAction("Details", "Writers", new { id = writer.Id });
                }
                else
                {
                    return RedirectToAction("Index", "Home", new { id = user.ID });
                }
            }
            return View(user);
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.ID == id);
        }
    }
}
