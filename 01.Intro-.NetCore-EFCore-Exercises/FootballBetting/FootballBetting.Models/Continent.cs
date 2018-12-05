using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FootballBetting.Models
{
    public class Continent
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<CountryContinent> Countries { get; set; } = new List<CountryContinent>();
    }
}
