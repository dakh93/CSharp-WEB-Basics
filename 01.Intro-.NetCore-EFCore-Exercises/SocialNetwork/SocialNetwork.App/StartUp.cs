using System;
using System.Collections.Generic;
using System.Linq;
using SocialNetwork.Data;
using SocialNetwork.Models;


namespace SocialNetwork.App
{
    public class StartUp
    {
        public static DateTime date = DateTime.Now;
        public static Random random = new Random();
        public static int userCount = 20;

        public static void Main()
        {
            using (var db = new SocialNetworkDbContext())
            {
                //db.Database.EnsureDeleted();
                //db.Database.EnsureCreated();

                //SeedInitial(db);
                //SeedAlbumsAndPictures(db);

                //PrintAllUsersWithCountOfFriends(db);

                //PrintAllActiveUsersWithMoreThanThreeFriends(db);

                //PrintAllAlbumInfo(db);

                //PrintPicturesExistingInMoreThanTwoAlbums(db);

                //PrintAlbumsInfoForGivenUserId(db);

            }
        }

        private static void SeedInitial(SocialNetworkDbContext db)
        {

            for (int i = 1; i <= userCount; i++)
            {
                var deletedStatus = i % 2 == 0 ? 1 : 0;

                var currUser = new User
                {
                    Username = $"Username {i}",
                    Password = $"Password {i}",
                    Email = $"user{i}@abv.bg",
                    RegisteredOn = date.AddDays(-10),
                    LastTimeLoggedIn = date.AddDays(-5),
                    Age = 25,
                    Deleted = Convert.ToBoolean(deletedStatus)
                };
                db.Users.Add(currUser);
            }
            db.SaveChanges();


            for (int i = 1; i <= userCount; i++)
            {
                var currUserId = i;

                var friendsToAdd = random.Next(2, 5);

                var addedIds = new List<int>();
                for (int j = 0; j < friendsToAdd; j++)
                {
                    var currFriendId = random.Next(1, 21);
                
                    if (!addedIds.Contains(currFriendId))
                    {
                        addedIds.Add(currFriendId);
                        db.UserFriends.Add(new UserFriends
                        {
                            UserId = currUserId,
                            FriendId = currFriendId
                        });
                    }
                    else
                    {
                        j--;
                    }
                    db.SaveChanges();
                }
            }
        }

        private static void PrintAllUsersWithCountOfFriends(SocialNetworkDbContext db)
        {
            var users = db
                .Users
                .Select(u => new
                {
                    u.Username,
                    FriendsCnt = u.Friends.Count,
                    Status = u.Deleted
                })
                .ToList();

            foreach (var u in users)
            {
                Console.WriteLine($"Username: {u.Username}");
                Console.WriteLine($"Friends Count: {u.FriendsCnt}");
                Console.WriteLine(u.Status ? "Active" : "Inactive");
            }
        }


        private static void PrintAllActiveUsersWithMoreThanThreeFriends(SocialNetworkDbContext db)
        {
            var activeUsers = db
                .Users
                .Where(u => !u.Deleted && u.Friends.Count > 3)
                .OrderBy(u => u.RegisteredOn)
                .ThenBy(u => u.Friends.Count)
                .Select(u => new
                {
                    u.Username,
                    FriendsCnt = u.Friends.Count,
                    Period = date.Subtract(u.RegisteredOn).Days
                })
                .ToList();

            foreach (var u in activeUsers)
            {
                Console.WriteLine($"Username: {u.Username}");
                Console.WriteLine($"Friends count: {u.FriendsCnt}");
                Console.WriteLine($"Registered before {u.Period} days");

                Console.WriteLine(new string('*',15));
            }
        }


        private static void SeedAlbumsAndPictures(SocialNetworkDbContext db)
        {

            int pictureCnt = 50;
            for (int i = 0; i < pictureCnt; i++)
            {
                db.Pictures.Add(new Picture
                {
                    Title = $"Title {i}",
                    Caption = $"Caption {i}",
                    Path = $@"D:\\dakh\directory{i}"
                });
            }
            db.SaveChanges();
            Console.WriteLine("Pictures Added...");

            int albumCnt = 60;
            
            for (int i = 1; i <= userCount; i++)
            {
                int currUserAlbumId = i;
                var isPublic = Convert.ToBoolean(i % 2 == 0 ? 1 : 0);
                var addedAlbumIds = new List<int>();
                for (int j = 0; j < 3; j++)
                {
                    var randomAlbumId  = random.Next(1, albumCnt + 1);

                    if (!addedAlbumIds.Contains(randomAlbumId))
                    {
                        db.Albums.Add(new Album
                        {
                            Name = $"Album {i}",
                            IsPublic = isPublic,
                            BackgroundColor = $"Color {i}",
                            UserId = currUserAlbumId

                        });
                    }
                    
                }

            }
            db.SaveChanges();
            Console.WriteLine("Albums Added...");


            for (int i = 1; i <= albumCnt; i++)
            {
                int currAlbumId = i;

                var picturesToAdd = random.Next(2, pictureCnt / 3);
                var addedPictureIds = new List<int>();
                for (int j = 0; j < picturesToAdd; j++)
                {
                    var currPictureId = random.Next(1, pictureCnt + 1);
                    if (!addedPictureIds.Contains(currPictureId))
                    {
                        db.PictureAlbums.Add(new PictureAlbums
                        {
                            AlbumId = currAlbumId,
                            PictureId = currPictureId
                        });
                        addedPictureIds.Add(currPictureId);
                    }
                }
            }
            db.SaveChanges();
            Console.WriteLine("PictureAlbums Created...");





        }


        private static void PrintAllAlbumInfo(SocialNetworkDbContext db)
        {
            var albums = db
                .Albums
                .Select(a => new
                {
                    OwnerName = a.User.Username,
                    PictureCnt = a.Pictures.Count,
                    a.Name,
                })
                .OrderByDescending(a => a.PictureCnt)
                .ThenBy(a => a.OwnerName)
                .ToList();

            foreach (var a in albums)
            {
                Console.WriteLine($"Album name: {a.Name}");
                Console.WriteLine($"Owner name: {a.OwnerName}");
                Console.WriteLine($"Pictures count: {a.PictureCnt}");

                Console.WriteLine(new string('*', 20));

            }
        }

        private static void PrintPicturesExistingInMoreThanTwoAlbums(SocialNetworkDbContext db)
        {
            var pictures = db
                .Pictures
                .Where(p => p.Albums.Count > 2)
                .OrderByDescending(p => p.Albums.Count)
                .Select(p => new
                {
                    p.Title,
                    AlbumNamesOwners = p.Albums.Select(a => new
                    {
                        a.Album.Name,
                        a.Album.User.Username
                    }),

                })
                .OrderBy(p => p.Title)
                .ToList();

            foreach (var p in pictures)
            {
                Console.WriteLine($"Title: {p.Title}");
                foreach (var a in p.AlbumNamesOwners)
                {
                    Console.WriteLine($"Album name: {a.Name}");
                    Console.WriteLine($"Owner name: {a.Username}");
                }
                Console.WriteLine(new string('*', 20));
            }
        }


        private static void PrintAlbumsInfoForGivenUserId(SocialNetworkDbContext db)
        {
            Console.Write("Please enter user id: ");
            var userId = int.Parse(Console.ReadLine());

            var userAlbumInfo = db
                .Users
                .Where(u => u.Id == userId)
                .Select(u => new
                {
                    u.Username,
                    Albums = u.Albums.Select(a => new
                    {
                        a.Name,
                        a.IsPublic,
                        PictureTitles = a.Pictures.Select(p => new
                        {
                            p.Picture.Title,
                            p.Picture.Path,
                        })
                    })
                })
                .OrderBy(u => u.Albums.Select(a => a.Name))
                .ToList();

            foreach (var a in userAlbumInfo)
            {
                Console.WriteLine($"Username: {a.Username}");

                foreach (var album in a.Albums)
                {
                    Console.WriteLine($"Album name: {album.Name}");

                    if (album.IsPublic)
                    {
                        foreach (var p in album.PictureTitles)
                        {
                            Console.WriteLine($"Picture title: {p.Title}");
                            Console.WriteLine($"Picture path: {p.Path}");
                        }
                        Console.WriteLine(new string('*', 20));
                    }
                    else
                    {
                        Console.WriteLine("Private Content!");
                    }
                }
            }


        }


    }
}
