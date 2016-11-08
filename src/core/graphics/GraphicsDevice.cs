using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace ConsoleEngine.Core.Graphics
{
    public class GraphicsDevice
    {
        public List<IObject> objects;

        public Tile[] view, prevView, map;

        public int width, height;
        public int mapWidth, mapHeight;

        public string windowname;

        public Vector2 camera;

        private int msCounter = 1000;
        private int displayMs;

        public GraphicsDevice(int width, int height, string windowname, int mapWidth, int mapHeight)
        {
            objects = new List<IObject>();

            this.width = width;
            this.height = height;
            this.windowname = windowname;
            this.mapWidth = mapWidth;
            this.mapHeight = mapHeight;

            camera = new Vector2(0, 0);

            Console.Clear();
            Console.CursorVisible = false;
            Console.SetWindowSize(width, height);
            Console.BufferWidth = width + 1;
            Console.BufferHeight = height;

            view = new Tile[width * height];
            prevView = new Tile[view.Length];
            map = new Tile[mapWidth * mapHeight];

            for (int i = 0; i < view.Length; i++)
            {
                view[i] = new Tile();
                prevView[i] = new Tile();
            }
            for (int i = 0; i < map.Length; i++)
                map[i] = new Tile();
        }

        public void Draw()
        {
            for(int i = 0; i < objects.Count; i++) 
            {
                objects[i].Draw(this);
            } 
        }

        public void SetTile(int x, int y, char content, ConsoleColor color)
        {
            if (0 <= x && x < mapWidth
             && 0 <= y && y < mapHeight)
            {
                Tile tile = map[x + y * mapWidth];
                tile.content = content;
                tile.color = color;
            }
        }

        public void UpdateView()
        {
            if (camera.x < 0) camera.x = 0;
            if (camera.y < 0) camera.y = 0;
            if (camera.x > mapWidth  - width)  camera.x = mapWidth  - width; 
            if (camera.y > mapHeight - height) camera.y = mapHeight - height;

            int index = 0;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    view[index++] = map[x + camera.x + (y + camera.y) * mapWidth];
                }
            }
        }

        public void Refresh()
        {
            UpdateView();

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

                    Tile tile = view[index];
                    Tile prevTile = prevView[index++];

                    if (tile.content != prevTile.content || tile.color != prevTile.color)
                    {
                        if (tile.color == color)
                        {
                            content += tile.content;
                        }
                        else
                        {
                            Console.ForegroundColor = color;
                            Console.Write(content);

                            draws++;

                            content = tile.content.ToString();
                            color = tile.color;
                            Console.SetCursorPosition(x, y);
                        }

                        prevTile.content = tile.content;
                        prevTile.color = tile.color;

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

            public ConsoleColor color = ConsoleColor.Black;
        }
    }
}