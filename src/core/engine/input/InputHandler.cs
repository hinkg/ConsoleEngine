using System;
using System.Threading;

namespace ConsoleEngine.Core.Input
{
    public class InputHandler
    {
        public ConsoleKey Key;
        
        public InputHandler()
        {
            Thread inputThread = new Thread(new ThreadStart(ListenForInput));
            inputThread.Start();
        }

        public void ListenForInput()
        {
            while (true)
            {
                Key = Console.ReadKey(true).Key;
            }
        }
    }
}