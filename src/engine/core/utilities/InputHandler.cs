using System;
using System.Threading;

namespace ConsoleEngine.Core
{
    public class InputHandler
    {
        public ConsoleKey Key;

        public bool exit;

        public InputHandler()
        {
            Thread inputThread = new Thread(new ThreadStart(ListenForInput));
            inputThread.Start();
        }

        public void ListenForInput()
        {
            while (!exit)
            {
                Key = Console.ReadKey(true).Key;
            }
        }
    }
}