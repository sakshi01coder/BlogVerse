using Microsoft.AspNetCore.Mvc;
using BlogVerse.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace BlogVerse.Controllers
{
    [Authorize]
    public class PostsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PostsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Create - GET
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Create - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreatePostViewModel model, string submitButton)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            if (ModelState.IsValid)
            {
                var post = new Post
                {
                    Title = model.Title,
                    Content = model.Content ?? "", // Ensure not null
                    Category = model.Category ?? "", // Ensure not null
                    UserId = userId,
                    CreatedAt = DateTime.Now,
                    IsPublished = submitButton == "Publish",
                    PublishedAt = submitButton == "Publish" ? DateTime.Now : null,
                    
                };

                // Handle image upload (optional)
                if (model.Image != null && model.Image.Length > 0)
                {
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/posts");
                    Directory.CreateDirectory(uploadsFolder); // Ensure the folder exists

                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(model.Image.FileName);
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.Image.CopyToAsync(stream);
                    }

                    post.ImagePath = "/images/posts/" + uniqueFileName;
                }

                _context.Posts.Add(post);
                await _context.SaveChangesAsync();

                return RedirectToAction("MyPosts");
            }

            return View(model);
        }

        // My Posts
        public IActionResult MyPosts()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var myPosts = _context.Posts
                .Where(p => p.UserId == userId && p.IsPublished)
                .OrderByDescending(p => p.CreatedAt)
                .ToList();

            return View(myPosts);
        }

        // Saved Drafts
        public IActionResult SavedDrafts()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var drafts = _context.Posts
                .Where(p => p.UserId == userId && !p.IsPublished)
                .OrderByDescending(p => p.CreatedAt)
                .ToList();

            return View(drafts);
        }

        // Publish Draft
        [HttpPost]
        public async Task<IActionResult> Publish(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var post = await _context.Posts
                .FirstOrDefaultAsync(p => p.Id == id && p.UserId == userId && !p.IsPublished);

            if (post == null)
            {
                return NotFound();
            }

            post.IsPublished = true;
            post.PublishedAt = DateTime.Now;

            _context.Update(post);
            await _context.SaveChangesAsync();

            return RedirectToAction("SavedDrafts");
        }

        // Analytics
        public IActionResult Analytics()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var posts = _context.Posts
                .Where(p => p.UserId == userId && p.IsPublished)
                .ToList();

            return View(posts);
        }

        // Edit - GET
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var post = await _context.Posts
                .FirstOrDefaultAsync(p => p.Id == id && p.UserId == userId);

            if (post == null)
            {
                return NotFound();
            }

            var model = new EditPostViewModel
            {
                Title = post.Title,
                Category = post.Category,
                Content = post.Content,
                ImagePath = post.ImagePath
            };

            return View(model);
        }

        // Edit - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditPostViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var post = await _context.Posts
                    .FirstOrDefaultAsync(p => p.Id == id && p.UserId == userId);

                if (post == null)
                {
                    return NotFound();
                }

                post.Title = model.Title;
                post.Category = model.Category;
                post.Content = model.Content;

                // Handle image update (optional)
                if (model.Image != null && model.Image.Length > 0)
                {
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/posts");
                    Directory.CreateDirectory(uploadsFolder);

                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(model.Image.FileName);
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.Image.CopyToAsync(stream);
                    }

                    post.ImagePath = "/images/posts/" + uniqueFileName;
                }

                _context.Update(post);
                await _context.SaveChangesAsync();

                return RedirectToAction("MyPosts");
            }

            return View(model);
        }

        // Delete - GET
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var post = await _context.Posts
                .FirstOrDefaultAsync(p => p.Id == id && p.UserId == userId);

            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // Delete - POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var post = await _context.Posts
                .FirstOrDefaultAsync(p => p.Id == id && p.UserId == userId);

            if (post == null)
            {
                return NotFound();
            }

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();

            return RedirectToAction("MyPosts");
        }
    }
}
