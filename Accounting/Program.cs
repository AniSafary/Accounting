using Accounting;
var rand = new Random();


/// veradardzni balans@
///account avelacnelu funkcional, 
///amen gortsarqic pahi fee, fee-n pahel accounti mej: balance=debet-credit-fee
///crediti jmk stugel pox ka te che
///mi hat el stugi ete karelia minus gna uremn toxi pox hanel
///



List<Account> acc = new()
{
    new(111, "AAA", AccountType.Physical, 0.5m, 0.2m, AccountCredit.Credit),
    new(222, "BBB", AccountType.Physical, 0.5m, 0.2m, AccountCredit.Credit),
    new(333, "CCC", AccountType.Corporate, 0.6m, 0.3m, AccountCredit.none),
    new(444, "DDD", AccountType.Physical, 0.5m, 0.2m, AccountCredit.Credit),
    new(555, "EEE", AccountType.Corporate, 0.6m, 0.3m, AccountCredit.Credit),
    new(666, "FFF", AccountType.Physical, 0.5m, 0.2m, AccountCredit.none),
};

foreach (Account accItem in acc)
{
    AccountingFunction.AddAccount(accItem);
}

AccountingFunction.AddAccount(new(777, "GGG", AccountType.Physical, 0.5m, 0.2m, AccountCredit.none));

AccountingFunction.PrintAccount();
Console.WriteLine();


foreach (var item in AccountingFunction.GetAccounts())
{
    AccountingFunction.DebitToAccount(item, rand.Next(10000, 20000));
    AccountingFunction.CreaditToAccount(item, rand.Next(10000, 20000));
}

Console.WriteLine("statement");
AccountingFunction.PrintAccountStatement();
Console.WriteLine();

Console.WriteLine("Closing Balance");
foreach (var item in AccountingFunction.GetAccounts())
{
    Console.Write(item.Name+"   ");
    Console.WriteLine(AccountingFunction.ReturnClosingBalance(item));
}
Console.WriteLine();

foreach (var item in AccountingFunction.GetAccounts())
{
    AccountingFunction.DebitToAccount(item, rand.Next(10000, 20000));
    AccountingFunction.CreaditToAccount(item, rand.Next(1000, 2000));
}

Console.WriteLine("Statements");
AccountingFunction.PrintAccountStatement();
Console.WriteLine();

Console.WriteLine("Closing Balance");
foreach (var item in AccountingFunction.GetAccounts())
{
    Console.Write(item.Name + "   ");
    Console.WriteLine(AccountingFunction.ReturnClosingBalance(item));
}
Console.WriteLine();