using System.Runtime;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

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
                Currency.EUR => new Price(this.Amount * exchangeRates.RateEURUSD, Currency.USD),
                Currency.SEK => new Price(this.Amount * exchangeRates.RateSEKUSD, Currency.USD),
                _ => this,
            };
        }

       
    }
}