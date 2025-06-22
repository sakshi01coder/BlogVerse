using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BlogVerse.Models
{
    public class CreatePostViewModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Category { get; set; }

        [Display(Name = "Upload Image")]
        public IFormFile Image { get; set; }

        [Required]
        public string Content { get; set; }
    }
}
