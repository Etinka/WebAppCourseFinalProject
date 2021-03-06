﻿using System;
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
                return RedirectToAction("Error", "Error");
            }

            var writer = await _context.Writer
                .FirstOrDefaultAsync(m => m.Id == id);
            if (writer == null)
            {
                return RedirectToAction("Error", "Error");
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
                return RedirectToAction("Error", "Error");
            }

            var writer = await _context.Writer.FindAsync(id);
            if (writer == null)
            {
                return RedirectToAction("Error", "Error");
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
                return RedirectToAction("Error", "Error");
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
                        return RedirectToAction("Error", "Error");
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
                return RedirectToAction("Error", "Error");
            }

            var writer = await _context.Writer
                .FirstOrDefaultAsync(m => m.User.ID == id);

            var user = await _context.User.FirstOrDefaultAsync(m => m.ID == id);

            if (writer == null && user == null)
            {
                return RedirectToAction("Error", "Error");
            }

            return View(writer);
        }

        // POST: Writers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var writer = await _context.Writer.Where(w => w.User.ID == id).FirstOrDefaultAsync();
            var user = await _context.User.FindAsync(id);

            if (writer == null)
            {
                _context.User.Remove(user);
            }
            else
            {
                var query = await _context.Post.Include(p => p.Writer).Where(p => p.Writer.Id == writer.Id).OrderByDescending(p => p.CreatedAt).ToListAsync();

                foreach (var item in query)
                {
                    var post = await _context.Post.FindAsync(item.Id);
                    var newWriter = await _context.Writer.FirstOrDefaultAsync(m => m.DisplayName == Consts.ANONYMOUS_USER_NAME);

                    post.Writer = newWriter;
                    _context.Update(post);
                }
                _context.Writer.Remove(writer);
                _context.User.Remove(user);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WriterExists(int id)
        {
            return _context.Writer.Any(e => e.Id == id);
        }

        public IActionResult AddPost(int? id)
        {
            return RedirectToAction("Create", "Posts");
        }

        public IActionResult WriterPosts(int id)
        {

            var res = _context.Post.Join(_context.Writer, 
                 post => post.Writer.Id,
                 writer => writer.Id,   
                 (post, writer) => new WritersPostViewModel { post = post, writer = writer }) 
                 .Where(postAndwriter => postAndwriter.post.Writer.Id == id).AsEnumerable().OrderByDescending(postAndwriter => postAndwriter.post.CreatedAt);

            return View( res);
        }

        public async Task<IActionResult> UsersList()
        {
            ViewData["CurrentId"] = getUserId();
            return View(await _context.User.Where(w => w.FirstName != Consts.ANONYMOUS_USER_NAME).ToListAsync());
        }

        public IActionResult Logout()
        {
            setUserLoggedOut();
            return RedirectToAction("Index", "Home");
        }
    }


}
