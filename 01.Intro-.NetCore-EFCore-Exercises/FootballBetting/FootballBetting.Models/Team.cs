using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace FootballBetting.Models
{
    public class Team
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Logo { get; set; }

        public string Initials { get; set; }

        public Color PrimaryKitColor { get; set; }
        public int PrimaryColorId { get; set; }

        public Color SecondaryKitColor { get; set; }
        public int SecondaryColorId { get; set; }


        public decimal Budget { get; set; }

        public int TownId { get; set; }
        public Town Town { get; set; }

        public ICollection<Player> Players { get; set; } = new List<Player>();
    }
}