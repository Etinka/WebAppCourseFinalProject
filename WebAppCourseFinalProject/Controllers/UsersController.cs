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
            ViewBag.Current = "Login";

            if (isLoggedIn())
            {
                var user = await _context.User.SingleOrDefaultAsync(m => m.ID == getUserId());

                if (user.IsAdmin)
                {
                    //Redirect to writer page
                    var writer = await getWriterAsync(user);

                    return RedirectToAction("Details", "Writers", new { id = writer.Id });
                }
                return RedirectToAction("Details", "Users", new { id = user.ID });
            }

            return View();
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewBag.Current = "Login";

            if (id == null)
            {
                return RedirectToAction("Error", "Error");
            }

            var user = await _context.User.SingleOrDefaultAsync(m => m.ID == id);
            if (user != null)
            {
                ViewData["Login"] = user.FirstName;
            }
            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,FirstName,LastName,Password,Email")] User user)
        {
            if (ModelState.IsValid)
            {
                if (EmailExists(user.Email))
                {
                    ViewData["Error"] = "Email already exists";
                }
                else
                {
                    _context.Add(user);
                    await _context.SaveChangesAsync();
                    setUserLoggedIn(user.ID, user.FirstName, user.IsAdmin);
                    return RedirectToAction("Details", "Users", new { id = user.ID });
                }
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Error", "Error");
            }

            var user = await _context.User.SingleOrDefaultAsync(m => m.ID == id);
            if (user == null)
            {
                return RedirectToAction("Error", "Error");
            }

            ViewBag.ShowDiv = isAdmin();
            return View(user);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,FirstName,LastName,Email,Password,IsAdmin")] User user)
        {
            if (id != user.ID)
            {
                return RedirectToAction("Error", "Error");
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
                        return RedirectToAction("Error", "Error");
                    }
                    else
                    {
                        throw;
                    }
                }
                if (isAdmin())
                {
                    return RedirectToAction("UsersList", "Writers");
                }
                return RedirectToAction("Details", "Users", new { id = user.ID });
            }


            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Error", "Error");
            }

            var user = await _context.User
                .SingleOrDefaultAsync(m => m.ID == id);
            if (user == null)
            {
                return RedirectToAction("Error", "Error");
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
            User user = await _context.User.SingleOrDefaultAsync(m => m.Email == email && m.Password == password);
            //if user is null -> doesn't exist - show error 
            if (user == null)
            {
                ViewData["Error"] = "User or email not found";
            }
            else
            {
                setUserLoggedIn(user.ID, user.FirstName, user.IsAdmin);
                if (user.IsAdmin)
                {
                    //Redirect to writer page
                    var writer = await getWriterAsync(user);

                    return RedirectToAction("Details", "Writers", new { id = writer.Id });
                }
                else
                {
                    return RedirectToAction("Index", "Home", new { id = user.ID });
                }
            }
            return View(user);
        }

        public IActionResult Logout()
        {
            setUserLoggedOut();
            return RedirectToAction("Index", "Home");
        }


        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.ID == id);
        }

        private bool EmailExists(String email)
        {
            return _context.User.Any(m => m.Email == email);
        }

    }
}
