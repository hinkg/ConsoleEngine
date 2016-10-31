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

            graphics.Draw(background1, GraphicsDevice.objType.rectangle);
            graphics.Draw(outline1, GraphicsDevice.objType.outline);

            //Console Engine
            Text text1 = new Text(
                " _____                 _        _____         _",
                ConsoleColor.White,
                new Vector2(15, 9));

            Text text2 = new Text(
                "|     |___ ___ ___ ___| |___   |   __|___ ___|_|___ ___",
                ConsoleColor.White,
                new Vector2(15, 10));

            Text text3 = new Text(
                "|   --| . |   |_ -| . | | -_|  |   __|   | . | |   | -_|",
                ConsoleColor.White,
                new Vector2(15, 11));

            Text text4 = new Text(
                "|_____|___|_|_|___|___|_|___|  |_____|_|_|_  |_|_|_|___|",
                ConsoleColor.White,
                new Vector2(15, 12));
            
            Text text5 = new Text(
                "                                         |___|",
                ConsoleColor.White,
                new Vector2(15, 13));
            
            graphics.Draw(text1, GraphicsDevice.objType.text);
            graphics.Draw(text2, GraphicsDevice.objType.text);
            graphics.Draw(text3, GraphicsDevice.objType.text);
            graphics.Draw(text4, GraphicsDevice.objType.text);
            graphics.Draw(text5, GraphicsDevice.objType.text);



            graphics.Refresh();
        }
    }
}
