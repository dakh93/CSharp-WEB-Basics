using System;
using System.Collections.Generic;
using System.Text;

namespace BankSystem.Models
{
    public class CheckingAccout : BankAccout
    {
        public decimal Fee { get; set; }

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

        public void DeductFee()
        {
            if (this.Balance < this.Fee)
            {
                throw new System.Exception(Exception.LowBalance);
            }

            this.Balance -= this.Fee;


        }
    }
}
