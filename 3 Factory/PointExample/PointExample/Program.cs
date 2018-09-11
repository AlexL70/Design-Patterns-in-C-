using System;

namespace PointExample
{
    public enum CoordinateSystem
    {
        Cartesian, Polar
    }

    public static class PointFactory
    {
        public static Point NewCartesianPoint(double x, double y)
        {
            return new Point() { x = x, y = y};
        }

        public static Point NewPolarPoint(double rho, double theta)
        {
            return new Point() { x = rho * Math.Cos(theta), y = rho * Math.Sin(theta)};
        }
    }

    public class Point
    {
        public double x { get; set; }
        public double y { get; set; }

        public Point() {}

        public override string ToString()
        {
            return $"x = {x}, y = {y}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var point = PointFactory.NewPolarPoint(1.0, Math.PI / 2);
            Console.WriteLine(point);
        }
    }
}
