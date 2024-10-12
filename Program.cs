// See https://aka.ms/new-console-template for more information
using static System.Console;

namespace AssetTracker
{
    public class Program
    {
        /*
            Main Program entry point
        */
        private static void Main(string[] args)
        {
            AssetTracker assetTracker = new();
            while( assetTracker.Run() );
        }


    }
}