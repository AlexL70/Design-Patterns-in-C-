using System;

namespace PointExample
{
    class Program
    {
        public enum CoordinateSystem
        {
            Cartesian, Polar
        }

        /// <summary>
        /// Bad approach. Constructor has really non-obvious interface
        /// </summary>
        public class Point
        {
            public double x { get; set; }
            public double y { get; set; }
            
            /// <summary>
            /// <see cref="a"/> and <see cref="b"/> values depend on <see cref="system"/> value
            /// because if <see cref="system"/> is <see cref="CoordinateSystem.Cartesian"/>, then
            /// they are cartesian coordinates (x and y), otherwise they are Polar coordinates
            /// (rho and theta).
            /// </summary>
            /// <param name="a">x if cartesian and rho if polar</param>
            /// <param name="b">y if cartesian and theta if polar</param>
            /// <param name="system"></param>
            public Point(double a, double b, CoordinateSystem system = CoordinateSystem.Cartesian)
            {
                switch (system)
                {
                    case CoordinateSystem.Cartesian:
                        x = a;
                        y = b;
                        break;
                    case CoordinateSystem.Polar:
                        x = a * Math.Cos(b);
                        y = a * Math.Sin(b);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(system), system, null);
                }
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
