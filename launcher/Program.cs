using System;
using System.Threading;
using ConsoleEngine.Core;
using ConsoleEngine.Core.Graphics;
using ConsoleEngine.Core.Interface;
using ConsoleEngine.Core.Input;

namespace ConsoleGame.Client
{
    public class Program
    {
        public static void Main(string[] args) => new Program().Start();

        public GraphicsDevice graphics;

        public InterfaceManager uinterface;
        public int index;

        public InputHandler input;

        protected void Start()
        {
            //Load graphics
            graphics = new GraphicsDevice();
            graphics.Load();

            //Load interface
            uinterface = new InterfaceManager();
            uinterface.Load();
            index = 1;
            AddButtons();

            //Load inputhandler and start input thread
            input = new InputHandler();
            input.StartInputListener();

            //Update
            while (true)
            {
                Update();
                Thread.Sleep(10);
            }
        }

        private void AddButtons()
        {
            new Button(
                "[Red Button]",
                ConsoleColor.Red,
                ConsoleColor.Gray,
                new Vector2(15, 16),
                1
            ).Add(uinterface);
            new Button(
                "[Green Button]",
                ConsoleColor.DarkGreen,
                ConsoleColor.Gray,
                new Vector2(15, 18),
                2
            ).Add(uinterface);
            new Button(
                "[Blue Button]",
                ConsoleColor.Blue,
                ConsoleColor.Gray,
                new Vector2(15, 20),
                3
            ).Add(uinterface);

            uinterface.SelectButton(index);
        }

        protected void Update()
        {
            //Draw
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
            }

            //Handle Input
            {
                if (input.Key == ConsoleKey.Escape)
                {
                    Console.Clear();
                    Thread.Sleep(1000);
                    Environment.Exit(0);
                }

                if (input.Key == ConsoleKey.UpArrow)
                {
                    index -= 1;

                    if (index == 0)
                        index = uinterface.buttons.Count;
                }

                if (input.Key == ConsoleKey.DownArrow)
                {
                    index += 1;

                    if (index == uinterface.buttons.Count + 1)
                        index = 1;
                }

                input.Key = ConsoleKey.Clear;

                uinterface.SelectButton(index);
            }

            uinterface.Draw(graphics);

            graphics.Refresh();
        }
    }
}
