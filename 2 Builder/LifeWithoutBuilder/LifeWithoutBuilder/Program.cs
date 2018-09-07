using System;
using System.Text;

namespace LifeWithoutBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            var hello = "hello";
            var sb = new StringBuilder();
            sb.Append("<p>");
            sb.Append(hello);
            sb.Append("</p>");
            Console.WriteLine(sb);

            var words = new string[] { "hello", "world"};
            sb.Clear();
            sb.Append("<ul>");
            foreach (var word in words)
            {
                sb.Append("<li>");
                sb.Append(word);
                sb.Append("</li>");
            }
            sb.Append("</ul>");
            Console.WriteLine(sb);
        }
    }
}
