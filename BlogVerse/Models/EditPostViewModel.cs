using System.ComponentModel.DataAnnotations;

namespace BlogVerse.Models
{
    public class EditPostViewModel
    {
        // Post ID is required for editing purposes
        public int Id { get; set; }

        // Title of the post
        [Required(ErrorMessage = "Title is required.")]
        [StringLength(100, ErrorMessage = "Title cannot be longer than 100 characters.")]
        public string Title { get; set; }

        // Category of the post
        [Required(ErrorMessage = "Category is required.")]
        public string Category { get; set; }

        // Content of the post
        [Required(ErrorMessage = "Content is required.")]
        public string Content { get; set; }

        // Optional image path for the post (if applicable)
        public string? ImagePath { get; set; }

        // Published status (if the post should be published)
        public bool IsPublished { get; set; }

        // Option to upload a new image for the post
        public IFormFile? Image { get; set; }
    }
}
