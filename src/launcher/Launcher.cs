using System;
using System.Threading;
using System.Threading.Tasks;
using ConsoleEngine.Core;
using ConsoleEngine.Core.Graphics;
using ConsoleEngine.Core.Input;
using ConsoleEngine.Core.Interface;

namespace ConsoleGame
{
    public class Launcher
    {
        private GraphicsDevice graphics;
        private InterfaceManager uinterface;
        private InputHandler input;

        private string[] args;

        private string windowname = "CE Launcher";

        public Launcher(string[] args)
        {
            graphics = new GraphicsDevice(100, 40, windowname);
            uinterface = new InterfaceManager();
            input = new InputHandler();

            this.args = args;
        }

        int index = 1;

        public void Initialize()
        {
            new Image(args[0].ToString() + "/launcher/resources/images/logo.txt", new Vector2(14, 9)).Add(graphics);
            new Outline("',", ConsoleColor.Gray, new Vector2(50, 20), new Vector2(90, 30), new Vector2(8, 4)).Add(graphics);
            new Button("[Start Game #1]", ConsoleColor.White, ConsoleColor.DarkGray, new Vector2(15, 16), 1).Add(uinterface);
            new Button("[Useless Button]", ConsoleColor.Red, ConsoleColor.DarkGray, new Vector2(15, 18), 2).Add(uinterface);
            new Button("[Exit]", ConsoleColor.White, ConsoleColor.DarkGray, new Vector2(15, 20), 3).Add(uinterface);

            uinterface.SelectButton(index);

            while (true)
            {
                Update();
                Draw();
                Thread.Sleep(10);
            }
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
                if (index == 2) PlaySong();
                if (index == 3) Environment.Exit(0);
            }

            input.Key = ConsoleKey.Clear;

            uinterface.SelectButton(index);
        }

        public Task PlaySong()
        {
            Console.Beep(440, 500);
            Console.Beep(440, 500);
            Console.Beep(440, 500);
            Console.Beep(349, 350);
            Console.Beep(523, 150);
            Console.Beep(440, 500);
            Console.Beep(349, 350);
            Console.Beep(523, 150);
            Console.Beep(440, 500);
            Thread.Sleep(500);
            Console.Beep(659, 500);
            Console.Beep(659, 500);
            Console.Beep(659, 500);
            Console.Beep(698, 350);
            Console.Beep(523, 150);
            Console.Beep(415, 500);
            Console.Beep(349, 350);
            Console.Beep(523, 150);
            Console.Beep(440, 500);

            return Task.CompletedTask;
        }

        public void Draw()
        {
            uinterface.Draw(graphics);

            graphics.Draw();

            graphics.Refresh();
        }
    }
}