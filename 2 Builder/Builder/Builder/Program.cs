using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace Builder
{
    public class HtmlElement
    {
        public string Text { get; set; }
        public string Name { get; set; }
        public List<HtmlElement> Children { get; set; } = new List<HtmlElement>();

        private const int indentSize = 2;

        public HtmlElement() { }

        public HtmlElement(string name, string text)
        {
            Text = text ?? throw new ArgumentNullException(nameof(text));
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        private string ToStringImpl(int indent)
        {
            StringBuilder sb = new StringBuilder();
            string ind = new string(' ', indent * indentSize);
            sb.AppendLine($"{ind}<{Name}>");
            if (!String.IsNullOrEmpty(Text))
            {
                foreach (string s in Text.Split(Environment.NewLine))
                {
                    sb.AppendLine($"{new string(' ', (indent + 1) * indentSize)}{s}");
                }
            }
            foreach (var child in Children)
            {
                sb.Append(child.ToStringImpl(indent + 1));
            }
            sb.AppendLine($"{ind}</{Name}>");
            return sb.ToString();
        }

        public override string ToString()
        {
            return ToStringImpl(0);
        }
    }

    public class HtmlBuilder
    {
        private HtmlElement root = new HtmlElement();

        public HtmlBuilder(string rootName)
        {
            root.Name = rootName ?? throw new ArgumentNullException();
        }

        public void AddChild(string childName, string childText)
        {
            var e = new HtmlElement(childName, childText);
            root.Children.Add(e);
        }

        public override string ToString()
        {
            return root.ToString();
        }

        public void Clear()
        {
            var name = root.Name;
            root = new HtmlElement(name, "");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var builder = new HtmlBuilder("ul");
            builder.AddChild("li", "hello");
            builder.AddChild("li", "world");
            builder.AddChild("li", @"This world
is full of shit.
Though, we have what
we have.");
            Console.WriteLine(builder);
        }
    }
}
