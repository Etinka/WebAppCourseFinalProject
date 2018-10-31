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
    public class PostsController : BaseController
    {

        private TwitterController twitterController;

        public PostsController(UserContext context) : base(context)
        {
            this.twitterController = new TwitterController();
        }

        // GET: Posts
        public async Task<IActionResult> Index()
        {
            return View(await _context.Post.ToListAsync());
        }

        // GET: Posts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Post.Include(p => p.Categories).Include(p => p.Writer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }
            ViewData["isAdmin"] = isAdmin() ? 1 : 0;

            return View(post);
        }

        // GET: Posts/Create
        public async Task<IActionResult> Create()
        {
            CreatePostViewModel createPostViewModel = new CreatePostViewModel(await _context.Category.ToListAsync());
            return View(createPostViewModel);
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Content,VideoLink")] Post post, IEnumerable<int> SelectedCategories, string NewCategories)
        {
            if (ModelState.IsValid)
            {
                twitterController.publishTweet("Just a try to post a new quacks");            

                post.Writer = await getWriterAsync();
                post.Categories = PrepareCategories(SelectedCategories, NewCategories);
                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }

        private List<Category> PrepareCategories(IEnumerable<int> SelectedCategories, string NewCategories)
        {
            List<Category> categories = new List<Category>();
            //Add existing categories
            if (SelectedCategories != null)
            {
                foreach (var categoryId in SelectedCategories)
                {
                    //TODO: check if we can do Join here
                    var category = _context.Category.Where(x => x.Id == categoryId).FirstOrDefault();
                    categories.Add(category);
                }
            }

            //Going through New NewCategories
            if (!String.IsNullOrWhiteSpace(NewCategories))
            {
                var newCategoriesArray = NewCategories.Split(',');
                foreach (var name in newCategoriesArray)
                {
                    var categoryExists = _context.Category.Where(x => x.Name == name.Trim()).FirstOrDefault();
                    if(categoryExists != null)
                    {
                        categories.Add(categoryExists);
                    }
                    else
                    {
                        Category category = new Category();
                        category.Name = name.Trim();
                        _context.Add(category);
                        _context.SaveChanges();
                        categories.Add(category);
                    }
                }
            }

            return categories.Distinct().ToList();
        }
        // GET: Posts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Post.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Content,VideoLink")] Post post)
        {
            if (id != post.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.Id))
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
            return View(post);
        }

        // GET: Posts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Post
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _context.Post.FindAsync(id);
            _context.Post.Remove(post);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
            return _context.Post.Any(e => e.Id == id);
        }
    }
}
