using System;

namespace ConsoleEngine.Core
{
    public class InputHandler
    {
        public ConsoleKey GetKey()
        {
            if (Console.KeyAvailable)
                return Console.ReadKey(true).Key;
            
            return ConsoleKey.Clear;
        }
    }
}