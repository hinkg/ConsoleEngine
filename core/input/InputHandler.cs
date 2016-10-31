using System;
using System.Threading;

namespace ConsoleEngine.Core.Input 
{
    public class InputHandler 
    {
        public ConsoleKey Key;

        private bool input;

        public void StartInputListener() 
        {
            Thread inputThread = new Thread(new ThreadStart(ListenForInput));
            inputThread.Start();
        }

        public void ListenForInput() 
        {
            while(true)
            {
                Key = Console.ReadKey(true).Key;
            }
        }
    }
}