
using System.Security.Cryptography;
using Microsoft.VisualBasic;

namespace AssetTracker
{
    
    public enum Country
    {
        USA,
        Spain,
        Germany,
        France,
        Sweden
    }
    public class Asset
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public Price Price { get; set; }
        public DateOnly DatePurchased { get; set; }
        public Country Office { get; set; } 


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