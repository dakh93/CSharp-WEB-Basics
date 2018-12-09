using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace BankSystem.Models
{
    public class SavingAccount : BankAccout
    {
        
        public decimal InterestRate { get; set; }
        
        public override void DepositMoney(decimal value)
        {
            this.Balance += value;
        }

        public override void WithdrawMoney(decimal value)
        {
            if (this.Balance < value)
            {
                throw new System.Exception(Exception.LowBalance);
            }

            this.Balance -= value;
        }

        public void AddInterest()
        {
            this.Balance += this.InterestRate * this.Balance;
        }
    }
}
