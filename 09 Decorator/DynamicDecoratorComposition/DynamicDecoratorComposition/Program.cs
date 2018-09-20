using System;
using static System.Console;

namespace DynamicDecoratorComposition
{
    public abstract class Shape
    {
        public abstract string AsString();
    }

    public class Circle : Shape
    {
        private float _radius;

        public Circle() : this(1f) { }

        public Circle(float radius) => _radius = radius;

        public void Resize(float factor) => _radius *= factor;

        public override string AsString() => $"A circle with radius {_radius}";
    }

    public class Square : Shape
    {
        private float _side;

        public Square(float side) => _side = side;

        public Square() : this(1f) { }

        public override string AsString() => $"A square with side {_side}";
    }

    public class ColoredShape : Shape
    {
        private Shape _shape;
        private string _color;

        public ColoredShape(Shape shape, string color)
        {
            _shape = shape ?? throw new ArgumentNullException(nameof(shape));
            _color = color ?? throw new ArgumentNullException(nameof(color));
        }

        public override string AsString() => $"{_shape.AsString()} colored with {_color}";
    }

    public class TransparentShape : Shape
    {
        private Shape _shape;
        private float _transparency;

        public TransparentShape(Shape shape, float transparency)
        {
            _shape = shape ?? throw new ArgumentNullException(nameof(shape));
            _transparency = transparency;
        }

        public override string AsString() => $"{_shape.AsString()} transparent of {_transparency * 100.0} percent";
    }

    public class ColoredShape<T> : Shape where T : Shape, new()
    {
        private string _color;
        private T _shape = new T();

        public ColoredShape() : this("black") { }

        public ColoredShape(string color)
        {
            _color = color ?? throw new ArgumentNullException(nameof(color));
        }

        public override string AsString() => $"{_shape.AsString()} colored with {_color}";
    }

    public class TransparentShape<T> : Shape where T : Shape, new()
    {
        private float _transparency;
        private T _shape = new T();

        public TransparentShape() : this(0) { }

        public TransparentShape(float transparency) => _transparency = transparency;

        public override string AsString() => $"{_shape.AsString()} transparent of {_transparency * 100.0} percent";
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            Shape square = new ColoredShape<Square>("red");
            WriteLine(square.AsString());
            Shape trCircle = new TransparentShape<ColoredShape<Circle>>(0.25f);
            WriteLine(trCircle.AsString());
            square = new Square(1.23f);
            WriteLine(square.AsString());
            var redSquare = new ColoredShape(square, "red");
            WriteLine(redSquare.AsString());
            var trRedSquare = new TransparentShape(redSquare, 0.25f);
            WriteLine(trRedSquare.AsString());
        }
    }
}
