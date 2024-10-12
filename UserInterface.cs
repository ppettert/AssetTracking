using static System.Console;

namespace AssetTracker
{
    public class UserInterface
    {
        public UserInterface()
        {
            WriteLine("UserInterface");
        }

        public void Run()
        {
            string? input = ReadLine()?.Trim().ToUpper();

        }
    }
}