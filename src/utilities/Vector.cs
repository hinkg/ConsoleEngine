namespace ConsoleEngine
{
    public class Vector2
    {
        public int X { get; set; }
        public int Y { get; set; }

        //Constructor
        public Vector2(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
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