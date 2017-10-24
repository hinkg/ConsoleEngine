using System;

namespace ConsoleEngine
{
    public abstract class Game
    {
        protected bool isRunning;

        protected int frameCounter;

        public Game()
        {

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

        protected abstract void Load();

        protected abstract void Unload();

        protected abstract void Update();

        protected abstract void Draw();
    }
}