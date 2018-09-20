using static System.Console;

namespace Coding.Exercise
{
    public interface IRenderer
    {
        string WhatToRenderAs { get; }
    }

    public abstract class Shape
    {
        protected IRenderer Renderer;

        protected Shape(IRenderer renderer)
        {
            Renderer = renderer;
        }

        public string Name { get; protected set; }

        public override string ToString()
        {
            return $"Drawing {Name} as {Renderer.WhatToRenderAs}";
        }
    }

    public class Triangle : Shape
    {
        public Triangle(IRenderer renderer) : base(renderer)
        {
            Name = "Triangle";
        }
    }

    public class Square : Shape
    {
        public Square(IRenderer renderer) : base(renderer)
        {
            Name = "Square";
        }
    }

    public class VectorRenderer : IRenderer
    {
        public string WhatToRenderAs => "lines";
    }

    public class RasterRenderer : IRenderer
    {
        public string WhatToRenderAs => "pixels";
    }

    class Program
    {
        static void Main(string[] args)
        {
            IRenderer renderer = new RasterRenderer();
            Shape shape = new Triangle(renderer);
            WriteLine(shape);
            renderer = new VectorRenderer();
            shape = new Square(renderer);
            WriteLine(shape);
        }
    }
}
