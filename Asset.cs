
namespace AssetTracker
{
    
    public enum Country
    {
        USA,
        Spain,
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
    }
}