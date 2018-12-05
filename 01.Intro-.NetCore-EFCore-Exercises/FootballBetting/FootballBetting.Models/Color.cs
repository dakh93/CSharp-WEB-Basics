using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FootballBetting.Models
{
    public class Color
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Team> Teams { get; set; } = new List<Team>();
    }
}
