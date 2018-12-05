using System;
using FootballBetting.Models;
using Microsoft.EntityFrameworkCore;


namespace FootballBetting.Data
{
    public class FootballBettingContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Bet> Bets { get; set; }
        public DbSet<Continent> Continents { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<CountryContinent> CountriesContinents { get; set; }
        public DbSet<Town> Towns { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<BetGame> BetsGames { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<CompetitionType> CompetitionTypes { get; set; }
        public DbSet<Round> Rounds { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<ResultPrediction> ResultPredictions { get; set; }
        public DbSet<PlayerStatistic> PlayersStatistics { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder.UseSqlServer(Configuration.ConnectionString);
            }
            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //User

            builder
                .Entity<User>()
                .HasMany(u => u.Bets)
                .WithOne(b => b.User)
                .HasForeignKey(b => b.UserId);

            //CountryContinent
            builder
                .Entity<CountryContinent>()
                .HasKey(cc => new {cc.CountryId, cc.ContinentId});


            builder
                .Entity<Continent>()
                .HasMany(c => c.Countries)
                .WithOne(cou => cou.Continent)
                .HasForeignKey(cou => cou.ContinentId);

            builder
                .Entity<Country>()
                .HasMany(c => c.Continents)
                .WithOne(cont => cont.Country)
                .HasForeignKey(cont => cont.ContinentId);

            builder
                .Entity<Country>()
                .HasMany(c => c.Towns)
                .WithOne(t => t.Country)
                .HasForeignKey(t => t.CountryId);

            //Position
            builder
                .Entity<Position>()
                .HasMany(p => p.Players)
                .WithOne(pl => pl.Position)
                .HasForeignKey(pl => pl.PositionId);

            //Town-Team
            builder
                .Entity<Town>()
                .HasMany(t => t.Teams)
                .WithOne(t => t.Town)
                .HasForeignKey(t => t.TownId);

            //Team - Player
            builder
                .Entity<Team>()
                .HasMany(t => t.Players)
                .WithOne(p => p.Team)
                .HasForeignKey(p => p.TeamId);

            //Round - Game
            builder
                .Entity<Round>()
                .HasMany(r => r.Games)
                .WithOne(g => g.Round)
                .HasForeignKey(g => g.RoundId);

            //C-Type - Competitions
            builder
                .Entity<CompetitionType>()
                .HasMany(ct => ct.Competitions)
                .WithOne(c => c.CompetitionType)
                .HasForeignKey(c => c.CompetitionTypeId);

            //Competition - Game
            builder
                .Entity<Competition>()
                .HasMany(c => c.Games)
                .WithOne(g => g.Competition)
                .HasForeignKey(g => g.CompetitionId);

            //Bet - Game
            builder
                .Entity<BetGame>()
                .HasKey(bg => new {bg.BetId, bg.GameId});

            builder.Entity<Bet>()
                .HasMany(b => b.Games)
                .WithOne(g => g.Bet)
                .HasForeignKey(g => g.BetId);

            

            //ResultPrediciton
            builder
                .Entity<ResultPrediction>()
                .HasMany(rp => rp.BetGames)
                .WithOne(bg => bg.ResultPrediction)
                .HasForeignKey(bg => bg.ResultPredictionId);

            //PlayersStatistics
            builder
                .Entity<PlayerStatistic>()
                .HasKey(ps => new {ps.GameId, ps.PlayerId});

            builder
                .Entity<Player>()
                .HasMany(p => p.Games)
                .WithOne(g => g.Player)
                .HasForeignKey(g => g.PlayerId);

            builder
                .Entity<Game>()
                .HasMany(g => g.Players)
                .WithOne(p => p.Game)
                .HasForeignKey(p => p.GameId);

            //Team - Color
            builder
                .Entity<Color>()
                .HasMany(c => c.Teams)
                .WithOne(t => t.PrimaryKitColor)
                .HasForeignKey(t => t.PrimaryColorId);

           

        }
    }
}
