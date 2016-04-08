namespace CQRSSpike.Models
{
    public class CustomerBankBalance
    {
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public string Name { get; set; }
    }
}