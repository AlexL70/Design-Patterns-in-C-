using System;
using System.Collections.Generic;
using System.Linq;

namespace _2_Open_Close
{
    class Program
    {
        public enum Color
        {
            Red, Green, Blue
        }

        public enum Size
        {
            Small, Medium, Large, Huge
        }

        public class Product
        {
            public string Name { get; set; }
            public Color Color { get; set; }
            public Size Size { get; set; }

            public Product(string name, Color color, Size size)
            {
                Name = name ?? throw new ArgumentNullException(paramName: nameof(name));
                Color = color;
                Size = size;
            }

            public override string ToString()
            {
                return $"{this.Name} {this.Color} {this.Size}";
            }
        }

        public interface ISpecification<T>
        {
            bool IsSatisfied(T t);
        }

        public interface IFilter<T>
        {
            IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> spec);
        }

        public class ColorSpecification : ISpecification<Product>
        {
            private Color _color;

            public ColorSpecification(Color color)
            {
                _color = color;
            }

            public bool IsSatisfied(Product t)
            {
                return t.Color == _color;
            }
        }

        public class SizeSpecification : ISpecification<Product>
        {
            private Size _size;

            public SizeSpecification(Size size)
            {
                _size = size;
            }

            public bool IsSatisfied(Product t)
            {
                return t.Size == _size;
            }
        }

        public class AndSpecification<T> : ISpecification<T>
        {
            private ISpecification<T> _first;
            private ISpecification<T> _second;

            public AndSpecification(ISpecification<T> first, ISpecification<T> second)
            {
                _first = first;
                _second = second;
            }

            public bool IsSatisfied(T t)
            {
                return _first.IsSatisfied(t) && _second.IsSatisfied(t);
            }
        }

        public class ProductFilter : IFilter<Product>
        {
            public IEnumerable<Product> Filter(IEnumerable<Product> items, ISpecification<Product> spec)
            {
                foreach (Product item in items)
                {
                    if (spec.IsSatisfied(item))
                    {
                        yield return item;
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            Product[] products = new List<Product>
            {
                new Product("Apple", Color.Green, Size.Small),
                new Product("Tree", Color.Green, Size.Large),
                new Product("House", Color.Blue, Size.Large)
            }.ToArray();

            var filter = new ProductFilter();
            Console.WriteLine("Green products:");
            foreach (Product product in filter.Filter(products, new ColorSpecification(Color.Green)))
            {
                Console.WriteLine(product);
            }
            Console.WriteLine();
            Console.WriteLine("Large products:");
            foreach (Product product in filter.Filter(products, new SizeSpecification(Size.Large)))
            {
                Console.WriteLine(product);
            }
            Console.WriteLine();
            Console.WriteLine("Large green products:");
            foreach (Product product in filter.Filter(products, 
                new AndSpecification<Product>(
                    new ColorSpecification(Color.Green), 
                    new SizeSpecification(Size.Large))))
            {
                Console.WriteLine(product);
            }
        }
    }
}
