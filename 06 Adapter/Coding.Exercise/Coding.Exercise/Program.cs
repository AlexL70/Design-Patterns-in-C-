using System;

namespace Coding.Exercise
{
    public class Square
    {
        public int Side;
    }

    public interface IRectangle
    {
        int Width { get; }
        int Height { get; }
    }

    public static class ExtensionMethods
    {
        public static int Area(this IRectangle rc)
        {
            return rc.Width * rc.Height;
        }
    }

    public class SquareToRectangleAdapter : IRectangle
    {
        public SquareToRectangleAdapter(Square square)
        {
            _square = square ?? throw new ArgumentNullException(nameof(square));
        }

        private readonly Square _square;

        public int Width => _square.Side;
        public int Height => _square.Side;
    }



    class Program
    {
        static void Main(string[] args)
        {
            var sq = new Square() { Side = 5 };
            Console.WriteLine($"Area is equal to {(new SquareToRectangleAdapter(sq)).Area()}");
        }
    }
}
