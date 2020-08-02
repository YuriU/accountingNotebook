namespace Api.Dtos.Account
{
    public class PutMoneyRequest
    {
        public string CreditCardFrom { get; set; }
        
        public decimal Amount { get; set; }
    }
}