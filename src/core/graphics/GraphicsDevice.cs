using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace ConsoleEngine.Core.Graphics
{
    public class GraphicsDevice
    {
        public List<IObject> objects;

        public Tile[] tiles;

        public int width, height;

        public string windowname;

        private int msCounter = 1000;
        private int displayMs;

        public GraphicsDevice(int width, int height, string windowname)
        {
            objects = new List<IObject>();

            this.width = width;
            this.height = height;
            this.windowname = windowname;

            Console.CursorVisible = false;
            Console.SetWindowSize(width, height);
            Console.BufferWidth = width + 1;
            Console.BufferHeight = height;

            tiles = new Tile[width * height];

            for (int i = 0; i < tiles.Length; i++)
                tiles[i] = new Tile();
        }

        public void Draw()
        {
            for(int i = 0; i < objects.Count; i++) 
            {
                objects[i].Draw(this);
            } 
        }

        public void DrawPixel(int x, int y, char content, ConsoleColor color)
        {
            if (0 <= x && x < width
             && 0 <= y && y < height)
            {
                Tile tile = tiles[x + y * width];
                tile.content = content;
                tile.color = color;
            }
        }

        public void Refresh()
        {
            int updates = 0;
            int draws = 0;

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            int index = 0;

            for (int y = 0; y < height; y++)
            {
                string content = "";
                ConsoleColor color = ConsoleColor.Black;

                Console.SetCursorPosition(0, y);

                for (int x = 0; x <= width; x++)
                {
                    if (x == width)
                    {
                        if(content.Length > 0)
                        {
                            Console.ForegroundColor = color;
                            Console.Write(content);

                            draws++;
                        }

                        break;
                    }

                    Tile tile = tiles[index++];

                    if (tile.content != tile.prevContent || tile.color != tile.prevColor)
                    {
                        if (tile.color == color)
                        {
                            content += tile.content;
                        }
                        else
                        {

                            content = tile.content.ToString();
                            color = tile.color;
                            Console.SetCursorPosition(x, y);
                        }

                        tile.prevContent = tile.content;
                        tile.prevColor = tile.color;

                        updates++;
                    }
                    else
                    {
                        if(content.Length > 0)
                        {
                            Console.ForegroundColor = color;
                            Console.Write(content);

                            draws++;
                        }

                        content = "";
                        color = tile.color;
                    }
                }
            }

            stopwatch.Stop();

            int ms = (int)stopwatch.ElapsedMilliseconds + 10;
            if (ms == 0) ms = 1;

            msCounter += ms;

            while(msCounter >= 1000)
            {
                displayMs = ms;
                msCounter -= 1000;
            }

            Console.Title = $"{windowname} : {1000 / displayMs}fps ({displayMs}ms) : {updates} updates : {draws} draws";
        }

        public class Tile
        {
            public char content = ' ';
            public char prevContent = ' ';

            public ConsoleColor color = ConsoleColor.Black;
            public ConsoleColor prevColor = ConsoleColor.Black;
        }
    }
}