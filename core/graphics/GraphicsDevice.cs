using System;
using System.Diagnostics;

namespace ConsoleEngine.Core.Graphics
{
    public class GraphicsDevice
    {
        public Row[] rows;

        public enum objType
        {
            text = 0,
            line = 1,
            rectangle = 2,
            outline = 3
        }

        public void Load()
        {
            rows = new Row[40];

            for (int a = 0; a < rows.Length; a++)
            {
                rows[a] = new Row();
                rows[a].tiles = new Tile[100];

                for (int b = 0; b < rows[a].tiles.Length; b++)
                {
                    rows[a].tiles[b] = new Tile();
                    rows[a].tiles[b].content = ' ';
                }
            }
        }

        public void Draw(IObject obj)
        {
            obj.Draw(this);
        }

        public void DrawPixel(int x, int y, char content, ConsoleColor color)
        {
            rows[y].tiles[x].content = content;
            rows[y].tiles[x].color = color;
        }

        public void Refresh()
        {
            Console.CursorVisible = false;
            Console.SetWindowSize(101, 40);

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            for (int a = 0; a < rows.Length; a++)
            {
                string content = "";
                ConsoleColor color = rows[a].tiles[0].color;

                Console.SetCursorPosition(0, a);

                for (int b = 0; b < rows[a].tiles.Length; b++)
                {
                    if (rows[a].tiles[b].color != color)
                    {
                        Console.Write(content);

                        Console.ForegroundColor = rows[a].tiles[b].color;
                        Console.Write(rows[a].tiles[b].content);

                        content = "";
                    }
                    else
                    {
                        content += rows[a].tiles[b].content;
                    }
                }
            }

            stopwatch.Stop();
            Console.Title = $"ConsoleEngine FPS: {1000 / (int)stopwatch.ElapsedMilliseconds} ({stopwatch.ElapsedMilliseconds}ms)";
        }

        public class Row
        {
            public Tile[] tiles;
        }

        public class Tile
        {
            public char content;

            public ConsoleColor color = ConsoleColor.Black;
        }
    }
}