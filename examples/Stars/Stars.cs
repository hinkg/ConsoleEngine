using System;
using System.Threading;
using ConsoleEngine;
using ConsoleEngine.Objects;

namespace Stars
{
    public class Stars : Game
    {
        private static readonly float SPREAD = 64;
        private static readonly float SPEED = 0.75f;
        private static readonly int STARS = 100;

        private Random random;

        private Vector3[] stars;
        private int[,] trail;

        private GraphicsDevice graphics;
        private World world;
        private InputHandler input;

        private Text exitInfoText;

        public Stars() : base() {}

        protected override void Load()
        {
            random = new Random();

            stars = new Vector3[STARS];
            trail = new int[100,40];

            for (int i = 0; i < STARS; i++)
            {
                RandomizeStar(i);
            }

            graphics = new GraphicsDevice(100, 40, "3D Stars");
            world = new World(new Vector2(100, 40));
            input = new InputHandler();

            world.Add(new Rectangle(" ", ConsoleColor.Black, new Vector2(100, 40), new Vector2(0, 0)));
            exitInfoText = new Text("Press escape to exit.", ConsoleColor.Green, new Vector2(1, 1));
            world.Add(exitInfoText);
           
        }

        protected override void Unload()
        {
            stars = null;
            graphics = null;
            world = null;
        }

        private void RandomizeStar(int star)
        {
            stars[star] = new Vector3(
                2 * ((float)random.NextDouble() - 0.5f) * SPREAD,
                2 * ((float)random.NextDouble() - 0.5f) * SPREAD,
                ((float)random.NextDouble() + 0.000001f) * SPREAD
            );
        }

        protected override void Update()
        {
            Thread.Sleep(3);

            if (frameCounter == 200)
            {
                world.objects.Remove(exitInfoText);
            }

            ConsoleKey key = input.GetKey();

            if(key == ConsoleKey.Escape)
            {
                Stop();
            }
        }

        protected override void Draw()
        {
            world.Draw();

            for (int i = 0; i < STARS; i++)
            {
                stars[i].z -= SPEED;

                if(stars[i].z <= 0)
                {
                    RandomizeStar(i);
                }

                int x = (int)((stars[i].x / stars[i].z) * 50 + 50);
                int y = (int)((stars[i].y / stars[i].z) * 20 + 20);

                if(x < 0 || x >= 100 || y < 0 || y >= 40)
                {
                    RandomizeStar(i);
                }
                else
                {
                    trail[x,y] = 6;
                }
            }

            for (int y = 0; y < 40; y++)
            {
                for (int x = 0; x < 100; x++)
                {
                    if (trail[x,y] == 0)
                    {
                        continue;
                    }

                    ConsoleColor color = ConsoleColor.Black;
                    char content = ' ';

                    switch (trail[x,y])
                    {
                        case 6: content = ','; color = ConsoleColor.White; break;
                        case 5: content = ','; color = ConsoleColor.Yellow; break;
                        case 4: content = '.'; color = ConsoleColor.Yellow; break;
                        case 3: content = '.'; color = ConsoleColor.Red; break;
                        case 2: content = '.'; color = ConsoleColor.Red; break;
                        case 1: content = '.'; color = ConsoleColor.DarkRed; break;
                    }

                    world.SetTile(x, y, content, color);

                    if (trail[x,y] > 0)
                    {
                        trail[x,y]--;
                    }
                }
            }

            graphics.Refresh(world);
        }
    }
}