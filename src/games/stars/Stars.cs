using System;
using System.Threading;
using ConsoleEngine.Core;
using ConsoleEngine.Objects;

namespace ConsoleGame
{
    public class Stars : Game
    {
        private Random random = new Random();

        private float SPREAD = 64;
        private float SPEED = 0.75f;
        private int STARS = 100;

        private string[] args;

        private int frameCounter;

        private Vector3[] stars;
        private int[,] trail;

        private GraphicsDevice graphics;
        private World world;
        private InputHandler input;

        private Text exitInfoText;

        private bool exit;

        public Stars(string[] args)
        {
            this.args = args;
        }

        public override void Load()
        {
            stars = new Vector3[STARS];
            trail = new int[100,40];

            for (int i = 0; i < STARS; i++)
                RandomizeStar(i);

            graphics = new GraphicsDevice(100, 40, "3D Stars 0.1");
            world = new World(new Vector2(100, 40));
            input = new InputHandler();

            new Rectangle(" ", ConsoleColor.Black, new Vector2(100, 40), new Vector2(0, 0)).Add(world);
            exitInfoText = new Text("Hold escape to go back.", ConsoleColor.Green, new Vector2(1, 1));
            exitInfoText.Add(world);

            while (!exit)
            {
                if (frameCounter == 200)
                    world.objects.Remove(exitInfoText);

                Update();
                Draw();

                frameCounter++;

                Thread.Sleep(10);
            }
        }

        public override void Unload()
        {
            stars = null;
            graphics = null;

            exit = true;
        }

        public void RandomizeStar(int star)
        {
            stars[star] = new Vector3(
                2 * ((float)random.NextDouble() - 0.5f) * SPREAD,
                2 * ((float)random.NextDouble() - 0.5f) * SPREAD,
                ((float)random.NextDouble() - 0.000001f) * SPREAD
            );
        }

        public override void Update()
        {
            if(input.Key == ConsoleKey.Escape)
            {
                Unload();
                new Launcher(args).Load();
            }
        }

        public override void Draw()
        {
            world.Draw();

            for (int i = 0; i < STARS; i++)
            {
                stars[i].z -= SPEED;

                if(stars[i].z <= 0)
                    RandomizeStar(i);
                
                int x = (int)((stars[i].x / stars[i].z) * 50 + 50);
                int y = (int)((stars[i].y / stars[i].z) * 20 + 20);

                if(x < 0 || x >= 100 || y < 0 || y >= 40)
                    RandomizeStar(i);
                else
                    trail[x,y] = 7;
            }

            for (int y = 0; y < 40; y++)
                for (int x = 0; x < 100; x++)
                {
                    ConsoleColor color = ConsoleColor.Black;
                    char content = ' ';

                    switch (trail[x,y])
                    {
                        case 7: content = ','; color = ConsoleColor.White; break;
                        case 6: content = ','; color = ConsoleColor.Yellow; break;
                        case 5: content = '.'; color = ConsoleColor.Yellow; break;
                        case 4: content = '.'; color = ConsoleColor.Red; break;
                        case 3: content = '.'; color = ConsoleColor.Red; break;
                        case 2: content = '.'; color = ConsoleColor.DarkRed; break;
                        case 1: content = '.'; color = ConsoleColor.DarkRed; break;
                    }

                    world.SetTile(x, y, content, color);

                    if (trail[x,y] > 0)
                        trail[x,y]--;
                }

            graphics.Refresh(world);
        }

        public override void Quit() {}
    }
}