
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

        public bool MarkedRed()
        {
            DateOnly today = DateOnly.FromDateTime(DateTime.Now);
            // Asset should be marked red if 2 years and 6 months or older
            return this.DatePurchased.AddMonths(30).CompareTo( today ) <= 0;  
        }

        public bool MarkedYellow()
        {
            DateOnly today = DateOnly.FromDateTime(DateTime.Now);
            // Asset should be marked yellow if 2 years and 3 months or older
            // but only if not marked red
            if( this.DatePurchased.AddMonths(30).CompareTo( today ) > 0 )
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