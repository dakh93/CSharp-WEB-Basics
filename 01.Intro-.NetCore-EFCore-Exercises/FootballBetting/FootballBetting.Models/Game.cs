using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FootballBetting.Models
{
    public class Game
    {
        [Key]
        public int Id { get; set; }

        public Team HomeTeam { get; set; }

        public Team AwayTeam { get; set; }

        public int HomeTeamGoals { get; set; }

        public int AwayTeamGoals { get; set; }

        public decimal HomeTeamWinBetRate { get; set; }

        public decimal AwayTeamWinBetRate { get; set; }

        public decimal DrawGameWinBetRate { get; set; }

        public DateTime DateTime { get; set; }

        public int RoundId { get; set; }
        public Round Round { get; set; }

        public int CompetitionId { get; set; }
        public Competition Competition { get; set; }

        public ICollection<BetGame> Bets = new List<BetGame>();

        public ICollection<PlayerStatistic> Players { get; set; } = new List<PlayerStatistic>();
    }
}
