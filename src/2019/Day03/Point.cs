using System;
using System.Collections.Generic;
using System.Linq;

namespace Day03
{
    public class Point
    {
        public char Direction { get; }
        public int X { get; }
        public int Y { get; }

        private Point(int x, int y, char direction)
        {
            X = x;
            Y = y;
            Direction = direction;
        }

        public static Point Create(int x, int y, char direction) 
            => new Point(x, y, direction);

        public override bool Equals(object obj)
        {
            if (obj is Point point)
                return point.X == X && point.Y == Y;
            return false;
        }

        public int DistanceTo(Point point)
        {
            var normalizedX = X < 0 ? X * -1 : X;
            var normalizedY = Y < 0 ? Y * -1 : Y;

            return normalizedX - point.X + (normalizedY - point.Y);
        }

        public override string ToString() => $"{X}|{Y}";

        public IEnumerable<Point> MoveTo(Vector vector)
        {
            const char right = 'R';
            const char left = 'L';
            const char up = 'U';
            const char down = 'D';

            return vector.Direction switch
            {
                right => Enumerable.Range(1, vector.Distance).Select(i => Create(X, Y + i, vector.Direction)),
                left => Enumerable.Range(1, vector.Distance).Select(i => Create(X, Y - i, vector.Direction)),
                up => Enumerable.Range(1, vector.Distance).Select(i => Create(X - i, Y, vector.Direction)),
                down => Enumerable.Range(1, vector.Distance).Select(i => Create(X + i, Y, vector.Direction)),
                _ => throw new NotImplementedException(),
            };
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }
    }
}
