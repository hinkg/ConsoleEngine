namespace ConsoleEngine.Core
{
    public class Vector2
    {
        public int x { get; set; }
        public int y { get; set; }

        //Constructor
        public Vector2(int X, int Y)
        {
            x = X;
            y = Y;
        }
    }

    public class Vector3
    {
        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }

        //Constructor
        public Vector3(float X, float Y, float Z)
        {
            x = X;
            y = Y;
            z = Z;
        }
    }
}