
using System;
using System.Collections.Generic;
using System.Net.Mime;

namespace SocialNetwork.Models
{
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 4, ErrorMessage = "The Username length should be between 4 and 30 characters!!!")]
        public string Username { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{6,50}$", ErrorMessage = "Minimum 6 characters required")]
        public string Password { get; set; }

        [Required]
        [RegularExpression(@"\b([a-zA-Z0-9]+\.*\-*_*[a-zA-Z0-9]+)@([a-zA-Z]+\.[a-zA-Z]+)\b", ErrorMessage = "Email does not follow the required pattern !!!")]
        public string Email { get; set; }


        public byte[] ProfilePicture { get; set; } = new byte[100];

        public DateTime RegisteredOn { get; set; }

        public DateTime LastTimeLoggedIn { get; set; }
        
        [Range(1, 120)]
        public int Age { get; set; }

        public bool Deleted { get; set; }

        public List<UserFriends> Friends { get; set; } = new List<UserFriends>();

        public List<Album> Albums { get; set; } = new List<Album>();
    }
}
