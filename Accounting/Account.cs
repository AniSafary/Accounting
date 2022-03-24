using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting
{
    public enum AccountType
    {
        Physical = 1,
        Corporate = 2,
    }

    public enum AccountCredit
    {
        none = 1,
        Credit,
    }

    public class Account
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public AccountType Type { get; set; }
        public decimal CreditFee { get; set; }
        public decimal DebitFee { get; set; }
        public AccountCredit AccountCreditType { get; set; }

        public Account(int Id, string Name, AccountType accountType, decimal debitfee, decimal creditfee, AccountCredit accountCredittype)
        {
            this.Id = Id;
            this.Name = Name;
            this.Type = accountType;
            this.CreditFee = creditfee;
            this.DebitFee = debitfee;
            this.AccountCreditType = accountCredittype;
        }
    }

   
}
