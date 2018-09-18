using MoreLinq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using static System.Console;

namespace VectorRasterDemo
{
    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"[{X},{Y}]";
        }
    }

    public class Line
    {
        public Point Start { get; set; }
        public Point End { get; set; }

        public Line(Point start, Point end)
        {
            Start = start ?? throw new ArgumentNullException(nameof(start));
            End = end ?? throw new ArgumentNullException(nameof(end));
        }

        public override string ToString()
        {
            return $"{Start.ToString()}-{End.ToString()}";
        }
    }

    public class VectorObject : Collection<Line>
    {

    }

    public class VectorRectangle : VectorObject
    {
        public VectorRectangle(int x, int y, int width, int height)
        {
            Add(new Line(new Point(x,y), new Point(x + width, y)));
            Add(new Line(new Point(x, y), new Point(x, y + height)));
            Add(new Line(new Point(x + width, y), new Point(x + width, y + height)));
            Add(new Line(new Point(x, y + height), new Point(x + width, y + height)));
        }
    }

    public class LineToPointAdapter : Collection<Point>
    {
        private static int _count = 0;

        public LineToPointAdapter(Line line)
        {
            WriteLine($"{++_count}: Generating points to line {line}");

            int left = Math.Min(line.Start.X, line.End.X);
            int right = Math.Max(line.Start.X, line.End.X);
            int top = Math.Min(line.Start.Y, line.End.Y);
            int bottom = Math.Max(line.Start.Y, line.End.Y);
            int dx = right - left;
            int dy = bottom - top;
            if (dx == 0)
            {
                for (int y = top; y <= bottom; y++)
                {
                    Add(new Point(left, y));
                }
            }
            else if(dy == 0)
            {
                for (int x = left; x <= right; x++)
                {
                    Add(new Point(x, top));
                }
            }
        }
    }

    class Demo
    {
        private static readonly List<VectorObject> _vectorObjects = new List<VectorObject>()
        {
            new VectorRectangle(1,1,10,10),
            new VectorRectangle(3,3,6,6)
        };

        public static void DrawPoint(Point p)
        {
            Write($"{p}.");
        }

        static void Main(string[] args)
        {
            Draw();
            Draw();
        }

        private static void Draw()
        {
            foreach (var vo in _vectorObjects)
            {
                foreach (var line in vo)
                {
                    var adapter = new LineToPointAdapter(line);
                    adapter.ForEach(DrawPoint);
                    WriteLine();
                }
            }
        }
    }
}
