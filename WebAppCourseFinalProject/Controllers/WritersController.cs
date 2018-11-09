using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAppCourseFinalProject.Controllers;
using WebAppCourseFinalProject.Models;

namespace WebAppCourseFinalProject
{
    public class WritersController : BaseController
    {

        public WritersController(UserContext context) : base(context)
        {
        }

        // GET: Writers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Writer.ToListAsync());
        }

        // GET: Writers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var writer = await _context.Writer
                .FirstOrDefaultAsync(m => m.Id == id);
            if (writer == null)
            {
                return NotFound();
            }

            return View(writer);
        }

        // GET: Writers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Writers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DisplayName,Description")] Writer writer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(writer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(writer);
        }

        // GET: Writers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var writer = await _context.Writer.FindAsync(id);
            if (writer == null)
            {
                return NotFound();
            }
            return View(writer);
        }

        // POST: Writers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DisplayName,Description")] Writer writer)
        {
            if (id != writer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(writer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WriterExists(writer.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(writer);
        }

        // GET: Writers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var writer = await _context.Writer
                .FirstOrDefaultAsync(m => m.Id == id);
            if (writer == null)
            {
                return NotFound();
            }

            return View(writer);
        }

        // POST: Writers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var writer = await _context.Writer.FindAsync(id);
            _context.Writer.Remove(writer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WriterExists(int id)
        {
            return _context.Writer.Any(e => e.Id == id);
        }

        public IActionResult AddPost(int? id)
        {
            return RedirectToAction("Create", "Posts" );
        }

        public async Task<IActionResult> WriterPosts(int id)
        {
            var query = (from post in _context.Post
                         where post.Writer.Id == id
                         select post).ToListAsync();
            return View(await query);
        }

        public async Task<IActionResult> UsersList()
        {
            return View(await _context.User.ToListAsync());
        }

        public IActionResult Logout()
        {
            setUserLoggedOut();
            return RedirectToAction("Index", "Home");
        }
    }

  
}
