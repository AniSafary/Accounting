using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting
{
    public enum TransactionType
    {
        Debit = 1,
        Credit = 2,
    }

    internal class AccountTransaction
    {
        public Guid Id { get; set; }

        public int AccountId { get; set; }

        public DateTime CreatedAt { get; set; }

        public TransactionType TransactionType { get; set; }

        public decimal Amount { get; set; }
    }
}
