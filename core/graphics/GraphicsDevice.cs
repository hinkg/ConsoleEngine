using System;
using ConsoleGame.Core.Objects;

namespace ConsoleGame.Core.Graphics
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

        public void Draw(Object obj, objType objType)
        {
            switch (objType)
            {
                case objType.text:
                    DrawText((Text)obj);
                    return;
                case objType.line:
                    DrawLine((Line)obj);
                    return;
                case objType.rectangle:
                    DrawRectangle((Rectangle)obj);
                    return;
                case objType.outline:
                    DrawOutline((Outline)obj);
                    return;
            }
        }

        public void DrawPixel(int x, int y, char content, ConsoleColor color)
        {
            rows[y].tiles[x].content = content;
            rows[y].tiles[x].color = color;
        }

        private void DrawText(Text text)
        {
            int charOffset = 0;

            for (int x = text.position.x; x < text.position.x + text.content.Length; x++)
            {
                DrawPixel(x, text.position.y, text.content[charOffset++], text.color);
            }
        }

        public void DrawLine(Line line)
        {
            int x1 = line.position1.x;
            int y1 = line.position1.y;
            int x2 = line.position2.x;
            int y2 = line.position2.y;

            int dx = Math.Abs(x2 - x1);
            int dy = Math.Abs(y2 - y1);

            int sx = x1 < x2 ? 1 : -1;
            int sy = y1 < y2 ? 1 : -1;

            int err = dx - dy;

            int charOffset = 0;

            while (true)
            {
                DrawPixel(x1, y1, line.content[charOffset++], line.color);

                if (charOffset >= line.content.Length)
                    charOffset = 0;

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

        public void DrawRectangle(Rectangle rect)
        {
            int charOffset = 0;

            for (int y = rect.position1.y; y < rect.position2.y; y++)
            {
                for (int x = rect.position1.x; x < rect.position2.x; x++)
                {
                    DrawPixel(x, y, rect.content[charOffset++], rect.color);

                    if (charOffset >= rect.content.Length)
                        charOffset = 0;
                }
            }
        }

        public void DrawOutline(Outline outline)
        {
            Vector2 spaceStart = new Vector2(outline.position1.x + outline.thicknessX, outline.position1.y + outline.thicknessY);
            Vector2 spaceEnd = new Vector2(outline.position2.x - outline.thicknessX, outline.position2.y - outline.thicknessY);

            int charOffset = 0;

            for (int y = outline.position1.y; y < outline.position2.y; y++)
            {
                for (int x = outline.position1.x; x < outline.position2.x; x++)
                {
                    if (spaceStart.y <= y && y < spaceEnd.y)
                    {
                        if (spaceStart.x <= x && x < spaceEnd.x)
                            continue;
                        else if (x == spaceEnd.x)
                            charOffset = 0;
                    }

                    DrawPixel(x, y, outline.content[charOffset++], outline.color);

                    if (charOffset >= outline.content.Length)
                        charOffset = 0;
                }

                charOffset = 0;
            }
            return;
        }

        public void Refresh()
        {
            Console.CursorVisible = false;
            Console.SetWindowSize(101, 40);

            for (int a = 0; a < rows.Length; a++)
            {
                Console.SetCursorPosition(0, a);

                for (int b = 0; b < rows[a].tiles.Length; b++)
                {
                    Console.ForegroundColor = rows[a].tiles[b].color;
                    Console.Write(rows[a].tiles[b].content);
                }
            }
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