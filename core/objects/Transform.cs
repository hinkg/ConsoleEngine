namespace ConsoleEngine.Core.Objects
{
    public class Transform
    {
        public Vector2 position { get; private set; }
        
        public void Translate(Vector2 newPosition)
        {
            position = newPosition;
        }

        //Constructor
        public Transform(Vector2 position)
        {
            this.position = position;
        }
    }
}