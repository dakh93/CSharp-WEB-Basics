using System;
using System.Collections.Generic;
using System.Linq;
using P04.Shop_Hierarchy.Data;
using P04.Shop_Hierarchy.Models;

namespace P04.Shop_Hierarchy
{
    class Program
    {
        static void Main()
        {
            using (var db = new ShopDbContext())
            {
                PrepareDatabase(db);
                FillSalesmenTable(db);
                FillItemTable(db);
                FillCustomerTable(db);
                //PrintSalesmanInfo(db);
                PrintCustomerInfo(db);
                //PrintNumberOfOrderItems(db);
            }
        }


        private static void PrepareDatabase(ShopDbContext db)
        {
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
        }


        private static void FillSalesmenTable(ShopDbContext db)
        {
            var input = Console.ReadLine().Split(";");

            foreach (string currName in input)
            {
                db.Salesmen.Add(new Salesman { Name = currName });
            }
            db.SaveChanges();
        }

        private static void FillCustomerTable(ShopDbContext db)
        {

            while (true)
            {
                var input = Console.ReadLine();

                if (input == "END")
                {
                    break;
                }

                var splitted = input.Split("-");


                var command = splitted[0];
                var arguments = splitted[1];

                switch (command)
                {
                    case "register":
                        RegisterCustomer(db, arguments);
                            break;
                    case "order":
                        MakeOrderByCustomer(db, arguments);
                        break;
                    case "review":
                        MakeReviewByCustomer(db, arguments);
                        break;
                    default:
                        break;
                }

            }

        }

       


        private static void RegisterCustomer(ShopDbContext db, string arguments)
        {
            var parts = arguments.Split(";");
            var custName = parts[0];
            var salesmanId = int.Parse(parts[1]);

            db.Add(new Customer
            {
                Name = custName,
                SalesmanId = salesmanId
            });

            db.SaveChanges();

        }

        private static void PrintSalesmanInfo(ShopDbContext db)
        {
            var result =
                db.Salesmen
                    .Select(s => new
                    {
                        s.Name,
                        CustomerCount = s.Customers.Count
                    })
                    .OrderByDescending(s => s.CustomerCount)
                    .ThenBy(s => s.Name);

            foreach (var sm in result)
            {
                Console.WriteLine($"{sm.Name} - {sm.CustomerCount} customers");
            }
        }

        private static void MakeOrderByCustomer(ShopDbContext db, string arguments)
        {
            var parts = arguments.Split(";");

            var custId = int.Parse(parts[0]);

            var order = new Order {CustomerId = custId};
            for (int i= 1; i < parts.Length; i++)
            {
                var itemId = int.Parse(parts[i]);
                
                order.Items.Add(new ItemOrder
                {
                   ItemId = itemId
                });
            }


            db.Add(order);

            db.SaveChanges();
        }

        private static void MakeReviewByCustomer(ShopDbContext db, string arguments)
        {
            var parts = arguments.Split(new[] {"review", "-", ";"}, StringSplitOptions.RemoveEmptyEntries);

            var custId = int.Parse(parts[0]);
            var itemId = int.Parse(parts[1]);


            db.Add(new Review
            {
                CustomerId = custId,
                ItemId = itemId
            });

            db.SaveChanges();
        }

        private static void PrintCustomerInfo(ShopDbContext db)
        {
            var custID = int.Parse(Console.ReadLine());
            var custInfo = db.Customers
                .Where(c => c.Id == custID)
                .Select(c => new
                {
                    c.Name,
                    OrdersCount = c.Orders.Count,
                    ReviewsCount = c.Reviews.Count,
                    SalesmanName = c.Salesman.Name
                })
                .OrderByDescending(c => c.OrdersCount)
                .ThenByDescending(c => c.ReviewsCount);

            foreach (var cust in custInfo)
            {
                Console.WriteLine(cust.Name);
                Console.WriteLine($"Orders: {cust.OrdersCount}");
                Console.WriteLine($"Reviews: {cust.ReviewsCount}");
                Console.WriteLine($"Salesman: {cust.SalesmanName}");

            }

            var custOrdersMoreThanOne = db
                .Orders
                .Count(o => o.CustomerId == custID && o.Items.Count > 1);

            Console.WriteLine($"Orders with more than 1 item: {custOrdersMoreThanOne}");
        }

        private static void FillItemTable(ShopDbContext db)
        {

            while (true)
            {
                var input = Console.ReadLine();

                if (input == "END")
                {
                    break;
                }


                if (!String.IsNullOrEmpty(input))
                {
                    var parts = input.Split(";");
                    
                    var itemName = parts[0];
                    var itemPrice = decimal.Parse(parts[1]);
                    db.Items.Add(new Item {Name = itemName, Price = itemPrice});
                }

            }
            db.SaveChanges();
        }

        private static void PrintNumberOfOrderItems(ShopDbContext db)
        {
            var custId = int.Parse(Console.ReadLine());

            var custInfo = db
                .Customers
                .Where(c => c.Id == custId)
                .Select(c => new
                {
                    Orders = c
                    .Orders
                    .Select(o => new
                        {
                            o.Id,
                            o.Items.Count
                        })
                        .OrderByDescending(o => o.Count),
                        Reviews = c.Reviews.Count
                    
                })
                .FirstOrDefault();
                

            foreach (var order in custInfo.Orders)
            {
                Console.WriteLine($"order {order.Id}: {order.Count} items");
            }

            

            Console.WriteLine($"reviews: {custInfo.Reviews}");

        }

    }
}
