namespace Day_03
{
    public class Vector
    {
        public int Distance { get; }
        public char Direction { get; }

        private Vector(char direction, int distance)
        {
            Distance = distance;
            Direction = direction;
        }

        public static Vector Create(string input) 
            => new Vector(input[0], int.Parse(input.Substring(1)));
    }
}
