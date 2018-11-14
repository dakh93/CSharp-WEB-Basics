using System;
using P03.Many_To_Many_Relation.Data;
using P03.Many_To_Many_Relation.Models;

namespace P03.Many_To_Many_Relation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var db = new StudentDbContext();

            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            
        }
    }
}
