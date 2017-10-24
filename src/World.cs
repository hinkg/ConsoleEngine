using System;
using System.Collections.Generic;
using ConsoleEngine.Objects;

namespace ConsoleEngine
{
    public class World
    {
        public List<IGraphicsObject> objects;

        public Tile[] map;
        public Vector2 size;

        public World(Vector2 size)
        {
            objects = new List<IGraphicsObject>();

            this.size = size;

            map = new Tile[size.X * size.Y];

            for (int i = 0; i < map.Length; i++)
            {
                map[i] = new Tile();
            }
        }

        public void SetTile(int x, int y, char content, ConsoleColor color)
        {
            if (0 <= x && x < size.X && 0 <= y && y < size.Y)
            {
                Tile tile = map[x + y * size.X];
                tile.content = content;
                tile.color = color;
            }
        }

        public void Draw()
        {
            for (int i = 0; i < objects.Count; i++)
            {
                objects[i].Draw(this);
            }
        }

        public void Add(IGraphicsObject obj)
        {
            objects.Add(obj);
        }
    }

    public class Tile
    {
        public char content = ' ';
        public ConsoleColor color = ConsoleColor.Black;
    }
}