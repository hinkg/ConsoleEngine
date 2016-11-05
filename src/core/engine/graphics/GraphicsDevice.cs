using System;
using System.Diagnostics;

namespace ConsoleEngine.Core.Graphics
{
    public class GraphicsDevice
    {
        public Tile[] tiles;

        public int width, height;

        public GraphicsDevice(int width, int height)
        {
            this.width = width;
            this.height = height;

            Console.CursorVisible = false;
            Console.SetWindowSize(width, height);
            Console.BufferWidth = width + 1;
            Console.BufferHeight = height;

            tiles = new Tile[width * height];

            for (int i = 0; i < tiles.Length; i++)
                tiles[i] = new Tile();
        }

        public void Draw(IObject obj)
        {
            obj.Draw(this);
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

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            int index = 0;

            for (int y = 0; y < height; y++)
            {
                Console.SetCursorPosition(0, y);

                for (int x = 0; x < width; x++)
                {
                    Tile tile = tiles[index++];

                    if (tile.content != tile.prevContent || tile.color != tile.prevColor)
                    {
                        Console.SetCursorPosition(x, y);
                        Console.ForegroundColor = tile.color;
                        Console.Write(tile.content);

                        tile.prevContent = tile.content;
                        tile.prevColor = tile.color;

                        updates++;
                    }
                }
            }

            stopwatch.Stop();

            int ms = (int)stopwatch.ElapsedMilliseconds;
            if (ms == 0) ms = 1;
            Console.Title = $"ConsoleEngine FPS: {1000 / ms} ({ms}ms), updates: {updates}";
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