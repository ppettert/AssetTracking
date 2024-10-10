using System.Runtime;
using System.Runtime.InteropServices;

namespace AssetTracker 
{

    public enum Currency 
    {   
        USD,
        EUR,
        SEK
    }

    public class ExchangeRates
    {
        public decimal EURUSD;
        public decimal SEKUSD;

    }
    public class Price
    {
    //    public decimal EURUSD = 1.09m;
    //    public decimal SEKUSD = 0.096m;

        public decimal Amount { get; set; }
        public Currency Currency { get; set; }

        public Price( decimal amount, Currency currency )
        {
            Amount = amount;
            Currency = currency;
        }

        public Price PriceInUSD( ExchangeRates exchangeRates )
        {
            return this.Currency switch
            {
                Currency.EUR => new Price(this.Amount * exchangeRates.EURUSD, Currency.USD),
                Currency.SEK => new Price(this.Amount * exchangeRates.SEKUSD, Currency.USD),
                _ => this,
            };
        }

       
    }
}