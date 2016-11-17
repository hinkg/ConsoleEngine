using System;
using System.Threading;

namespace ConsoleEngine.Core 
{
    public abstract class Game
    {
        protected string[] args;

        protected bool isRunning;

        protected int frameCounter;

        public Game(string[] args)
        {
            this.args = args; 
        }

        public void Start()
        {
            if(!isRunning)
            {
                Load();

                isRunning = true;

                while(isRunning)
                {
                    Update();

                    // The game usually unloads in the Update method, so we avoid null errors here
                    if(isRunning)
                    {
                        Draw();
                        frameCounter++;
                    }
                }
            }
        }

        public void Stop()
        {
            if(isRunning)
            {
                isRunning = false;
                Unload();

                Console.Clear();
            }
        }

        protected void ChangeGame(Game game)
        {
            if(isRunning)
            {
                isRunning = false;
                Unload();

                game.args = args;
                game.Start();
            }
        }

        protected abstract void Load();

        protected abstract void Unload();

        protected abstract void Update();

        protected abstract void Draw();
    }
}