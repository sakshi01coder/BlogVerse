using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace BlogVerse.Models
{
    public class ApplicationUser : IdentityUser
    {

        [Required]
        public string Name { get; set; }
    }
}
