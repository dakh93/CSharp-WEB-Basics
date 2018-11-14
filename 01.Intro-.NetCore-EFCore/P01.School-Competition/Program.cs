using System;
using System.Collections.Generic;
using System.Linq;

namespace P01.School_Competition
{
    public class Program
    {
        static void Main()
        {
            var students = new Dictionary<string, Dictionary<string, int>>();

            while (true)
            {
                var input = Console.ReadLine();

                if (input == "END")
                {
                    break;
                }

                var curSplit = input.Split(" ");
                var name = curSplit[0];
                var category = curSplit[1];
                var points = int.Parse(curSplit[2]);

                if (!students.ContainsKey(name))
                {
                    students.Add(name, new Dictionary<string, int>());
                    students[name].Add(category, points);
                    continue;
                }

                if (students[name].ContainsKey(category))
                {
                    students[name][category] += points;
                    continue;
                }

                students[name].Add(category, points);

            }

            var ordered =
                students
                    .Select(s => new
                    {
                        Name = s.Key,
                        TotalPoints = s.Value.Values.Sum(),
                        Categories = s.Value.Keys
                    })
                    .OrderByDescending(s => s.TotalPoints)
                    .ThenBy(s => s.Name);


            foreach (var student in ordered)
            {
                var name = student.Name;
                var points = student.TotalPoints;
                var categories = student.Categories.OrderBy(s => s).ToList();

                Console.WriteLine($"{name}: {points} [{string.Join(", ", categories)}]");
            }
        }
    }
}
