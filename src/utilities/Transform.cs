namespace ConsoleEngine
{
    public class Transform
    {
        public Vector2 Position { get; private set; }
        
        public void Translate(Vector2 newPosition)
        {
            Position = newPosition;
        }

        //Constructor
        public Transform(Vector2 position)
        {
            this.Position = position;
        }
    }
}