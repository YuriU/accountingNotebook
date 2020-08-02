namespace Contracts.Accounting
{
    public class AccountTransaction
    {
        public decimal Amount { get; set; }

        public string CreditCard { get; set; }

        public AccountTransaction ShallowCopy()
        {
            return (AccountTransaction)MemberwiseClone();
        }
    }
}