using System;
using Autofac;
using static System.Console;

namespace Bridge
{
    public interface IRenderer
    {
        void RenderCircle(float radius);
    }

    public class VectorRenderer : IRenderer
    {
        public void RenderCircle(float radius)
        {
            WriteLine($"Drawing a circle of radius {radius}...");
        }
    }

    public class RasterRenderer : IRenderer
    {
        public void RenderCircle(float radius)
        {
            WriteLine($"Drawing pixels for circle of radius {radius}...");
        }
    }

    public abstract class Shape
    {
        protected IRenderer Renderer;

        protected Shape(IRenderer renderer)
        {
            Renderer = renderer ?? throw new ArgumentNullException(nameof(renderer));
        }

        public abstract void Draw();
        public abstract void Resize(float factor);
    }

    public class Circle : Shape
    {
        private float _radius;

        public Circle(IRenderer renderer, float radius) : base(renderer)
        {
            _radius = radius;
        }

        public override void Draw()
        {
            Renderer.RenderCircle(_radius);
        }

        public override void Resize(float factor)
        {
            _radius *= factor;
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            //  Without Autofac
            WriteLine("Without Autofac:");
            IRenderer renderer = new RasterRenderer();
            var circle = new Circle(renderer, 5);
            circle.Draw();
            circle.Resize(0.3f);
            circle.Draw();
            renderer = new VectorRenderer();
            circle = new Circle(renderer, 7);
            circle.Draw();
            WriteLine();

            //  With Autofac
            WriteLine("With Autofac:");
            var cb = new ContainerBuilder();
            cb.RegisterType<VectorRenderer>().As<IRenderer>().SingleInstance();
            cb.Register((c, p) => new Circle(c.Resolve<IRenderer>(), p.Positional<float>(0)));
            using (var c = cb.Build())
            {
                var circleAuto = c.Resolve<Circle>(new PositionalParameter(0, 5.0f));
                circleAuto.Draw();
                circleAuto.Resize(0.3f);
                circleAuto.Draw();
            }
        }
    }
}
