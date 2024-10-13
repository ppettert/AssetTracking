namespace AssetTracker
{        
    /*
        Asset subclass for computers
    */
    public class Computer : Asset
    {
        public Computer() : base()
        {        
        }

        public Computer(string name, string model, Price amount, DateOnly datePurchased, Country office) 
        : base(name, model, amount, datePurchased, office)
        {
        }
    }
}