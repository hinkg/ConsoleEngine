using System;
using System.Diagnostics;
using System.Collections.Generic;
using ConsoleEngine.Objects;

namespace ConsoleEngine.Core
{
    public class GraphicsDevice
    {
        public Tile[] view, prevView;

        public int width, height;

        public string windowname;

        public Vector2 camera;

        private int msCounter = 1000;
        private int displayMs;

        public GraphicsDevice(int width, int height, string windowname)
        {
            this.width = width;
            this.height = height;
            this.windowname = windowname;

            camera = new Vector2(0, 0);

            Console.Clear();
            Console.CursorVisible = false;
            Console.SetWindowSize(width, height);
            Console.BufferWidth = width + 1;
            Console.BufferHeight = height;

            view = new Tile[width * height];
            prevView = new Tile[view.Length];

            for (int i = 0; i < view.Length; i++)
            {
                view[i] = new Tile();
                prevView[i] = new Tile();
            }
        }

        public void UpdateView(World world)
        {
            if (camera.x < 0) camera.x = 0;
            if (camera.y < 0) camera.y = 0;
            if (camera.x > world.size.x - width) camera.x = world.size.x - width;
            if (camera.y > world.size.y - height) camera.y = world.size.y - height;

            int index = 0;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    view[index++] = world.map[x + camera.x + (y + camera.y) * world.size.x];
                }
            }
        }

        public void Refresh(World world)
        {
            UpdateView(world);

            int updates = 0;
            int draws = 0;

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            /* This complicated rendering technique tries to be as efficient as possible, by
             * drawing only what is necessary. Two optimizations exist:
             *
             *    1: Text can only be written with one color at a time. So if a tile's color is the
             *       same as the color before it, it can be rendered together with it, making long
             *       strings of the same color rendered only once, as opposed to multiple times.
             *
             *    2: Writing the same tile every frame makes no difference. If a tile is unchanged
             *       from the last frame, it can be ignored on the next frame.
             */

            // Index for getting which tile to render
            int index = 0;

            // For each row on screen
            for (int y = 0; y < height; y++)
            {
                // For writing consecutive tiles with the same color
                string content = "";
                ConsoleColor color = ConsoleColor.Black;

                // For each tile on that row
                for (int x = 0; x < width; x++)
                {
                    // Get tile and its previous state
                    Tile tile = view[index];
                    Tile prevTile = prevView[index];
                    index++;

                    // If the tile has changed since the last frame
                    if (tile.content != prevTile.content || tile.color != prevTile.color)
                    {
                        // If the tile's color is the same as the consecutive tiles
                        // and that THERE IS content in the first place
                        if (tile.color == color && content.Length > 0)
                        {
                            // Add the tile's content to the consecutive tiles
                            content += tile.content;
                        }
                        else // If the tile has a different color as the consecutive tiles
                             // or that there is no content
                        {
                            // If there is content to write
                            if (content.Length > 0)
                            {
                                // Set the color and write it
                                Console.ForegroundColor = color;
                                Console.Write(content);

                                // Count the render
                                draws++;
                            }

                            // Create new consecutive tiles
                            content = tile.content.ToString();
                            color = tile.color;

                            // Prepare cursor position for next render
                            Console.SetCursorPosition(x, y);
                        }

                        // Update the tile, current content becomes previous content
                        prevTile.content = tile.content;
                        prevTile.color = tile.color;

                        // Count the update
                        updates++;
                    }
                    else if (content.Length > 0) // If the tile is unchanged, and there is content to render
                    {
                        // Set the color and write it
                        Console.ForegroundColor = color;
                        Console.Write(content);

                        // Count the render
                        draws++;

                        // Clear the content
                        content = "";
                    }

                    // If we are on the end of the row, and there is unrendered content
                    if (x == width - 1 && content.Length > 0)
                    {
                        // Set the color and write it
                        Console.ForegroundColor = color;
                        Console.Write(content);

                        // Count the render
                        draws++;
                    }
                }
            }

            stopwatch.Stop();

            int ms = (int)stopwatch.ElapsedMilliseconds + 10;
            if (ms == 0) ms = 1;

            msCounter += ms;

            while (msCounter >= 1000)
            {
                displayMs = ms;
                msCounter -= 1000;
            }

            Console.Title = $"{windowname} : {1000 / displayMs}fps ({displayMs}ms) : {updates} updates : {draws} draws";
        }
    }
}