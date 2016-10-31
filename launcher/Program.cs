using System;
using System.Threading;
using ConsoleEngine.Core;
using ConsoleEngine.Core.Graphics;

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
            //Background
            Rectangle background1 = new Rectangle(
                //"   .      .        .    ",
                " ",
                ConsoleColor.Black,
                new Vector2(100, 40),
                new Vector2(50, 20));
            Outline outline1 = new Outline(
                "',",
                ConsoleColor.Gray,
                new Vector2(50, 20),
                new Vector2(90, 30),
                new Vector2(8, 4));

            graphics.Draw(background1);
            graphics.Draw(outline1);

            //Console Engine
            new Text(
                " _____                 _        _____         _\n" +
                "|     |___ ___ ___ ___| |___   |   __|___ ___|_|___ ___\n" +
                "|   --| . |   |_ -| . | | -_|  |   __|   | . | |   | -_|\n" +
                "|_____|___|_|_|___|___|_|___|  |_____|_|_|_  |_|_|_|___|\n" +
                "                                         |___|",
                ConsoleColor.White,
                new Vector2(15, 9)
            ).Draw(graphics);



            graphics.Refresh();
        }
    }
}
