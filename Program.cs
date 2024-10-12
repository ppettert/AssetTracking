// See https://aka.ms/new-console-template for more information
using static System.Console;

namespace AssetTracker
{

    public class ExchangeRates
    {
        public decimal EURUSD { get; set; } = 0.0m;
        public decimal SEKUSD { get; set; } = 0.0m;
    }

    public class Program
    {

        public static void PrettyPrint( List<Asset> assets, ExchangeRates exchangeRates )
        {
            WriteLine 
            ( 
                "Type".PadRight(20) +
                "Brand".PadRight(20) +
                "Model".PadRight(20) +
                "Price".PadRight(20) +
                "US Price".PadRight(20) +
                "Date Purchased".PadRight(20) +
                "Office"
            ); 

            WriteLine
            ( 
                "----".PadRight(20) +
                "-----".PadRight(20) +
                "-----".PadRight(20) +
                "-----".PadRight(20) +
                "--------".PadRight(20) +
                "-------------".PadRight(20) +
                "------"
            );

            foreach (Asset asset in assets)
            {
      
                if( asset.MarkedRed() )
                {
                    ForegroundColor = ConsoleColor.Red;
                }
                else if( asset.MarkedYellow() )
                {
                    // My console background color is white, Yellow is barely visible
                    ForegroundColor = ConsoleColor.Magenta;
                }

                WriteLine
                ( 
                    asset.GetType().Name.PadRight(20) +
                    asset.Brand.PadRight(20) +
                    asset.Model.PadRight(20) +
                    (asset.Price.Amount + " " + asset.Price.Currency).PadRight(20) +
                    (asset.Price.PriceInUSD( exchangeRates ).Amount.ToString("N2") + " " + Currency.USD).PadRight(20) +
                    asset.DatePurchased.ToString("yyyy-MM-dd").PadRight(20) +
                    asset.Office
                );

                ResetColor();
            }

        }
        private static void Main(string[] args)
        {

            List<Asset> assets = new();
            assets.Add(
                new Computer("Asus", "ROG 500", new Price(9999.90m, Currency.SEK),
                DateOnly.FromDateTime(DateTime.Now.AddDays(2.0)), Country.Sweden)
            );

            assets.Add(
                new Phone("Iphone", "15", new Price(13990.90m, Currency.SEK),
                new DateOnly(2022,7,10), Country.Sweden)
            );

            assets.Add(
                new Computer("Lenovo", "TinkPad T490", new Price(1599m, Currency.USD),
                new DateOnly(2003,11,5), Country.USA)
            );



            LiveCurrency.FetchRates();
            ExchangeRates exchangeRates = new();
            exchangeRates.EURUSD = LiveCurrency.Convert(1.0m, "EUR", "USD");
            exchangeRates.SEKUSD = LiveCurrency.Convert(1.0m, "SEK", "USD");

            // Use fixed exchange rate if LiveCurrency call fails
            exchangeRates.EURUSD = exchangeRates.EURUSD == 0.0m ? 1.09m : exchangeRates.EURUSD;
            exchangeRates.SEKUSD = exchangeRates.SEKUSD == 0.0m ? 0.096m : exchangeRates.SEKUSD;

            // AssetList sortedAssets = (AssetList)assets.OrderBy(x => x.GetType().Name)
            //                                  .ThenBy(x => x.DatePurchased)
            //                                  .ToList<Asset>();

            
            List<Asset> sortedAssets = assets.OrderBy(x => x.GetType().Name)
                                             .ThenBy(x => x.DatePurchased)
                                             .ToList();

            PrettyPrint( sortedAssets, exchangeRates );

            new UserInterface().Run();

        }


    }

    //Default list of assets
    //P.S I made up the model names from my limited imagination
    // tracker.AddAsset(new Smartphone(new Price(200, Currency.USD), DateTime.Now.AddMonths(-36 + 4), "Motorola", "X3", usa));
    // tracker.AddAsset(new Smartphone(new Price(400, Currency.USD), DateTime.Now.AddMonths(-36 + 5), "Motorola", "X3", usa));
    // tracker.AddAsset(new Smartphone(new Price(400, Currency.USD), DateTime.Now.AddMonths(-36 + 10), "Motorola", "X2", usa));
    // tracker.AddAsset(new Smartphone(new Price(4500, Currency.SEK), DateTime.Now.AddMonths(-36 + 6), "Samsung", "Galaxy 10", sweden));
    // tracker.AddAsset(new Smartphone(new Price(4500, Currency.SEK), DateTime.Now.AddMonths(-36 + 7), "Samsung", "Galaxy 10", sweden));
    // tracker.AddAsset(new Smartphone(new Price(3000, Currency.SEK), DateTime.Now.AddMonths(-36 + 4), "Sony", "XPeria 7", sweden));
    // tracker.AddAsset(new Smartphone(new Price(3000, Currency.SEK), DateTime.Now.AddMonths(-36 + 5), "Sony", "XPeria 7", sweden));
    // tracker.AddAsset(new Smartphone(new Price(220, Currency.EUR), DateTime.Now.AddMonths(-36 + 12), "Siemens", "Brick", germany));
    // tracker.AddAsset(new Computer(new Price(100, Currency.USD), DateTime.Now.AddMonths(-38), "Dell", "Desktop 900", usa));
    // tracker.AddAsset(new Computer(new Price(100, Currency.USD), DateTime.Now.AddMonths(-37), "Dell", "Desktop 900", usa));
    // tracker.AddAsset(new Computer(new Price(300, Currency.USD), DateTime.Now.AddMonths(-36 + 1), "Lenovo", "X100", usa));
    // tracker.AddAsset(new Computer(new Price(300, Currency.USD), DateTime.Now.AddMonths(-36 + 4), "Lenovo", "X200", usa));
    // tracker.AddAsset(new Computer(new Price(500, Currency.USD), DateTime.Now.AddMonths(-36 + 9), "Lenovo", "X300", usa));
    // tracker.AddAsset(new Computer(new Price(1500, Currency.SEK), DateTime.Now.AddMonths(-36 + 7), "Dell", "Optiplex 100", sweden));
    // tracker.AddAsset(new Computer(new Price(1400, Currency.SEK), DateTime.Now.AddMonths(-36 + 8), "Dell", "Optiplex 200", sweden));
    // tracker.AddAsset(new Computer(new Price(1300, Currency.SEK), DateTime.Now.AddMonths(-36 + 9), "Dell", "Optiplex 300", sweden));
    // tracker.AddAsset(new Computer(new Price(1600, Currency.EUR), DateTime.Now.AddMonths(-36 + 14), "Asus", "ROG 600", germany));
    // tracker.AddAsset(new Computer(new Price(1200, Currency.EUR), DateTime.Now.AddMonths(-36 + 4), "Asus", "ROG 500", germany));
    // tracker.AddAsset(new Computer(new Price(1200, Currency.EUR), DateTime.Now.AddMonths(-36 + 3), "Asus", "ROG 500", germany));
    // tracker.AddAsset(new Computer(new Price(1300, Currency.EUR), DateTime.Now.AddMonths(-36 + 2), "Asus", "ROG 500", germany));
}