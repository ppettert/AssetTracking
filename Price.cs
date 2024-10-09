using System.Runtime;

namespace AssetTracker 
{
    public enum Currency 
    {   
        USD,
        EUR,
        SEK
    }
    public class Price
    {
        public Price( decimal amount, Currency amountCurrency )
        {
            Amount = amount;
            AmountCurrency = AmountCurrency;
        }

        public decimal Amount { get; set; }
        public Currency AmountCurrency { get; set; }

    }
}