using System;
using System.Threading;
using System.Threading.Tasks;
using ConsoleEngine.Core;
using ConsoleEngine.Objects;

namespace ConsoleGame
{
    public class Launcher : Game
    {
        private GraphicsDevice graphics;
        private InterfaceManager uinterface;
        private InputHandler input;

        private World world1;

        private bool exit;
        private string[] args;
        private int index;

        public Launcher(string[] args)
        {
            this.args = args;
            this.index = 1;

            graphics = new GraphicsDevice(100, 40, "CE Launcher 1.0");
            uinterface = new InterfaceManager();
            input = new InputHandler();
            world1 = new World(new Vector2(150, 90));
        }

        public override void Load()
        {
            new Outline("',", ConsoleColor.White, new Vector2(50, 20), new Vector2(90, 36), new Vector2(8, 4)).Add(world1);
            new Image(args[0] + "/launcher/resources/images/logo.txt", new Vector2(16, 7)).Add(world1);
            new Button("[Launch Stars]", ConsoleColor.White, ConsoleColor.DarkGray, new Vector2(16, 13), 1).Add(uinterface);
            new Button("[Useless Button]", ConsoleColor.White, ConsoleColor.DarkGray, new Vector2(16, 15), 2).Add(uinterface);
            new Button("[Quit]", ConsoleColor.White, ConsoleColor.DarkGray, new Vector2(16, 17), 3).Add(uinterface);

            while (!exit)
            {
                Update();
                Draw();

                Thread.Sleep(10);
            }
        }

        public override void Unload()
        {
            graphics = null;
            uinterface = null;
            input.exit = true;
            input = null;
        }

        public override void Quit()
        {
            Console.Clear();
            Console.WriteLine("\n  Shutting down...");
            Thread.Sleep(1000);
            Environment.Exit(0);
        }

        public override void Update()
        {
            if (input.Key == ConsoleKey.UpArrow)
                index--;

            if (input.Key == ConsoleKey.DownArrow)
                index++;

            if (index == 0)
                index = uinterface.buttons.Count;

            if (index == uinterface.buttons.Count + 1)
                index = 1;

            uinterface.SelectButton(index);

            if (input.Key == ConsoleKey.Enter)
            {
                if (index == 1)
                {
                    Unload();
                    new Stars(args).Load();
                }

                if (index == 3)
                {
                    Quit();
                }
            }

            input.Key = ConsoleKey.Clear;
        }

        public override void Draw()
        {
            world1.Draw();

            uinterface.Draw(world1);

            graphics.Refresh(world1);
        }
    }
}