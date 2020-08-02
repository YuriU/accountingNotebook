using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Transactions;
using Contracts.Accounting;

namespace Service.Accounting
{
    public class Account
    {
        private ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();

        private decimal _amount;
        
        private List<AccountTransaction> _transactions = new List<AccountTransaction>();

        public decimal GetAmount()
        {
            _lock.EnterReadLock();
            try
            {
                return _amount;
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        public void PutMoney(string creditCardFrom, decimal amount)
        {
            _lock.EnterWriteLock();
            try
            {
                _amount += amount;
                _transactions.Add(new AccountTransaction
                {
                    Amount = amount,
                    CreditCard = creditCardFrom
                });
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public void TransferMoney(string creditCardTo, decimal amount)
        {
            _lock.EnterWriteLock();
            try
            {
                if (_amount - amount < 0)
                {
                    throw new InvalidOperationException("The balance cannot be negative");
                }

                _amount -= amount;
                _transactions.Add(new AccountTransaction
                {
                    Amount = -amount,
                    CreditCard = creditCardTo
                });
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public List<AccountTransaction> GetTransactionsLog()
        {
            _lock.EnterReadLock();
            try
            {
                return _transactions.Select(t => t.ShallowCopy()).ToList();
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }
    }
}