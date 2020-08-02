using System.Collections.Generic;
using Contracts.Accounting;

namespace Service.Accounting
{
    public class AccountingService : IAccountingService
    {
        // Operating just only one account
        private readonly Account _theAccount = new Account();
        
        public string SayHello()
        {
            return "Hello";
        }

        public void PutMoney(string creditCardFrom, decimal amount)
        {
            _theAccount.PutMoney(creditCardFrom, amount);
        }

        public void TransferMoney(string creditCardTo, decimal amount)
        {
            _theAccount.TransferMoney(creditCardTo, amount);
        }

        public decimal GetCurrentAmount()
        {
            return _theAccount.GetAmount();
        }

        public List<AccountTransaction> GetTransactionLog()
        {
            return _theAccount.GetTransactionsLog();
        }
    }
}