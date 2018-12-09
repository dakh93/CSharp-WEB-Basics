using System;
using System.Collections.Generic;
using System.Text;

namespace BankSystem.App
{
    public static class Messages
    {
        public const string LoginFail = "Incorrect username / password";

        public const string LogoutFail = "Cannot log out. No user was logged in.";

        public const string IncorrectBankAccount = "Bank account with number {0} does not exist";

        public const string NoLoggedUser = "There isn't currently logged user";

        public const string AddedInterest = "Added interest to {0}. Current balance: {1}";

        public const string DeductedFee = "Deducted fee of {0}. Current balance: {1}";

    }
}
