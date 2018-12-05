using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FootballBetting.Models
{
    public class Bet
    {
        [Key]
        public int Id { get; set; }

        public decimal BetMoney { get; set; }

        public DateTime DateTime { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<BetGame> Games { get; set; } = new List<BetGame>();

    }
}
