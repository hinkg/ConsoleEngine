using System;
using System.Threading;
using ConsoleGame.Core;
using ConsoleGame.Core.Objects;
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
            Random rdm = new Random();

            Text text1 = new Text(
                "Hi, I'm some shaky text",
                ConsoleColor.Magenta,
                new Vector2(20, 10));

            Line line1 = new Line(
                "1234",
                ConsoleColor.Blue,
                new Vector2(0, 0),
                new Vector2(99, 39));

            Rectangle background1 = new Rectangle(
                "   .      .        .    ",
                ConsoleColor.DarkGray,
                new Vector2(0, 0),
                new Vector2(100, 40));

            Outline outline1 = new Outline(
                "',",
                ConsoleColor.Red, 
                new Vector2(5, 5),
                new Vector2(96, 35), 
                5, 4);

            text1.position = new Vector2(rdm.Next(19, 21), text1.position.y);

            graphics.Draw(background1, GraphicsDevice.objType.rectangle);
            graphics.Draw(outline1, GraphicsDevice.objType.outline);
            graphics.Draw(line1, GraphicsDevice.objType.line);
            graphics.Draw(text1, GraphicsDevice.objType.text);

            graphics.Refresh();
        }
    }
}
