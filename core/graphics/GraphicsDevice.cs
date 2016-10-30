using System;

namespace ConsoleGame.Core.Graphics
{
    public class GraphicsDevice
    {
        public Line[] lines;

        public void Load()
        {
            lines = new Line[40];

            for (int a = 0; a < lines.Length; a++)
            {
                lines[a] = new Line();
                lines[a].tiles = new Tile[100];

                for (int b = 0; b < lines[a].tiles.Length; b++)
                {
                    lines[a].tiles[b] = new Tile();
                    lines[a].tiles[b].content = " ";
                }
            }
        }

        public void DrawPixel(int x, int y, string content, ConsoleColor color)
        {
            lines[y].tiles[x].content = content;
            lines[y].tiles[x].color = color;
        }

        public void DrawLine(Vector2 start, Vector2 end, string content, ConsoleColor color)
        {
            int x1 = start.x;
            int y1 = start.y;
            int x2 = end.x;
            int y2 = end.y;

            int dx = Math.Abs(x2 - x1);
            int dy = Math.Abs(y2 - y1);

            int sx = x1 < x2 ? 1 : -1;
            int sy = y1 < y2 ? 1 : -1;

            int err = dx - dy;

            while (true)
            {
                DrawPixel(x1, y1, content, color);

                if (x1 == x2 && y1 == y2)
                    break;

                int e2 = 2 * err;

                if (e2 > -dy)
                {
                    err -= dy;
                    x1 += sx;
                }

                if (e2 < dx)
                {
                    err += dx;
                    y1 += sy;
                }
            }
        }

        public void DrawFill(Vector2 pos1, Vector2 pos2, string content, ConsoleColor color)
        {
            for (int x = pos1.x; x < pos2.x; x++)
            {
                for (int y = pos1.y; y < pos2.y; y++)
                {
                    DrawPixel(x, y, content, color);
                }
            }
        }

        public void DrawOutline(Vector2 pos1, Vector2 pos2, int thickness, string content, ConsoleColor color)
        {
            //Top
            for (int x = pos1.x; x < pos2.x; x++)
            {
                for (int y = pos1.y; y < (pos1.y + thickness); y++)
                {
                    DrawPixel(x, y, content, color);
                }
            }

            //Bottom
            for (int x = pos1.x; x < pos2.x; x++)
            {
                for (int y = pos2.y - thickness; y < pos2.y; y++)
                {
                    DrawPixel(x, y, content, color);
                }
            }

            //Right
            for (int x = pos2.x - thickness; x < pos2.x; x++)
            {
                for (int y = pos1.y; y < (pos2.y); y++)
                {
                    DrawPixel(x, y, content, color);
                }
            }

            //Left
            for (int x = pos1.x; x < pos1.x + thickness; x++)
            {
                for (int y = pos1.y; y < (pos2.y); y++)
                {
                    DrawPixel(x, y, content, color);
                }
            }
        }

        public void Draw()
        {
            Console.CursorVisible = false;
            Console.SetWindowSize(101, 40);

            for (int a = 0; a < lines.Length; a++)
            {
                Console.SetCursorPosition(0, a);

                for (int b = 0; b < lines[a].tiles.Length; b++)
                {
                    Console.ForegroundColor = lines[a].tiles[b].color;
                    Console.Write(lines[a].tiles[b].content);
                }
            }
        }
    }
}