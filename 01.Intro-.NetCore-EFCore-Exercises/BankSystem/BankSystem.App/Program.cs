using System;
using System.Linq;
using BankSystem.App.Core;
using BankSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var db = new BankSystemDbContext())
            {
                //db.Database.EnsureDeleted();
                //db.Database.EnsureCreated();
                var engine = new Engine();

                engine.Run();
            }
        }
    }
}
