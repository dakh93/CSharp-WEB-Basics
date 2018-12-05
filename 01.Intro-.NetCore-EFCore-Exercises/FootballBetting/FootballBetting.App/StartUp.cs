using System;
using FootballBetting.Data;

namespace FootballBetting.App
{
    public class StartUp
    {
        public static void Main()
        {
            using (var db = new FootballBettingContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
            }
        }
    }
}
