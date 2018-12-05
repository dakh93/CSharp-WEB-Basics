using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FootballBetting.Models
{
    public class Position
    {
        [Key]
        public int Id { get; set; }

        public string PositionDescription { get; set; }

        public ICollection<Player> Players { get; set; } = new List<Player>();
    }
}