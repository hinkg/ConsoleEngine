using System;
using System.Threading;
using System.Threading.Tasks;
using ConsoleGame.Core;
using ConsoleGame.Core.Graphics;

namespace ConsoleGame.Client
{
    public class Program
    {
        public static void Main(string[] args) => new Program().Start();

        public GraphicsDevice graphics;

        protected void Start()
        {
            graphics = new GraphicsDevice();
            graphics.Load();

            Console.CursorVisible = false;

            while (true)
            {
                Update();
                Thread.Sleep(10);
            }
        }

        protected void Update()
        {
            graphics.DrawOutline(
                new Vector2(1, 1),
                new Vector2(27, 27),
                8,
                "#");

            graphics.DrawLine(
                new Vector2(1, 6),
                new Vector2(26, 21),
                "%");

            graphics.Draw();
        }
    }
}
