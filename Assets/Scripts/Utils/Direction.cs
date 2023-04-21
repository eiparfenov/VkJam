namespace Utils
{
    public enum Direction
    {
        Top, Left, Bottom, Right
    }

    public static class DirectionExtension
    {
        public static Direction Turn(this Direction direction, int times)
        {
            return (Direction)((((int)direction) + times) % 4);
        }

        public static Direction Opposite(this Direction direction)
        {
            return direction.Turn(2);
        }
    }
}