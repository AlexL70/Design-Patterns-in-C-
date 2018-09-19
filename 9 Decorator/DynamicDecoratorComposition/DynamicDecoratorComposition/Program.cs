using System;
using static System.Console;

namespace DynamicDecoratorComposition
{
    public interface IShape
    {
        string AsString();
    }

    public class Circle : IShape
    {
        private float _radius;

        public Circle(float radius) => _radius = radius;

        public void Resize(float factor) => _radius *= factor;

        public string AsString() => $"A circle with radius {_radius}";
    }

    public class Square : IShape
    {
        private float _side;

        public Square(float side) => _side = side;

        public string AsString() => $"A square with side {_side}";
    }

    public class ColoredShape : IShape
    {
        private IShape _shape;
        private string _color;

        public ColoredShape(IShape shape, string color)
        {
            _shape = shape ?? throw new ArgumentNullException(nameof(shape));
            _color = color ?? throw new ArgumentNullException(nameof(color));
        }

        public string AsString() => $"{_shape.AsString()} colored with {_color}";
    }

    public class TransparentShape : IShape
    {
        private IShape _shape;
        private float _transparency;

        public TransparentShape(IShape shape, float transparency)
        {
            _shape = shape ?? throw new ArgumentNullException(nameof(shape));
            _transparency = transparency;
        }

        public string AsString() => $"{_shape.AsString()} transparent of {_transparency * 100.0} percent";
    }

    class Program
    {
        static void Main(string[] args)
        {
            var square = new Square(1.23f);
            WriteLine(square.AsString());
            var redSquare = new ColoredShape(square, "red");
            WriteLine(redSquare.AsString());
            var trRedSquare = new TransparentShape(redSquare, 0.25f);
            WriteLine(trRedSquare.AsString());
        }
    }
}
