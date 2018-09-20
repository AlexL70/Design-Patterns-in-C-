using System;

namespace PointExample
{
    public enum CoordinateSystem
    {
        Cartesian, Polar
    }

    public class Point
    {
        public double x { get; set; }
        public double y { get; set; }

        private Point() {}

        public override string ToString()
        {
            return $"x = {x}, y = {y}";
        }

        public static class Factory
        {
            public static Point NewCartesianPoint(double x, double y)
            {
                return new Point { x = x, y = y };
            }

            public static Point NewPolarPoint(double rho, double theta)
            {
                return new Point { x = rho * Math.Cos(theta), y = rho * Math.Sin(theta) };
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var point = Point.Factory.NewPolarPoint(1.0, Math.PI / 2);
            Console.WriteLine(point);
        }
    }
}
