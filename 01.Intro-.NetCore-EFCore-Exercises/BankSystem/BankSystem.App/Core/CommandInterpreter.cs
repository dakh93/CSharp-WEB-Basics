using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BankSystem.Data;
using BankSystem.Models;
using BankSystem.Services;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.App.Core
{

    public class CommandInterpreter
    {
        private static Random random = new Random();

        private static char[] alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
            .ToCharArray();

        private static char[] digits = "1234567890"
            .ToCharArray();


        private static readonly BankSystemDbContext db = new BankSystemDbContext();

        public CommandInterpreter() { }



        public void GetInputInfo(string input)
        {
            db.Database.Migrate();



            var command = input.Split(' ').First().ToString();
            var data = input?.Split(' ', StringSplitOptions.RemoveEmptyEntries).Skip(1).ToList();

            if (command == "Add")
            {
                command = input.Split(' ').Skip(1).First();
                data = data.Skip(1).ToList();
            }


            switch (command)
            {
                case "Register":
                    RegisterUser(data);
                    break;
                case "Login":
                    LoginUser(data);
                    break;
                case "Logout":
                    LogoutUser();
                    break;
                case "SavingAccount":
                    AddSavingAccount(data);
                    break;
                case "CheckingAccount":
                    AddCheckingAccount(data);
                    break;
                case "Deposit":
                    DepositToAcc(data);
                    break;
                case "Withdraw":
                    Withdraw(data);
                    break;
                case "AddInterest":
                    AddInterest(data);
                    break;
                case "DeductFee":
                    DeductFee(data);
                    break;
                case "ListAccounts":
                    ListAccountsInfo();
                    break;
            }

        }

        private void ListAccountsInfo()
        {
            var user = GetCurrentLoggedUser();

            var savAcc = user.SavingAccounts
                .ToList();

            var checkAcc = user.CheckingAccouts
                .ToList();


            PrintSavingAccounts(savAcc);
            PrintCheckingAccounts(checkAcc);
        }

        private void PrintCheckingAccounts(List<CheckingAccout> checkAcc)
        {
            Console.WriteLine($"Checking Accounts:");
            foreach (var a in checkAcc)
            {
                Console.WriteLine($"--{a.AccountNumer} {a.Balance}");
            }
        }

        private void PrintSavingAccounts(List<SavingAccount> savAcc)
        {
            Console.WriteLine($"Saving Accounts:");
            foreach (var a in savAcc)
            {
                Console.WriteLine($"--{a.AccountNumer} {a.Balance}");
            }
        }

        private void DeductFee(List<string> data)
        {
            var accNum = data[0];

            if (!CheckForLoggedUser())
            {
                Console.WriteLine(Messages.NoLoggedUser);
                return;
            }

            var user = GetCurrentLoggedUser();
            if (!CheckForAccountNumber(user, accNum))
            {
                Console.WriteLine(Messages.IncorrectBankAccount);
                return;
            }

            user.CheckingAccouts.FirstOrDefault(s => s.AccountNumer == accNum).DeductFee();

            var balance = user.CheckingAccouts.FirstOrDefault(sa => sa.AccountNumer == accNum).Balance;

            db.Update(user);
            db.SaveChanges();
            Console.WriteLine($"{Messages.DeductedFee}", accNum, balance);
        }

        private void AddInterest(List<string> data)
        {
            var accNum = data[0];

            if (!CheckForLoggedUser())
            {
                Console.WriteLine(Messages.NoLoggedUser);
                return;
            }

            var user = GetCurrentLoggedUser();
            if (!CheckForAccountNumber(user, accNum))
            {
                Console.WriteLine(Messages.IncorrectBankAccount);
                return;
            }

            user.SavingAccounts.FirstOrDefault(s => s.AccountNumer == accNum).AddInterest();

            var balance = user.SavingAccounts.FirstOrDefault(sa => sa.AccountNumer == accNum).Balance;

            db.Update(user);
            db.SaveChanges();
            Console.WriteLine($"{Messages.AddedInterest}",accNum, balance);

        }

        private void Withdraw(List<string> data)
        {
            var accNum = data[0];
            var money = decimal.Parse(data[1]);

            var user = GetCurrentLoggedUser();

            if (CheckForAccountNumber(user, accNum))
            {
                var saveAcc = user.SavingAccounts.FirstOrDefault(ba => ba.AccountNumer == accNum);
                var checkAcc = user.CheckingAccouts.FirstOrDefault(ba => ba.AccountNumer == accNum);

                if (saveAcc != null)
                {

                    saveAcc?.WithdrawMoney(money);

                    db.Update(saveAcc);
                    db.SaveChanges();
                    Console.WriteLine($"Account {accNum} has balance of {saveAcc.Balance}");
                }
                else
                {
                    checkAcc?.WithdrawMoney(money);

                    db.Update(checkAcc);
                    db.SaveChanges();
                    Console.WriteLine($"Account {accNum} has balance of {checkAcc.Balance}");
                }


            }
            else
            {
                Console.WriteLine($"{Messages.IncorrectBankAccount}", accNum);
            }


        }

        private void DepositToAcc(List<string> data)
        {

            var accNum = data[0];
            var money = decimal.Parse(data[1]);

            var user = GetCurrentLoggedUser();

            if (CheckForAccountNumber(user, accNum))
            {
                var saveAcc = user.SavingAccounts.FirstOrDefault(ba => ba.AccountNumer == accNum);
                var checkAcc = user.CheckingAccouts.FirstOrDefault(ba => ba.AccountNumer == accNum);

                if (saveAcc != null)
                {
                    
                    saveAcc?.DepositMoney(money);

                    db.Update(saveAcc);
                    db.SaveChanges();
                    Console.WriteLine($"Account {accNum} has balance of {saveAcc.Balance}");
                }
                else
                {
                    checkAcc?.DepositMoney(money);

                    db.Update(checkAcc);
                    db.SaveChanges();
                    Console.WriteLine($"Account {accNum} has balance of {checkAcc.Balance}");
                }


            }
            else
            {
                Console.WriteLine($"{Messages.IncorrectBankAccount}", accNum);
            }


        }

        private bool CheckForAccountNumber(User user, string accNum)
        {
            var isExistsCheck = user.CheckingAccouts.Any(a => a.AccountNumer == accNum);
            var isExistsSave = user.SavingAccounts.Any(a => a.AccountNumer == accNum);

            if (isExistsSave || isExistsCheck)
            {
                return true;
            }
            return false;
        }

        private void AddCheckingAccount(List<string> data)
        {
            if (CheckForLoggedUser())
            {
                var user = GetCurrentLoggedUser();

                var balance = decimal.Parse(data[0]);
                var fee = decimal.Parse(data[1]);

                var accNumber = GenerateAccNumber();

                var checkAcc = new CheckingAccout()
                {
                    AccountNumer = accNumber,
                    Balance = balance,
                    Fee = fee
                };

                user.CheckingAccouts.Add(checkAcc);
                //db.CheckingAccouts.Add(checkAcc);
                db.Update(user);
                db.SaveChanges();

                Console.WriteLine($"Succesfully added accout with number {accNumber}");
            }
        }

        private void AddSavingAccount(List<string> data)
        {
            if (CheckForLoggedUser())
            {
                var user = GetCurrentLoggedUser();

                var balance = decimal.Parse(data[0]);
                var interestRate = decimal.Parse(data[1]);

                var accNumber = GenerateAccNumber();

                var savAcc = new SavingAccount()
                {
                    AccountNumer = accNumber,
                    Balance = balance,
                    InterestRate = interestRate
                };

                user.SavingAccounts.Add(savAcc);

                db.Update(user);
                db.SaveChanges();

                Console.WriteLine($"Succesfully added accout with number {accNumber}");
            }
        }

        private string GenerateAccNumber()
        {
            var builder = new StringBuilder();
            for (int i = 0; i < 10; i++)
            {
                if (i == 3 || i == 7 || i == 9)
                {
                    builder.Append(digits[random.Next(0, digits.Length)]);
                }
                else
                {
                    builder.Append(alpha[random.Next(0, alpha.Length)]);
                }
            }
            return builder.ToString().TrimEnd();
        }

        private void LogoutUser()
        {
            if (CheckForLoggedUser())
            {
                var user = GetCurrentLoggedUser();

                user.IsLoggedIn = false;
                db.Update(user);
                db.SaveChanges();
                Console.WriteLine($"User {user.Username} logged out from the system.");
                return;
            }

            Console.WriteLine(Messages.LogoutFail);
        }

        private void LoginUser(List<string> data)
        {
            var username = data[0];
            var password = data[1];

            var userExists = CheckIfUserExists(username, password);

            var currLoggedUser = CheckForLoggedUser();

            if (userExists && !currLoggedUser)
            {
                var user = db.Users.FirstOrDefault(u => u.Username == username && u.Password == password);

                user.IsLoggedIn = true;

                db.Update(user);
                db.SaveChanges();
                Console.WriteLine($"Succesfully logged in {username}");
            }
            else
            {
                Console.WriteLine(Messages.LoginFail);
            }

        }

        private static bool CheckForLoggedUser()
        {
            var anyLoggedUser = db.Users.Any(u => u.IsLoggedIn == true);

            return anyLoggedUser;
        }

        private static bool CheckIfUserExists(string username, string password)
        {
            var userExists = db.Users.Any(u => u.Username == username && u.Password == password);

            return userExists;
        }

        private void RegisterUser(List<string> data)
        {


            var username = data[0];
            var password = data[1];
            var email = data[2];


            var currUser = new User(username, password, email);

            if (!Validation.IsValid(currUser))
            {
                Console.WriteLine(string.Join("\r\n", Validation.ValidationResults.Select(v => v.ErrorMessage)));
                return;
            }

            var userExists = db.Users.Any(u => u.Username == username);
            var emailExists = db.Users.Any(u => u.Email == email);

            if (!userExists && !emailExists)
            {
                db.Users.Add(currUser);
                db.SaveChanges();
                Console.WriteLine($"{currUser.Username} was registered in the system.");
                return;
            }

            Console.WriteLine("Username or Email already exists!");

        }

        private static User GetCurrentLoggedUser()
        {
            var user = db
                .Users
                .Include(u => u.SavingAccounts)
                .Include(u => u.CheckingAccouts)
                .FirstOrDefault(u => u.IsLoggedIn);

            return user;
        }


    }
}
