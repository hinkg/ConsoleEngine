using System;
using System.Threading;
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

            while (true)
            {
                Update();
                Thread.Sleep(10);
            }
        }

        protected void Update()
        {
            graphics.DrawFill(
                new Vector2(0, 0),
                new Vector2(100, 40),
                "   .      .        .    ",
                ConsoleColor.DarkGray
            );

            graphics.DrawLine(
                new Vector2(0, 0),
                new Vector2(99, 39),
                "1234",
                ConsoleColor.Blue
            );

            graphics.DrawOutline(
                new Vector2(5, 5),
                new Vector2(96, 35),
                5, 4,
                "',",
                ConsoleColor.Red
            );
            graphics.Draw();
        }
    }
}
