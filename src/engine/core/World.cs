using System;
using System.Linq;
using System.Collections.Generic;
using ConsoleEngine;
using ConsoleEngine.Objects;

namespace ConsoleEngine.Core
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

            map = new Tile[size.x * size.y];

            for (int i = 0; i < map.Length; i++)
            {
                map[i] = new Tile();
            }
        }

        public void SetTile(int x, int y, char content, ConsoleColor color)
        {
            if (0 <= x && x < size.x && 0 <= y && y < size.y)
            {
                Tile tile = map[x + y * size.x];
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
    }

    public class Tile
    {
        public char content = ' ';
        public ConsoleColor color = ConsoleColor.Black;
    }
}