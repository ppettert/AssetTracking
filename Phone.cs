namespace AssetTracker
{
    public class Phone : Asset
    {
        public Phone(string name, string model, Price amount, DateOnly datePurchased, Country office) 
        : base(name, model, amount, datePurchased, office )
        {
        }
        
    }
}