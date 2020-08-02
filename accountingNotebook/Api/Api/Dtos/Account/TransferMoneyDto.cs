namespace Api.Dtos.Account
{
    public class TransferMoneyDto
    {
        public string CreditCardTo { get; set; }
        
        public decimal Amount { get; set; }
    }
}