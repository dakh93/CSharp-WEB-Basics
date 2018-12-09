using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BankSystem.Models
{
    public class User
    {
        public User(string username, string password, string email)
        {
            this.Username = username;
            this.Password = password;
            this.Email = email;
        }

        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z]\w{2,15}$", ErrorMessage = "Username must be atleast with 3 characters and contains only of lowercase letters, uppercase letters and digits !")]
        public string Username { get; set; }

        [Required]
        [RegularExpression(@"\b([a-zA-Z0-9]+\.*\-*_*[a-zA-Z0-9]+)@([a-zA-Z]+\.[a-zA-Z]+)\b", ErrorMessage = "Email does not follow the required pattern !!!")]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{6,}$", ErrorMessage = "Password must must contain at least 1 lowercase letter, 1 uppercase letter and 1 digit. Also, must be more than 6 symbols long.")]
        public string Password { get; set; }

        public bool IsLoggedIn { get; set; } = false;

        public ICollection<SavingAccount> SavingAccounts { get; set; } = new List<SavingAccount>();

        public ICollection<CheckingAccout> CheckingAccouts { get; set; } = new List<CheckingAccout>();


        
    }
}
