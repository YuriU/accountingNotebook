using System;
using System.Collections.Generic;

namespace Contracts.Accounting
{
    // Im not familiar with accounting domain model so I use domestic methods names
    public interface IAccountingService
    {
        string SayHello();
        
        void PutMoney(string creditCardFrom, decimal amount);

        void TransferMoney(string creditCardTo, decimal amount);

        decimal GetCurrentAmount();

        List<AccountTransaction> GetTransactionLog();
    }
}