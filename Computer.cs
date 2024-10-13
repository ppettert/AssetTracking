namespace AssetTracker
{        
    /*
        Asset subclass for phones
    */
    public class Computer : Asset
    {
        public Computer(string name, string model, Price amount, DateOnly datePurchased, Country office) 
        : base(name, model, amount, datePurchased, office)
        {
        }
    }
}