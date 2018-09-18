using System;
using System.Collections.Generic;
using System.Text;

namespace GeometricShapes
{
    public class GraphicObject
    {
        public virtual string Name { get; set; } = "Group";
        public string Color { get; set; }

        private readonly Lazy<List<GraphicObject>> _children = new Lazy<List<GraphicObject>>();
        public List<GraphicObject> Children => _children.Value;

        private void Print(StringBuilder sb, int depth)
        {
            sb.Append(new string('*', depth))
                .Append(String.IsNullOrEmpty(Color) ? String.Empty : $"{Color} ")
                .AppendLine($"{Name}");
            foreach (var child in Children)
            {
                child.Print(sb, depth + 1);
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            Print(sb, 0);
            return sb.ToString();
        }
    }

    public class Circle : GraphicObject
    {
        public override string Name => nameof(Circle);
    }

    public class Square : GraphicObject
    {
        public override string Name => nameof(Square);
    }

    class Program
    {
        static void Main(string[] args)
        {
            var drawing = new GraphicObject() {Name = "My New Drawing"};
            drawing.Children.Add(new Square() { Color = "Red"});
            drawing.Children.Add(new Circle() { Color = "Yellow"});
            var group = new GraphicObject();
            group.Children.Add(new Circle() {Color = "Green"});
            group.Children.Add(new Square() {Color = "Blue"});
            drawing.Children.Add(group);
            Console.WriteLine(drawing);
        }
    }
}
