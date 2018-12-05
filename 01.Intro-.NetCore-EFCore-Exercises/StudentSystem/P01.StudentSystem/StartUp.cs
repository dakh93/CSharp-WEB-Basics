using System;
using System.Collections.Generic;
using System.Linq;
using StudentSystem.Data;

namespace StudentSystem
{
    public class StartUp
    {
        private static Random random = new Random();

        public static void Main(string[] args)
        {

            using (var db = new StudentSystemDbContext())
            {
                //db.Database.EnsureDeleted();
                //db.Database.EnsureCreated();

                //SeedInitial(db);
                //SeedLicenses(db);

                //PrintStudentHomeworkSubmissions(db);

                //PrintCoursesInfo(db);

                //PrintCoursesWithMoreThanFiveResources(db);

                //PrintCoursesActiveOnGivenDate(db);

                //PrintStudentInfo(db);

                //PrintCourseResourcesAndLicenses(db);

                //PrintStudentFullInfo(db);

            }
        }


        private static void SeedInitial(StudentSystemDbContext db)
        {
            const int totalStudents = 50;
            const int totalCourses = 15;
            var currDate = DateTime.Now;

            // Fill Student Table
            for (int i = 0; i < totalStudents; i++)
            {

                db.Students.Add(new Student
                {
                    Name = $"Student{i}",
                    RegistrationDate = currDate.AddDays(i),
                    Birthday = currDate.AddYears(-20).AddDays(i),
                    PhoneNumber = $"Random Number {i}"
                });
            }
            db.SaveChanges();
            Console.WriteLine("Students Added...");

            //Fill Course Table
            var basePrice = 50;
            var addedCourses = new List<Course>();

            for (int i = 0; i < totalCourses; i++)
            {
                var currCourse = new Course()
                {
                    Name = $"Level {i}",
                    Description = $"Description for level {i}",
                    Price = basePrice + ((i + 5) * 2),
                    StartDate = currDate.AddDays(i),
                    EndDate = currDate.AddDays(i + 30)
                };

                addedCourses.Add(currCourse);
                db.Courses.Add(currCourse);

            }
            db.SaveChanges();
            Console.WriteLine("Courses Added....");

            //Students in Courses
            var studentIds = db
                .Students
                .Select(s => s.Id)
                .ToList();

            for (int i = 0; i < totalCourses; i++)
            {
                var currCourse = addedCourses[i];
                var studInCourse = random.Next(2, totalStudents / 2);

                for (int j = 0; j < studInCourse; j++)
                {
                    var studId = studentIds[random.Next(0, studentIds.Count)];

                    if (currCourse.Students.All(s => s.StudentId != studId))
                    {

                        currCourse.Students.Add(new StudentCourse
                        {
                            StudentId = studId
                        });
                    }
                    else
                    {
                        j--;
                    }
                }

                var resourceInCourse = random.Next(3, 10);
                var types = new[] {1, 2, 3, 999};

                for (int j = 0; j < resourceInCourse; j++)
                {
                    
                    currCourse.Resources.Add(new Resource
                    {
                        Name = $"Resource {j}",
                        ResourceType = (ResourceType) types[random.Next(1, types.Length)],
                        Url = $"URL {j}",

                    });
                }

            }

            db.SaveChanges();

            // Fill Homework Table
            for (int i = 0; i < totalCourses; i++)
            {
                var currCourse = addedCourses[i];

                var studentInCourseIDs = currCourse
                    .Students
                    .Select(s => s.StudentId)
                    .ToList();

                for (int j = 0; j < studentInCourseIDs.Count; j++)
                {
                    var totalHomework = random.Next(2, 6);

                    for (int k = 0; k < totalHomework; k++)
                    {
                        db.Homeworks.Add(new Homework
                        {
                            Content = $"Content Homework {i}",
                            SubmissionDate = currDate.AddDays(-i),
                            ContentType = FileType.Zip,
                            StudentId = studentInCourseIDs[j],
                            CourseId = currCourse.Id
                        });
                    }
                }
            }
            db.SaveChanges();
            Console.WriteLine("Homeworks Added...");
            Console.WriteLine("The data is seeded to database...");
        }

        private static void SeedLicenses(StudentSystemDbContext db)
        {
            var resourcesIds = db
                .Resources
                .Select(r => r.Id)
                .ToList();

            for (int i = 0; i < resourcesIds.Count; i++)
            {
                var totalLicenses = random.Next(2, 5);

                for (int j = 0; j < totalLicenses; j++)
                {
                    db.Licenses.Add(new License
                    {
                        Name = $"License {i} {j}",
                        ResourceId = resourcesIds[i]
                    });
                }
            }
            db.SaveChanges();
            Console.WriteLine("Licenses Added...");
        }

        private static void PrintStudentHomeworkSubmissions(StudentSystemDbContext db)
        {
            var studentsContent = db
                .Students
                .Select(s => new
                {
                    s.Name,
                    HomeworkInfo = s.Homeworks
                        .Select(sh => new
                        {
                            sh.Content,
                            sh.ContentType
                        })
                })
                .ToList();

            foreach (var student in studentsContent)
            {
                Console.WriteLine($"{student.Name}");
                foreach (var hw in student.HomeworkInfo)
                {
                    Console.WriteLine(new string(' ', 5));
                    Console.WriteLine($"Content: {hw.Content}");
                    Console.WriteLine($"Content Type: {hw.ContentType}");
                }
                Console.WriteLine(new string('-', 50));
            }
        }

        private static void PrintCoursesInfo(StudentSystemDbContext db)
        {
            var courses = db
                .Courses
                .OrderBy(c => c.StartDate)
                .ThenByDescending(c => c.EndDate)
                .Select(c => new
                {
                    c.Name,
                    c.Description,
                    Resources = c.Resources.Select(cr => new
                    {
                        cr.Name,
                        cr.ResourceType,
                        cr.Url
                    })
                })
                .ToList();

            foreach (var course in courses)
            {
                Console.WriteLine($"Course Name: {course.Name}");
                Console.WriteLine($"Description: {course.Description}");

                foreach (var resource in course.Resources)
                {
                    Console.WriteLine(new string('-', 5));
                    Console.WriteLine($"Resource Name: {resource.Name}");
                    Console.WriteLine($"Resource Type: {resource.ResourceType}");
                    Console.WriteLine($"Resource URL: {resource.Url}");
                    Console.WriteLine(new string('-', 5));
                }
                Console.WriteLine(new string('*', 20));
            }
        }


        private static void PrintCoursesWithMoreThanFiveResources(StudentSystemDbContext db)
        {
            var courses = db
                .Courses
                .Where(c => c.Resources.Count > 5)
                .OrderByDescending(c => c.Resources.Count)
                .ThenByDescending(c => c.StartDate)
                .Select(c => new
                {
                    c.Name,
                    ResourceCnt = c.Resources.Count
                })
                .ToList();

            foreach (var course in courses)
            {
                Console.WriteLine($"Course name: {course.Name}");
                Console.WriteLine($"Resources count: {course.ResourceCnt}");
                Console.WriteLine(new string('*', 20));

            }
        }


        private static void PrintCoursesActiveOnGivenDate(StudentSystemDbContext db)
        {
            var givenDate = DateTime.Now.AddDays(-15);

            var courses = db
                .Courses
                .Where(c => c.StartDate > givenDate)
                .Select(c => new
                {
                    c.Name,
                    c.StartDate,
                    c.EndDate,
                    Duration = c.EndDate.Subtract(c.StartDate),
                    StudentsCnt = c.Students.Count
                })
                .OrderByDescending(c => c.StudentsCnt)
                .ThenByDescending(c => c.Duration)
                .ToList();

            foreach (var c in courses)
            {
                
                Console.WriteLine($"Course name: {c.Name}");
                Console.WriteLine($"Start date: {c.StartDate}");
                Console.WriteLine($"End date: {c.EndDate}");
                Console.WriteLine($"Duration: {c.Duration}");
                Console.WriteLine($"Enrolled students: {c.StudentsCnt}");

                Console.WriteLine(new string('*', 20));



            }
        }


        private static void PrintStudentInfo(StudentSystemDbContext db)
        {
            var students = db
                .Students
                .Select(s => new
                {
                    s.Name,
                    CoursesCnt = s.Courses.Count,
                    TotalPrice = s.Courses
                        .Select(c => c.Course.Price).Sum(),
                    AveragePrice = s.Courses
                        .Select(c => c.Course.Price).Average()
                })
                .OrderByDescending(s => s.TotalPrice)
                .ThenByDescending(s => s.CoursesCnt)
                .ThenBy(s => s.Name)
                .ToList();

            foreach (var s in students)
            {
                Console.WriteLine($"Name: {s.Name}");
                Console.WriteLine($"Courses number: {s.CoursesCnt}");
                Console.WriteLine($"Total price: {s.TotalPrice}");
                Console.WriteLine($"Average price: {s.AveragePrice}");

                Console.WriteLine(new string('*', 20));
            }

        }

        private static void PrintCourseResourcesAndLicenses(StudentSystemDbContext db)
        {
            var courses = db
                .Courses
                .Select(c => new
                {
                    c.Name,
                    Resources = c.Resources.Select(r => new
                        {
                            r.Name,
                            r.Licenses
                        })
                        .OrderByDescending(r => r.Licenses.Count)
                        .ThenBy(r => r.Name)
                })
                .OrderByDescending(c => c.Resources.Count())
                .ThenBy(c => c.Name)
                .ToList();

            foreach (var course in courses)
            {
                Console.WriteLine($"Course name: {course.Name}");

                foreach (var r in course.Resources)
                {
                    Console.Write(new string('-',2));
                    Console.WriteLine($"Resource name: {r.Name}");

                    foreach (var l in r.Licenses)
                    {
                        Console.Write(new string('-', 4));
                        Console.WriteLine($"License name: {l.Name}");
                    }
                }
            }
        }


        private static void PrintStudentFullInfo(StudentSystemDbContext db)
        {
            var students = db
                .Students
                .Select(s => new
                {
                    s.Name,
                    CoursesCnt = s.Courses.Count,
                    TotalResourcesCnt = s.Courses.Select(c => c.Course.Resources.Count).Sum(),
                    TotalLicensesCnt = s.Courses
                        .Select(c => c.Course.Resources
                            .Select(cr => new
                            {
                                cr.Licenses.Count
                            })).Count()
                });

            foreach (var s in students)
            {
                Console.WriteLine($"Student name: {s.Name}");
                Console.WriteLine($"Course count: {s.CoursesCnt}");
                Console.WriteLine($"Total resources: {s.TotalResourcesCnt}");
                Console.WriteLine($"Total licenses: {s.TotalLicensesCnt}");
            }
        }

    }
}
