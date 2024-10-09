
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
        public string Name { get; set; }
        public string Model { get; set; }
        public Price Amount { get; set; }
        public DateOnly DatePurchased { get; set; }
        public Country Office { get; set; } 


        public Asset( string name, string model, Price amount, DateOnly datePurchased, Country office  )
        { 
            Name = name;
            Model = model;
            Amount = amount;
            DatePurchased = datePurchased;
            Office = office;
        }
    }
}