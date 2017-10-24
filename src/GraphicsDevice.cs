using System;

namespace ConsoleEngine
{
    public class GraphicsDevice
    {
        public Tile[] view, prevView;

        public int width, height;

        public string windowname;

        public Vector2 camera;

        private float msCounter = 1000;
        private string displayMs, displayFps;
        private long previousTime = DateTime.Now.Ticks;

        private bool error;

        public GraphicsDevice(int width, int height, string windowname)
        {
            this.width = width;
            this.height = height;
            this.windowname = windowname;

            camera = new Vector2(0, 0);

            Console.Clear();
            Console.CursorVisible = false;
            Console.SetWindowSize(width + 1, height + 1);
            Console.BufferWidth = width + 1;
            Console.BufferHeight = height + 1;

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
            if (camera.X < 0)
            {
                camera.X = 0;
            }

            if (camera.Y < 0)
            {
                camera.Y = 0;
            }

            if (camera.X > world.size.X - width)
            {
                camera.X = world.size.X - width;
            }

            if (camera.Y > world.size.Y - height)
            {
                camera.X = world.size.Y - height;
            }

            int index = 0;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    view[index++] = world.map[x + camera.X + (y + camera.Y) * world.size.X];
                }
            }
        }

        public void Refresh(World world)
        {
            UpdateView(world);

            int updates = 0;
            int draws = 0;

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

                            if (Console.WindowWidth < width || Console.WindowHeight < height)
                            {
                                error = true;
                                break;
                            }
                            else if(error)
                            {
                                Console.Clear();
                                error = false;
                            }

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

            long time = DateTime.Now.Ticks;
            float ms = (time - previousTime) / 10000f;
            previousTime = time;

            msCounter += ms;

            while (msCounter >= 1000)
            {
                displayMs = String.Format("{0:0.0}", ms);
                displayFps = String.Format("{0:0.0}", 1000 / ms);
                msCounter -= 1000;
            }

            if (error)
            {
                Console.Title = "ERROR";
            }
            else
            {
                Console.Title = $"{windowname} : {displayFps}fps ({displayMs}ms) : {updates} updates : {draws} draws";
            }
        }
    }
}