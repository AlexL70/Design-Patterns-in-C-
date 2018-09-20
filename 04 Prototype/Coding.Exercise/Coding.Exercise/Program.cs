using System;
using System.IO;

namespace Coding.Exercise
{
    public class Point
    {
        public int X, Y;

        public override string ToString()
        {
            return $"{nameof(X)}: {X}, {nameof(Y)}: {Y}";
        }
    }

    public class Line
    {
        public Point Start, End;

        public Line DeepCopy()
        {
            return new Line
            {
                Start = new Point() {X = this.Start.X, Y = this.Start.Y},
                End = new Point() {X = this.End.X, Y = this.End.Y}
            };
        }

        public override string ToString()
        {
            return $"{nameof(Start)}: {Start}, {nameof(End)}: {End}";
        }
    }

    public class Program
    {
        public static void Main()
        {
            var p1 = new Line();
            p1.Start = new Point() {X = 1, Y = 2};
            p1.End = new Point() {X = 3, Y = 4};
            var p2 = p1.DeepCopy();
            p2.End.Y = 6;
            Console.WriteLine(p1);
            Console.WriteLine(p2);
        }
    }

}