﻿using MoreLinq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using static System.Console;

namespace VectorRasterDemo
{
    public class Point
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"[{X},{Y}]";
        }

        protected bool Equals(Point other)
        {
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == this.GetType() && Equals((Point) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X * 397) ^ Y;
            }
        }
    }

    public class Line
    {
        public Point Start { get; private set; }
        public Point End { get; private set; }

        public Line(Point start, Point end)
        {
            Start = start ?? throw new ArgumentNullException(nameof(start));
            End = end ?? throw new ArgumentNullException(nameof(end));
        }

        public override string ToString()
        {
            return $"{Start}-{End}";
        }

        protected bool Equals(Line other)
        {
            return Equals(Start, other.Start) && Equals(End, other.End);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == this.GetType() && Equals((Line) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Start != null ? Start.GetHashCode() : 0) * 397) ^ (End != null ? End.GetHashCode() : 0);
            }
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

    public class LineToPointAdapter : IEnumerable<Point>
    {
        private static int _count = 0;
        private static Dictionary<int,List<Point>> _cache = new Dictionary<int, List<Point>>();

        public LineToPointAdapter(Line line)
        {
            var hash = line.GetHashCode();
            if (_cache.ContainsKey(hash))
            {
                return;
            }
            
            var points = new List<Point>();

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
                    points.Add(new Point(left, y));
                }
            }
            else if(dy == 0)
            {
                for (int x = left; x <= right; x++)
                {
                    points.Add(new Point(x, top));
                }
            }

            _cache.Add(hash, points);
        }

        public IEnumerator<Point> GetEnumerator()
        {
            return _cache.Values.SelectMany(x => x).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
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
            LineToPointAdapter adapter = null;
            foreach (var vo in _vectorObjects)
            {
                foreach (var line in vo)
                {
                    adapter = new LineToPointAdapter(line);
                    //adapter.ForEach(DrawPoint);
                    //WriteLine();
                }
            }
            adapter?.ForEach(DrawPoint);
        }
    }
}
