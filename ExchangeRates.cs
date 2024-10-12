namespace AssetTracker
{
    public class ExchangeRates
    {
    
        public ExchangeRates()
        {  
            LiveCurrency.FetchRates();

            RateEURUSD = LiveCurrency.Convert(1.0m, "EUR", "USD");
            RateSEKUSD = LiveCurrency.Convert(1.0m, "SEK", "USD");

            // Using fixed exchange rate if LiveCurrency call fails
            RateEURUSD = RateEURUSD == 0.0m ? 1.09m : RateEURUSD;
            RateSEKUSD = RateSEKUSD == 0.0m ? 0.096m : RateSEKUSD; 
        
        }

        public decimal RateEURUSD { get; set; } = 0.0m;
        public decimal RateSEKUSD { get; set; } = 0.0m;
    }

}