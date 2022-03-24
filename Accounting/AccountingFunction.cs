using Accounting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting
{
    static class AccountingFunction
    {
        private static List<Account> Accounts { get; set; } = new List<Account>();

        private static List<AccountStatement> AccountStatements { get; set; } = new List<AccountStatement>();

        private static List<AccountTransaction> AccountTransactions { get; set; } = new List<AccountTransaction>();

        public static AccountTransaction DebitToAccount(Account account, decimal amount)
            => CreateAccountTransaction(account, TransactionType.Debit, amount);

        public static AccountTransaction CreaditToAccount(Account account, decimal amount)
            => CreateAccountTransaction(account, TransactionType.Credit, amount);

        public static List<Account> GetAccounts() => Accounts;
        
        public static void PrintAccount()
        {
            foreach (var item in Accounts)
            {
                Console.WriteLine(item.Name+"  "+item.Id+"  "+item.Type+"  ");
            }
        }

        public static void PrintAccountStatement()
        {
            Console.WriteLine("ID   Debit  Credit    Fee    ClosingBalance");
            foreach (var item in AccountStatements)
            {
                Console.WriteLine(item.AccountId + "    " + item.DebitAmount + "   " + item.CreditAmount + "     " + item.Fee + "    " + item.ClosingBalance);
            }
        }

        public static void PrintAccountTransactions()
        {
            foreach (var item in AccountTransactions)
            {
                Console.WriteLine(item.AccountId + "  " + item.TransactionType + "  " + item.Amount);
            }
        }
        public static void AddAccount(Account account)
        {
            if (!Accounts.Any(x => x.Id == account.Id))
            {
                Accounts.Add(account);
            }
            else throw new InvalidOperationException($"{account.Id} does not exist");
        }

        public static decimal ReturnClosingBalance(Account account)
        {
            if (!Accounts.Any(x => x.Id == account.Id))
            {
                throw new InvalidOperationException($"{account.Id} does not exist");
            }

            Checkpoint();
            AccountStatement? accountStatement = AccountStatements.FirstOrDefault(x => x.AccountId == account.Id);
            return accountStatement.ClosingBalance;
        }

        public static void Checkpoint()
        {
            DateTime lastProcessingDate = DateTime.MinValue;
            DateTime curDate = DateTime.UtcNow;
            if (AccountStatements.Any())
            {
                lastProcessingDate = AccountStatements.Max(x => x.UpdatedAt);
            }

            foreach (var account in Accounts)
            {
                Account accountCurrent = Accounts.FirstOrDefault(x => x.Id == account.Id);
                var statement = AccountStatements.FirstOrDefault(x => x.AccountId == account.Id);

                if (statement == null)
                {                   
                    statement = new AccountStatement
                    {
                        AccountId = account.Id,
                        CreditAmount = AccountTransactions.FindAll(x => x.AccountId == account.Id
                            && x.TransactionType == TransactionType.Credit
                            && x.CreatedAt <= curDate).Sum(x => x.Amount),
                        DebitAmount = AccountTransactions.FindAll(x => x.AccountId == account.Id
                            && x.TransactionType == TransactionType.Debit
                            && x.CreatedAt <= curDate).Sum(x => x.Amount),
                        UpdatedAt = curDate,

                    };
                    statement.Fee = (statement.CreditAmount * accountCurrent.CreditFee / 100) +
                        (statement.DebitAmount * accountCurrent.DebitFee / 100);

                AccountStatements.Add(statement);
                }
                else
                {
                    statement.DebitAmount += AccountTransactions
                            .FindAll(x => x.AccountId == account.Id
                            && x.CreatedAt > lastProcessingDate
                            && x.TransactionType == TransactionType.Debit
                            && x.CreatedAt <= curDate).Sum(x => x.Amount);
                    statement.CreditAmount += AccountTransactions
                            .FindAll(x => x.AccountId == account.Id
                            && x.CreatedAt > lastProcessingDate
                            && x.TransactionType == TransactionType.Credit
                            && x.CreatedAt <= curDate).Sum(x => x.Amount);
                    statement.UpdatedAt = curDate;
                    statement.Fee = (statement.CreditAmount * accountCurrent.CreditFee / 100) +
                        (statement.DebitAmount * accountCurrent.DebitFee / 100);
                }
            }
        }

        private static AccountTransaction CreateAccountTransaction(Account account, TransactionType transactionType, decimal amount)
        {
            if (!Accounts.Any(x => x.Id == account.Id))
            {
                throw new InvalidOperationException($"{account.Id} does not exist");
            }

            if (transactionType == TransactionType.Credit && account.AccountCreditType == AccountCredit.none)
            {                
                var acc= AccountStatements.FirstOrDefault(x => x.AccountId == account.Id);
                var accfee = Accounts.FirstOrDefault(x => x.Id == account.Id);
                decimal sum = amount + amount * accfee.CreditFee / 100;
                if (sum > acc.ClosingBalance)
                {
                    //throw new InvalidOperationException($"{account.Id} does not money");
                    Console.WriteLine(account.Name + "  Dont have money");
                    amount = 0;
                }
            }

            AccountTransaction transaction = new()
            {
                Id = Guid.NewGuid(),
                AccountId = account.Id,
                Amount = amount,
                CreatedAt = DateTime.UtcNow,
                TransactionType = transactionType
            };

            AccountTransactions.Add(transaction);
            Checkpoint();
            return transaction;
        }
    }
}
