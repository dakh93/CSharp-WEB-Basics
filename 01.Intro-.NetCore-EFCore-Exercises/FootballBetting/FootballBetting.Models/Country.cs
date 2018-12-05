using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FootballBetting.Models
{
    public class Country
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<CountryContinent> Continents { get; set; } = new List<CountryContinent>();

        public ICollection<Town> Towns { get; set; } = new List<Town>();
    }
}
