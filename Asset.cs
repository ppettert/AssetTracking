
using System.Security.Cryptography;
using Microsoft.VisualBasic;

namespace AssetTracker
{
    
    /*
        Enum for countries used to define Offices in Asset
    */
    public enum Country
    {
        France,    
        Germany,
        Spain,
        Sweden,
        USA
    }

    /*
        Generic Asset Class
        Asset class properties described in constructor 
    */
    public class Asset
    {
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public Price? Price { get; set; }
        public DateOnly DatePurchased { get; set; }
        public Country Office { get; set; } 

        /*
            Asset constructor for empty asset
        */
        public Asset()
        {
        }

        /*
            Asset constructor
            in:     brand           string with brand name of asset
            in:     model           string with model name of asset
            in:     price           Price object with paid amount and currency
            in:     datePurchased   DateOnly object with date of purchase
            return: new Asset object
        */
        public Asset( string brand, string model, Price price, DateOnly datePurchased, Country office  )
        { 
            Brand = brand;
            Model = model;
            Price = price;
            DatePurchased = datePurchased;
            Office = office;
        } 


        /*
            Method to enable marking asset yellow in asset list, if it was 
            purchased 2 months and 3 months ago or more, but not 2 months and 6 months. 
        
            return: true if purchase date is 2 years and 6 months ago or more
        */
        public bool MarkedRed()
        {
            DateOnly today = DateOnly.FromDateTime(DateTime.Now);
            return this.DatePurchased.AddMonths(30).CompareTo( today ) <= 0;  
        }


        /*
            Method to enable marking asset yellow in asset list, if it was 
            purchased 2 months and 3 months ago or more, but not if it matches
            2 months and 6 months. 
        
            return: true if within the time span describe above, false otherwise
        */
        public bool MarkedYellow()
        {
            DateOnly today = DateOnly.FromDateTime(DateTime.Now);
            if( !this.MarkedRed() )
            {
                return this.DatePurchased.AddMonths(27).CompareTo( today ) <= 0; 
            } 
            else
            { 
                return false;
            }
        }
    }
}