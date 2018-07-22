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

        public class ProductFilter
        {
            public static IEnumerable<Product> FilterBySize(IEnumerable<Product> products, Size size)
            {
                foreach (Product product in products)
                {
                    if (product.Size == size)
                    {
                        yield return product;
                    }
                }
            }

            public static IEnumerable<Product> FilterByColor(IEnumerable<Product> products, Color color)
            {
                foreach (Product product in products)
                {
                    if (product.Color == color)
                    {
                        yield return product;
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

            var greenProducts = ProductFilter.FilterByColor(products, Color.Green).ToArray();
            foreach (Product product in greenProducts)
            {
                Console.WriteLine(product);
            }
        }
    }
}
