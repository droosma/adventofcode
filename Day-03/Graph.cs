using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day_03
{
    public class Graph
    {
        private List<Point> _points;
        private Point _center;

        private Graph(Point center, 
                     List<Point> points)
        {
            _points = points;
            _center = center;
        }

        public static Graph From(Point center)
            => new Graph(center, new List<Point>());

        public static Graph Create
            => new Graph(Point.Create(0, 0, ' '), new List<Point>());

        public IEnumerable<Point> Intersections(Graph graph) 
            => _points.Intersect(graph._points);

        public Point Draw(Vector vector)
        {
            if (!_points.Any())
            {
                _points.AddRange(_center.MoveTo(vector));
                return _points.Last();
            }

            var last = _points.Last();
            _points.AddRange(last.MoveTo(vector));
            return _points.Last();
        }

        public override string ToString()
        {
            var grid = new StringBuilder();

            var rows = _points.Select(p => p.X);
            var rowStart = rows.Min();
            var rowEnd = rows.Max();
            var rowRange = Enumerable.Range(rowStart, rowEnd - rowStart + 1);

            var columns = _points.Select(p => p.Y);
            var columnStart = columns.Min();
            var columnEnd = columns.Max();
            var columnRange = Enumerable.Range(columnStart, columnEnd - columnStart + 1);

            grid.AppendLine("   " + string.Join("", columnRange));
            grid.AppendLine("   " + new string('-', columnEnd));

            foreach (var rowIndex in rowRange) 
            {
                var row = new StringBuilder();
                foreach (var columnIndex in columnRange)
                {
                    if (rowIndex == 0 && columnIndex == 0)
                    {
                        row.Append("o");
                        continue;
                    }

                    var point = _points.FirstOrDefault(p => p.X == rowIndex && p.Y == columnIndex);

                    if (point == null)
                    {
                        row.Append(".");
                    }
                    else
                    {
                        if (point.Direction == 'U' || point.Direction == 'D')
                            row.Append("|");
                        else
                            row.Append("-");
                    }
                }

                var prefix = rowIndex > -1 ? " " : "";
                grid.AppendLine($"{prefix}{rowIndex}|{row}");
            }

            return grid.ToString();
        }
    }
}
