using System;

namespace ConsoleEngine.Core 
{
    public abstract class Game
    {
        public abstract void Draw();

        public abstract void Load();

        public abstract void Unload();

        public abstract void Update();

        public abstract void Quit();

        public virtual void Exit() {}

        public virtual void Save() {} 
    }
}