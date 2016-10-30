using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleGame.Core.Graphics
{
    public class GraphicsDevice
    {
        public Line[] lines;

        public void Load()
        {
            lines = new Line[29];

            for (int a = 0; a < lines.Length; a++)
            {
                lines[a] = new Line();
                lines[a].tiles = new Tile[28];

                for (int b = 0; b < lines[a].tiles.Length; b++)
                {
                    lines[a].tiles[b] = new Tile();
                    lines[a].tiles[b].content = " ";
                }
            }
        }

        public void DrawPixel(int x, int y, string content)
        {
            lines[y].tiles[x].content = content;
        }

        public void DrawLine(Vector2 start, Vector2 end, string content)
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

            while(true)
            {
                DrawPixel(x1, y1, content);

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

        public void DrawFill(Vector2 pos1, Vector2 pos2, string content)
        {
            for (int x = pos1.x; x < pos2.x; x++)
            {
                for (int y = pos1.y; y < pos2.y; y++)
                {
                    lines[y].tiles[x].content = content;
                }
            }
        }

        public void DrawOutline(Vector2 pos1, Vector2 pos2, int thickness, string content)
        {
            //Top
            for (int x = pos1.x; x < pos2.x; x++)
            {
                for (int y = pos1.y; y < (pos1.y + thickness); y++)
                {
                    lines[y].tiles[x].content = content;
                }
            }

            //Bottom
            for (int x = pos1.x; x < pos2.x; x++)
            {
                for (int y = pos2.y - thickness; y < pos2.y; y++)
                {
                    lines[y].tiles[x].content = content;
                }
            }

            //Right
            for (int x = pos2.x - thickness; x < pos2.x; x++)
            {
                for (int y = pos1.y; y < (pos2.y); y++)
                {
                    lines[y].tiles[x].content = content;
                }
            }

            //Left
            for (int x = pos1.x; x < pos1.x + thickness; x++)
            {
                for (int y = pos1.y; y < (pos2.y); y++)
                {
                    lines[y].tiles[x].content = content;
                }
            }
        }

        public void Draw()
        {
            for (int a = 0; a < lines.Length; a++)
            {   
                string content = "";

                for (int b = 0; b < lines[a].tiles.Length; b++)
                {
                    Console.SetCursorPosition(0, a);
                    content += lines[a].tiles[b].content + " ";
                }

                Console.Write(content);
            }
        }
    }
}