using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FootballBetting.Models
{
    public class Round
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Game> Games { get; set; } = new List<Game>();
    }
}
