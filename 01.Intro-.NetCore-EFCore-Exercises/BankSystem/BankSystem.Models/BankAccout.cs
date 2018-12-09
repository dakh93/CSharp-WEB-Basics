using System;
using System.Collections.Generic;
using System.Text;

namespace BankSystem.Models
{
    public abstract class BankAccout
    {
        public int Id { get; set; }

        public string AccountNumer { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public decimal Balance { get; set; }

        public abstract void DepositMoney(decimal value);

        public abstract void WithdrawMoney(decimal value);
    }
}
