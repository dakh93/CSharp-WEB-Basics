
using System;

namespace P02.One_To_Many_Relation
{
    public class Program
    {
        static void Main(string[] args)
        {
            var db = new MyDbContext();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            Console.WriteLine("Database Created!!!");
        }
    }
}
