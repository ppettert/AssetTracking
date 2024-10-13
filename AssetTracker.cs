
using System.Security.Principal;
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
                WriteLine("\nAsset list is empty!");
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

            foreach (Asset asset in sortedAssets)
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

        public void InsertSampleData()
        {
            // AI was decent at creating this list, also gave me the right answers for dates that are 2 years and 3 months old
            assets.Add(new Computer("HP", "Elitebook", new Price(1176.03m, Currency.EUR), new DateOnly( 2019, 6, 1 ), Country.Spain) );
            assets.Add(new Computer("Asus", "W234", new Price(4900m, Currency.SEK), new DateOnly( 2020, 10, 2 ), Country.Sweden) );
            assets.Add(new Computer("Lenovo", "Yoga 730", new Price(835m, Currency.USD), new DateOnly( 2018, 5, 28 ), Country.USA) );
            assets.Add(new Phone("Apple", "Iphone 15", new Price(10000m, Currency.USD), new DateOnly( 2024, 9, 11 ), Country.USA) );
            assets.Add(new Computer("Lenovo", "Yoga 530", new Price(1030m, Currency.USD), new DateOnly( 2019, 5, 21 ), Country.USA));
            assets.Add(new Computer("Apple", "Macbook Pro", new Price(970m, Currency.EUR), new DateOnly( 2022, 7, 13 ), Country.Spain));
            assets.Add(new Computer("Apple", "iPhone", new Price(818.18m, Currency.EUR), new DateOnly( 2020, 9, 25 ), Country.Spain));
            assets.Add(new Computer("Apple", "iPhone", new Price(10375m, Currency.SEK), new DateOnly( 2018, 7, 15 ), Country.Sweden));
            assets.Add(new Phone("Motorola", "Razr", new Price(8083.33m, Currency.SEK), new DateOnly( 2022, 5, 16 ), Country.Sweden));
            assets.Add(new Phone("Samsung", "Galaxy S23", new Price(8083.33m, Currency.SEK), new DateOnly( 2023, 3, 16 ), Country.Sweden));
            assets.Add(new Computer("Asus","ROG 500",new Price(9999.90m, Currency.SEK), new DateOnly( 2024, 10, 15 ), Country.Sweden ));
        
            WriteLine($"\nTest data added to list, Asset list now has {assets.Count} items.");
        }

        public void AddAsset()
        {
         //     Enum.TryParse("Active", out StatusEnum myStatus);
            // Add user interaction for entering assets here
        }

        public void ListCommands()
        {
            WriteLine("\n\tA - Add new asset to list");
            WriteLine("\tP - Print list sorted by Office");
            WriteLine("\tT - Print list sorted by Type");
            WriteLine("\tS - Show asset list stats");
            WriteLine("\tF - Fill asset list with test data");
            WriteLine("\tQ - Quit");
        }

        public void ShowStats()
        {
            // It's LINQ time!
            int count = assets.Count;
            int computers = assets.Where( item => item.GetType().Name.Equals("Computer")).ToList().Count;
            int phones = assets.Where( item => item.GetType().Name.Equals("Phone")).ToList().Count;
            int offices = assets.Select( item => item.Office ).Distinct().ToList().Count;

            int redItems = assets.Where( item => item.MarkedRed() ).ToList().Count;
            int yellowItems = assets.Where( item => item.MarkedYellow() ).ToList().Count;


            WriteLine();
            WriteLine( $"Computers: {computers}" );
            WriteLine( $"   Phones: {phones}" );
            WriteLine( $"    Total: {count}" );

            WriteLine();
            WriteLine( $"Items that are within 3 months to write-off: {redItems}" );
            WriteLine( $"Items that are within 6 months (but not within 3) to write-off: {yellowItems}" );

            WriteLine($"\nAssets spread over {offices} offices.\n");
        }

        /*
            Run method contains "Main Menu" to and reads user input
            to perform actions:

            (A)dd Asset, (P)rint Asset List, or (Q)uit

            return: false if user entered Q to quit, true otherwise
        */
        public bool Run()
        {

            Write("\n(A)dd Asset, (P)rint Asset List, (L)ist all commands or (Q)uit: " );
            
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

                case 'L':
                    ListCommands();
                    break;

                case 'F':
                    InsertSampleData();           
                    break;

                case 'S':
                    ShowStats(); 
                    break;

                case 'Q':
                    WriteLine("\nGoodbye!");
                    return false;

                default:
                    break;
            }

            return true; 
        }
    }
}