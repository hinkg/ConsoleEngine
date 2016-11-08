using System;
using System.Threading;
using System.Threading.Tasks;
using ConsoleEngine.Core;
using ConsoleEngine.Core.Graphics;
using ConsoleEngine.Core.Input;
using ConsoleEngine.Core.Interface;

namespace ConsoleGame
{
    public class Sandbox
    {
        private GraphicsDevice graphics;
        private InterfaceManager uinterface;
        private InputHandler input;

        private string[] args;

        private string windowname = "CE Sandbox 0.1";

        private bool exit;

        public Sandbox(string[] args)
        {
            graphics = new GraphicsDevice(100, 40, windowname, 100, 40);
            uinterface = new InterfaceManager();
            input = new InputHandler();

            this.args = args;
        }

        int index = 1;

        public void Initialize()
        {
            new Outline("',", ConsoleColor.Gray, new Vector2(50, 20), new Vector2(90, 30), new Vector2(8, 4)).Add(graphics);
            new Text("ConsoleEngine Sandbox", ConsoleColor.White, new Vector2(15, 10)).Add(graphics);
            new Button("[Button #1]", ConsoleColor.White, ConsoleColor.DarkGray, new Vector2(15, 13), 1).Add(uinterface);
            new Text("[Not a button]", ConsoleColor.DarkGray, new Vector2(15, 15)).Add(graphics);
            new Button("[Exit to launcher]", ConsoleColor.White, ConsoleColor.DarkGray, new Vector2(15, 17), 2).Add(uinterface);

            uinterface.SelectButton(index);

            while (true)
            {
                Update();
                Draw();
                Thread.Sleep(10);
            }
        }

        public void Unload()
        {
            exit = true;

            graphics = null;
            uinterface = null;
            input.exit = true;
            input = null;

            Console.Clear();
        }

        public void Update()
        {
            if (input.Key == ConsoleKey.UpArrow)
                index -= 1;

            if (input.Key == ConsoleKey.DownArrow)
                index += 1;

            if (index == 0)
                index = uinterface.buttons.Count;

            if (index == uinterface.buttons.Count + 1)
                index = 1;

            if (input.Key == ConsoleKey.Enter)
            {
                if (index == 2)
                {
                    Unload();
                    new Launcher(args).Initialize();
                }
            }

            input.Key = ConsoleKey.Clear;

            uinterface.SelectButton(index);
        }

        public void Draw()
        {
            uinterface.Draw(graphics);

            graphics.Draw();

            graphics.Refresh();
        }
    }
}