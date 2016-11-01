using System;
using System.IO;
using System.Threading;
using ConsoleEngine.Core;
using ConsoleEngine.Core.Graphics;
using ConsoleEngine.Core.Interface;
using ConsoleEngine.Core.Input;

namespace ConsoleGame.Client
{
    public class Program
    {
        public static void Main(string[] args) => new Program().Start(args);

        public GraphicsDevice graphics;

        public InterfaceManager uinterface;
        public int index = 1;

        public InputHandler input;

        public string[] args;

        public bool drawImage1 = false;
        public bool drawImage2 = false;

        protected void Start(string[] args)
        {
            this.args = args;

            //Load graphics
            graphics = new GraphicsDevice(100, 40);
            graphics.Load();

            //Load interface
            uinterface = new InterfaceManager();
            uinterface.Load();
            
            LoadObjects();
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
            new Button("[Load Image #1]", ConsoleColor.White, ConsoleColor.DarkGray, new Vector2(15, 16), 1).Add(uinterface);
            new Button("[Load Image #2]", ConsoleColor.White, ConsoleColor.DarkGray, new Vector2(15, 18), 2).Add(uinterface);
            new Button("[Useless Button]", ConsoleColor.White, ConsoleColor.DarkGray, new Vector2(15, 20), 3).Add(uinterface);
            new Button("[Exit]", ConsoleColor.White, ConsoleColor.DarkGray, new Vector2(15, 22), 4).Add(uinterface);

            uinterface.SelectButton(index);
        }

        Image image1;
        Image image2;
        Rectangle background1;
        Outline outline1;
        Text text1;

        private void LoadObjects()
        {
            image1 = new Image(args[0].ToString() + "/launcher/content/image1.txt", new Vector2(32, 16));
            image2 = new Image(args[0].ToString() + "/launcher/content/image2.txt", new Vector2(32, 16));
            background1 = new Rectangle(" ", ConsoleColor.Black, new Vector2(100, 40), new Vector2(50, 20));
            outline1 = new Outline("',", ConsoleColor.Gray, new Vector2(50, 20), new Vector2(90, 30), new Vector2(8, 4));
            text1 = new Text(

                    " _____                 _        _____         _\n" +
                    "|     |___ ___ ___ ___| |___   |   __|___ ___|_|___ ___\n" +
                    "|   --| . |   |_ -| . | | -_|  |   __|   | . | |   | -_|\n" +
                    "|_____|___|_|_|___|___|_|___|  |_____|_|_|_  |_|_|_|___|\n" +
                    "                                         |___|",
                    ConsoleColor.White, new Vector2(15, 9));
        }

        protected void Update()
        {
            //Handle Input
            {
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

                if (input.Key == ConsoleKey.Enter)
                {
                    if (index == 4)
                    {
                        Console.Clear();
                        Thread.Sleep(1000);
                        Environment.Exit(0);
                    }

                    if (index == 1)
                    {
                        drawImage2 = false;

                        if (!drawImage1)
                            drawImage1 = true;
                        else
                            drawImage1 = false;
                    }

                    if (index == 2)
                    {
                        drawImage1 = false;

                        if (!drawImage2)
                            drawImage2 = true;
                        else
                            drawImage2 = false;
                    }

                }

                input.Key = ConsoleKey.Clear;

                uinterface.SelectButton(index);
            }

            background1.Draw(graphics);
            outline1.Draw(graphics);
            text1.Draw(graphics);

            if (drawImage1)
            {
                image1.Draw(graphics);
            }

            if (drawImage2)
            {
                image2.Draw(graphics);
            }

            uinterface.Draw(graphics);

            graphics.Refresh();
        }
    }
}
