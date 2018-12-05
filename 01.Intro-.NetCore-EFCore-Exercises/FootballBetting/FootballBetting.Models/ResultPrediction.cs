using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using FootballBetting.Models.Enums;

namespace FootballBetting.Models
{
    public class ResultPrediction
    {
        [Key]
        public int Id { get; set; }

        public Prediction Prediction { get; set; }

        public ICollection<BetGame> BetGames { get; set; } = new List<BetGame>();
    }
}
