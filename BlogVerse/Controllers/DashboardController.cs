using Microsoft.AspNetCore.Mvc;
using BlogVerse.Models;
using System.Collections.Generic;

// Alias the Channel model to avoid conflict with System.Threading.Channels
using AppChannel = BlogVerse.Models.Channel;
using BlogVerse.ViewModels;

namespace BlogVerse.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            var model = new DashboardViewModel
            {
                FollowedChannels = GetMockChannels(),
                Posts = GetMockPosts()
            };

            return View(model);
        }
        public IActionResult Account()
        {
            ViewData["Title"] = "Account"; // Optional: Set the page title
            return View(); // This will look for a view named "Account.cshtml" in your Dashboard view folder
        }
        public IActionResult Settings()
        {
            ViewData["Title"] = "Settings";
            return View(); // This will look for Settings.cshtml
        }

        public IActionResult BreakingNews()
        {
            ViewData["Title"] = "Breaking News";
            // Potentially fetch breaking news data here and pass it to the view
            // Example:
            // var breakingNewsList = _newsService.GetBreakingNews();
            // return View(breakingNewsList);
            return View();
        }

        public IActionResult Community()
        {
            var availableCommunities = new List<CommunityModel>()
    {
        new CommunityModel { Id = 1, Name = "Photography Enthusiasts", Description = "Share your best photos and discuss techniques.", ImageUrl = "~/images/Photography.png", MemberCount = 150 },
        new CommunityModel { Id = 2, Name = "Food Lovers United", Description = "A place to share recipes, restaurant reviews, and all things food!", ImageUrl = "~/images/foodie.png", MemberCount = 280 },
        new CommunityModel { Id = 3, Name = "Travel Explorers", Description = "Discuss your travel adventures, share tips, and plan your next trip.", ImageUrl = "~/images/travel.png", MemberCount = 95 },
        new CommunityModel { Id = 4, Name = "Book Worms Corner", Description = "Talk about your favorite books, authors, and literary discussions.", ImageUrl = "~/images/bookwormm.png", MemberCount = 210 }
    };

            ViewBag.AvailableCommunities = availableCommunities;
            return View();
        }

        [HttpPost]
        public IActionResult JoinCommunity(int communityId)
        {
            // In a real application, you would fetch the community from a database or service.
            var community = GetCommunityById(communityId); // Use helper method

            if (community != null)
            {
                TempData["JoinMessage"] = $"You have successfully joined the '{community.Name}' community!";
            }
            else
            {
                TempData["JoinMessage"] = "Error: Community not found!"; // Handle the error case
            }

            return RedirectToAction("Community");
        }
        public IActionResult MyPosts()
        {
            var posts = GetMockPosts(); // Replace with actual DB calls
            return View(posts);
        }

        public IActionResult Analytics()
        {
            var posts = GetMockPosts();
            return View(posts); // You can expand this to include views/likes/etc.
        }

        [HttpPost]
        public IActionResult ToggleFollow(int channelId)
        {
            // Simulated follow/unfollow logic
            // In production, you would check if the user follows this channel in the DB and update it
            return Json(new { success = true, followed = true });
        }

        private List<AppChannel> GetMockChannels() =>
            new List<AppChannel> {
                new AppChannel { Id = 1, Name = "Tech Digest", IsFollowed = true },
                new AppChannel { Id = 2, Name = "Creative Writers", IsFollowed = false },
                new AppChannel { Id = 3, Name = "Design Diaries", IsFollowed = false }
            };

        private List<Post> GetMockPosts() =>
            new List<Post> {
                new Post { Id = 1, Title = "AI Writing Tips", Views = 120, Likes = 25 },
                new Post { Id = 2, Title = "React vs ASP.NET", Views = 80, Likes = 12 }
            };

        // Helper method to get community by ID
        private CommunityModel GetCommunityById(int id)
        {
            var availableCommunities = new List<CommunityModel>() //Ideally this should come from DB
    {
        new CommunityModel { Id = 1, Name = "Photography Enthusiasts", Description = "Share your best photos and discuss techniques.", ImageUrl = "~/images/Photography.png", MemberCount = 150 },
        new CommunityModel { Id = 2, Name = "Food Lovers United", Description = "A place to share recipes, restaurant reviews, and all things food!", ImageUrl = "~/images/foodie.png", MemberCount = 280 },
        new CommunityModel { Id = 3, Name = "Travel Explorers", Description = "Discuss your travel adventures, share tips, and plan your next trip.", ImageUrl = "~/images/travel.png", MemberCount = 95 },
        new CommunityModel { Id = 4, Name = "Book Worms Corner", Description = "Talk about your favorite books, authors, and literary discussions.", ImageUrl = "~/images/bookwormm.png", MemberCount = 210 }
    };
            return availableCommunities.FirstOrDefault(c => c.Id == id);
        }
    }
}
