using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting
{
    internal class AccountStatement
    {
        public int AccountId { get; set; }
        public DateTime UpdatedAt { get; set; }
        public decimal DebitAmount { get; set; }
        public decimal CreditAmount { get; set; }
        public decimal Fee { get; set; }

        public decimal ClosingBalance => DebitAmount - CreditAmount - Fee;
    }
}
