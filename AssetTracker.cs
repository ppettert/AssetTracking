using static System.Console;
using static System.StringComparison;

namespace AssetTracker
{
    /*
        General class for user interaction and creation and handling of asset list
    */
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


        /*
            Prints the asset list in a formatted way with color highlighting depending
            on if write-off date is getting closer

            in: sortByOffice    Set to false to sort by Asset type, otherwise sorted by in which location asset is in
        */
        public void PrettyPrint( bool sortByOffice = true )
        {
            if( assets.Count == 0 )
            {
                WriteLine("\nAsset list is empty!");
                return; 
            }

            // if sortByOffice is true:  set sortedAssets to assets sorted by Office first
            // if sortByOffice is false: set sortedAsset to assets sorted by Asset type first
            // secondary sort criteria is Date Purchased in both cases
            List<Asset> sortedAssets = 
                sortByOffice ? 
                    assets.OrderBy(x => x.Office )
                                     .ThenBy(x => x.DatePurchased)
                                     .ToList()
                    :
                        assets.OrderBy(x => x.GetType().Name)
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
                    asset.Brand?.PadRight(15) +
                    asset.Model?.PadRight(20) +
                    (asset.Price?.Amount.ToString("N2") + " " + asset.Price?.Currency).PadRight(20) +
                    (asset.Price?.PriceInUSD( exchangeRates ).Amount.ToString("N2") + " " + Currency.USD).PadRight(20) +
                    asset.DatePurchased.ToString("yyyy-MM-dd").PadRight(18) +
                    asset.Office
                );

                ResetColor();
            }
        }

        /*
            Helper method to make it easy to add a lot of assets into the asset list
        */
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


        // For code review: 
        // This method is a bit long, and too deep, would like to refactor
        // to make it a little bit flatter and maybe divide it into more
        // functions

        /*
            Helper function for Add asset
            return: true if asset was added, false if user aborted
        */ 
        public bool AddAssetProperties( Asset asset )
        {
            // Brand property input loop
            bool done = false;
            while( !done )
            {
                Write("Enter brand name: ");
                string? input = ReadLine()?.Trim();
                if( input is not null)
                {
                    if( input.Equals("Q", OrdinalIgnoreCase) )
                    {
                        return false;
                    }
                    else if( input.Length != 0 )
                    {
                        asset.Brand = input;
                        done = true;
                    }

                }
            }

            // Model property input loop
            done = false;
            while( !done )
            {
                Write("Enter model name: ");
                
                string? input = ReadLine()?.Trim();
                if( input is not null)
                {
                    if( input.Equals("Q", OrdinalIgnoreCase) )
                    {
                        return false;
                    }
                    else if( input.Length != 0 )
                    {
                        asset.Model = input;
                        done = true;
                    } 

                }
            }

            // Office (Countries) user input loop
            done = false;
            while( !done )
            {
                Write("Enter Office (Spain, Sweden or USA): ");
                string? input = ReadLine()?.Trim();

                if( input is not null )
                {
                    if( input.Length == 0 )
                    {   
                    }
                    else if( input.Equals("Q", OrdinalIgnoreCase) )
                    {
                        return false;
                    }
                    else
                    {
                        Country office = Country.Spain;

                        string[] validCountries = ["Spain", "Sweden", "USA"];

                        foreach (var curr in validCountries)
                        {
                            if (input.Equals(curr, OrdinalIgnoreCase))
                            {
                                office = Enum.Parse<Country>(curr);
                                asset.Office = office; 
                                done = true;
                                break;
                            }
                        }

                        if( !done )
                        {
                            WriteLine("Not a valid office!");
                        }
                    }
                }
            }

            // Price property input loop
            done = false;
            while( !done )
            {
                Write("Enter amount paid: ");

                string? input = ReadLine()?.Trim();
                if( input is not null )
                {
                    if( input.Equals("Q", OrdinalIgnoreCase ) )
                    {
                        return false;
                    }

                    if( decimal.TryParse(input, out decimal amount) )
                    {
                        Currency currency = Currency.USD; // Default to USD
                        
                        if( asset.Office == Country.Spain )  
                        {
                            currency = Currency.EUR;
                        }

                        if( asset.Office == Country.Sweden )
                        {
                            currency = Currency.SEK;
                        }
                        
                        asset.Price = new( amount, currency ); 
                        done = true;

                    }
                    else
                    {
                        WriteLine("Invalid amount value entered!");
                    }
                }

            }

            // DateOnly user input loop
            // asset.DatePurchased = DateOnly.FromDateTime(DateTime.Today); 
            done = false;
            while( !done )
            {
                Write("Enter Purchase date in yyyy-MM-dd format or Today for today's date: ");
                string? input = ReadLine()?.Trim();   

                if( input is not null )
                {
                    if( input.Length == 0 )
                    {
                    }
                    else if( input.Equals("Q", OrdinalIgnoreCase) )
                    {
                        return false;
                    }
                    else if( input.Equals("Today", OrdinalIgnoreCase) )
                    {
                        asset.DatePurchased = DateOnly.FromDateTime(DateTime.Today);
                        done = true;    
                    }
                    else 
                    {
                        if( DateOnly.TryParse( input, out DateOnly date ))
                        {
                            asset.DatePurchased = date;
                            done = true;
                        }
                        else
                        {
                            WriteLine("Was not able to understand the date format. Please try again!");
                        }
                    }

                }

            }
    
            assets.Add(asset);

            return true;
        }


        /*
            Helper method to add an asset from user input
        */ 
        public void AddAsset()
        {
             
            WriteLine("\nAsset Entry, enter Q to abort and return to Main Menu");
            bool done = false;
            while( !done )
            {
                WriteLine("\nPlease enter asset type,");
                WriteLine("(C)omputer, (P)hone or Q to return to Main Menu: ");

                string? input = ReadLine()?.Trim();

                if( input is not null )
                {
                    Asset? asset = null; 

                    if( input.Equals("Computer", OrdinalIgnoreCase) || input.Equals("C", OrdinalIgnoreCase) )                                 
                    {
                        asset = new Computer();
                    }
                    else if( input.Equals("Phone", OrdinalIgnoreCase) || input.Equals("P", OrdinalIgnoreCase) )
                    {
                        asset = new Phone();
                    }
                    else if( input.Equals("Q", OrdinalIgnoreCase) )
                    {
                        WriteLine( "Returning to Main Menu!" );
                        done = true;
                    }
                    else
                    {
                        WriteLine( "Unknown Asset Type! ");
                    }

                    if(asset is not null)
                    {
                        if( AddAssetProperties(asset) )
                        {
                            WriteLine($"New asset added! Number assets in list: {assets.Count}");
                        }
                        else
                        {
                            WriteLine("Aborted! Returning to Main Menu.");
                            return; 
                        }
                    }

                }
            }

        }

        /*
            Helper Method to display all possible menu choices
        */
        public void ListCommands()
        {
            WriteLine("\n\tA - Add new asset to list");
            WriteLine("\tP - Print list sorted by Office");
            WriteLine("\tT - Print list sorted by Type");
            WriteLine("\tS - Show asset list stats");
            WriteLine("\tF - Fill asset list with test data");
            WriteLine("\tQ - Quit");
        }

        /*
            Summarizes and prints some statistics about the current asset list state
        */
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

            A - Add new asset to list
            P - Print list sorted by Office
            T - Print list sorted by Type
            S - Show asset list stats
            F - Fill asset list with test data
            L - List commands
            Q - Quit

            return: false if user entered Q to quit, true otherwise
        */
        public bool Run()
        {

            Write("\n(A)dd Asset, (P)rint Asset List, List all (C)ommands or (Q)uit: " );
            
            string? input = ReadLine()?.Trim().ToUpper();

            // if input==null or empty then set inputChar to ' ' ...
            // char? inputChar = input?.Length == 0 ? ' ' : input?.First();
                  
            switch( input )
            {
                case "A":  
                    AddAsset();
                    break;
            
                case "P":
                    PrettyPrint();
                    break;

                case "T":
                    PrettyPrint( false );
                    break;

                case "C":
                    ListCommands();
                    break;

                case "F":
                    InsertSampleData();           
                    break;

                case "S":
                    ShowStats(); 
                    break;

                case "Q":
                    WriteLine("\nGoodbye!");
                    return false;

                default:
                    break;
            }

            return true; 
        }
    }
}