using System;

namespace PointExample
{
    public enum CoordinateSystem
    {
        Cartesian, Polar
    }

    public class Point
    {
        //  factory method
        public static Point NewCartesianPoint(double x, double y)
        {
            return new Point(x, y);
        }

        //  another factory method
        public static Point NewPolarPoint(double rho, double theta)
        {
            return new Point(rho * Math.Cos(theta), rho * Math.Sin(theta));
        }

        public double x { get; set; }
        public double y { get; set; }

        private Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public override string ToString()
        {
            return $"x = {x}, y = {y}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var point = Point.NewPolarPoint(1.0, Math.PI / 2);
            Console.WriteLine(point);
        }
    }
}
