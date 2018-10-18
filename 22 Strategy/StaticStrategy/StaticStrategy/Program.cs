using System;
using System.Collections.Generic;
using System.Text;

namespace StaticStrategy
{
    public interface IListStrategy
    {
        void Start(StringBuilder sb);
        void End(StringBuilder sb);
        void AddListItem(StringBuilder sb, string item);
    }

    public class HtmlListStrategy : IListStrategy
    {
        public void Start(StringBuilder sb)
        {
            sb.AppendLine("<ul>");
        }

        public void End(StringBuilder sb)
        {
            sb.AppendLine("</ul>");
        }

        public void AddListItem(StringBuilder sb, string item)
        {
            sb.AppendLine($"  <li>{item}</li>");
        }
    }

    public class MarkdownListStrategy : IListStrategy
    {
        public void Start(StringBuilder sb) { }

        public void End(StringBuilder sb) { }

        public void AddListItem(StringBuilder sb, string item)
        {
            Console.WriteLine($" * {item}");
        }
    }

    public class TextProcessor<LS> where LS : IListStrategy, new()
    {
        private readonly StringBuilder _sb = new StringBuilder();
        private readonly IListStrategy _listStrategy = new LS();

        public void AppendList(IEnumerable<string> items)
        {
            if (items == null) throw new ArgumentNullException(nameof(items));
            _listStrategy.Start(_sb);
            foreach (var item in items)
                _listStrategy.AddListItem(_sb, item);
            _listStrategy.End(_sb);
        }

        public override string ToString() => _sb.ToString();
    }

    class Program
    {
        static void Main(string[] args)
        {
            string[] list = new[] { "foo", "bar", "baz" };
            var tpm = new TextProcessor<MarkdownListStrategy>();
            tpm.AppendList(list);
            Console.WriteLine(tpm);
            Console.WriteLine();
            var tph = new TextProcessor<HtmlListStrategy>();
            tph.AppendList(list);
            Console.WriteLine(tph);
        }
    }
}
