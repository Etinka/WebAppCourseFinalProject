using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
                return RedirectToAction("Error", "Error");

            }

            var post = await _context.Post.Include("PostTags.Category").Include(p => p.Writer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return RedirectToAction("Error", "Error");
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
                post.Writer = await getWriterAsync();
                twitterController.publishTweet(post.Writer.DisplayName + " just posted a new quack titled " + post.Title);
                List<Category> categories = PrepareCategories(SelectedCategories, NewCategories);
                foreach (var category in categories)
                {
                    _context.Add(new PostCategory { Post = post, Category = category });
                }
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
                    if (categoryExists != null)
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
                return RedirectToAction("Error", "Error");
            }

            var post = await _context.Post.Include("PostTags.Category").
                FirstOrDefaultAsync(p => p.Id == id);
            if (post == null)
            {
                return RedirectToAction("Error", "Error");
            }
            CreatePostViewModel createPostViewModel = new CreatePostViewModel(await _context.Category.ToListAsync());
            createPostViewModel.Post = post;
            List<int> cat = new List<int>();

            foreach (var item in post.Categories)
            {
                cat.Add(item.Id);
            }
            createPostViewModel.SelectedCategories = cat;
            return View(createPostViewModel);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Content,VideoLink")] Post post, IEnumerable<int> SelectedCategories, string NewCategories)
        {
            if (id != post.Id)
            {
                return RedirectToAction("Error", "Error");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    List<Category> categories = PrepareCategories(SelectedCategories, NewCategories);
                        foreach (var category in categories)
                        {
                            var pcTag = _context.PostCategory.Where(e => e.CategoryId == category.Id && e.PostId == post.Id)
                                .FirstOrDefault();
                            if (pcTag == null)
                            {
                                var oldPostTag = GetPostTags(post).FirstOrDefault(e => e.Category.Name == category.Name);
                                if (oldPostTag != null)
                                {
                                    GetPostTags(post).Remove(oldPostTag);
                                    GetPostTags(post).Add(new PostCategory { Post = post, Category = category });
                                }
                                GetPostTags(post).Add(new PostCategory { Post = post, Category = category });
                            }

                        }
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.Id))
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
            return View(post);
        }



        private bool DidCategoriesChanged(Post post, List<Category> categories)
        {
            if (post.Categories.Count() != categories.Count() 
                || !post.Categories.Equals(categories))
            {
                foreach (var category in post.Categories)
                {
                    var pcTag = _context.PostCategory.Where(e => e.CategoryId == category.Id && e.PostId == post.Id)
                        .FirstOrDefault();
                    if (pcTag == null)
                    {
                        GetPostTags(post).Remove(pcTag);
                    }
                }
                return true;
            }


            return false;
        }
        // GET: Posts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Error", "Error");
            }

            var post = await _context.Post
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return RedirectToAction("Error", "Error");
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
