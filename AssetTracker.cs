using static System.Console;

namespace AssetTracker
{

    public class AssetTracker
    {
        private List<Asset> assets;
        private ExchangeRates exchangeRates;

        public AssetTracker()
        {
            assets = new();
            exchangeRates = new();
            WriteLine("Welcome to AssetTracker 1.0");
        }

        public void PrettyPrint()
        {
            if( assets.Count == 0 )
            {
                WriteLine("Asset list is empty!");
                return; 
            }

            List<Asset> sortedAssets = assets.OrderBy(x => x.GetType().Name)
                                             .ThenBy(x => x.DatePurchased)
                                             .ToList();

            WriteLine 
            ( 
                "Type".PadRight(15) +
                "Brand".PadRight(15) +
                "Model".PadRight(20) +
                "Price".PadRight(20) +
                "US Price".PadRight(20) +
                "Date Purchased".PadRight(18) +
                "Office"
            ); 

            WriteLine
            ( 
                "----".PadRight(15) +
                "-----".PadRight(15) +
                "-----".PadRight(20) +
                "-----".PadRight(20) +
                "--------".PadRight(20) +
                "--------------".PadRight(18) +
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
                    asset.GetType().Name.PadRight(15) +
                    asset.Brand.PadRight(15) +
                    asset.Model.PadRight(20) +
                    (asset.Price.Amount + " " + asset.Price.Currency).PadRight(20) +
                    (asset.Price.PriceInUSD( exchangeRates ).Amount.ToString("N2") + " " + Currency.USD).PadRight(20) +
                    asset.DatePurchased.ToString("yyyy-MM-dd").PadRight(18) +
                    asset.Office
                );

                ResetColor();
            }
        }

        public void InsertDummyValues()
        {
            assets.Add
            (
                new Computer
                ( 
                    "Asus", 
                    "ROG 500", 
                    new Price(9999.90m, Currency.SEK), 
                    DateOnly.FromDateTime(DateTime.Now.AddDays(2.0)), 
                    Country.Sweden
                )
            );

            assets.Add
            (
                new Phone
                (
                    "Iphone", 
                    "15", 
                    new Price(13990.90m, Currency.SEK), 
                    new DateOnly(2022,7,10), 
                    Country.Sweden 
                )
            );

            assets.Add
            (
                new Computer
                ( 
                    "Lenovo", 
                    "TinkPad T490", 
                    new Price(1599m, Currency.USD), 
                    new DateOnly(2003,11,5), 
                    Country.USA 
                )
            );
        }

        public void AddAsset()
        {
            InsertDummyValues();

            // Add user interaction for entering assets here
        }


        /*
            Run method contains "Main Menu" to and reads user input
            to perform actions:

            (A)dd Asset, (P)rint Asset List, or (Q)uit

            return: false if user entered Q to quit, true otherwise
        */
        public bool Run()
        {

            Write("\n(A)dd Asset, (P)rint Asset List, or (Q)uit: " );
            
            string? input = ReadLine()?.Trim().ToUpper();

            // if input==null or empty then set inputChar to ' ' ...
            char? inputChar = input?.Length == 0 ? ' ' : input?.First();
                  
            switch( inputChar )
            {
                case 'A':  
                    AddAsset();
                    break;
            
                case 'P':
                    PrettyPrint();
                    break;

                case 'I':
                    InsertDummyValues();
                    break;

                case 'Q':
                    return false;

                default:
                    break;
            }

            return true; 
        }
    }
}