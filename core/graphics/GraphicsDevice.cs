using System;

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