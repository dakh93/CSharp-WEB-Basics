
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FootballBetting.Models.Enums;

namespace FootballBetting.Models
{

    public class CompetitionType
    {
        [Key]
        public int Id { get; set; }

        public Type Type { get; set; }

        public ICollection<Competition> Competitions { get; set; } = new List<Competition>();
    }
}