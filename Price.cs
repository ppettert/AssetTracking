using System.Runtime;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace AssetTracker 
{

    /* Enum defining the currencies used by the AssetTracker */
    public enum Currency 
    {   
        USD,
        EUR,
        SEK
    }

    /*
        Price class, stores amount paid for an asset and in what currency
    */
    public class Price
    {

        public decimal Amount { get; set; }
        public Currency Currency { get; set; }

        /*
            Class constructor
            in:     amount      amount paid for asset in decimal type
            in:     currency    currency of the amount
            return: a new Price object contaning amount and currency
        */
        public Price( decimal amount, Currency currency )
        {
            Amount = amount;
            Currency = currency;
        }

        /*
            Converts Price in non-USD currencies SEK or EUR to USD
            in:     exchangeRates   ExchangeRate object containing exchange rates
            return: a new Price object with amount paid in USD according to today's exchange rate
        */
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