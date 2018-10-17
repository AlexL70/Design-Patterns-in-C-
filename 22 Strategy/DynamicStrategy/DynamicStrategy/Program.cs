using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using static System.Console;

namespace DynamicStrategy
{
    class Program
    {
        public enum OutputFormat
        {
            Markdown,
            Html
        }

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
            public void Start(StringBuilder sb) {}

            public void End(StringBuilder sb) {}

            public void AddListItem(StringBuilder sb, string item)
            {
                WriteLine($" * {item}");
            }
        }

        public class TextProcessor
        {
            private readonly StringBuilder _sb = new StringBuilder();
            private IListStrategy _listStrategy;

            public void SetOutputFormat(OutputFormat format)
            {
                switch (format)
                {
                    case OutputFormat.Markdown:
                        _listStrategy = new MarkdownListStrategy();
                        break;
                    case OutputFormat.Html:
                        _listStrategy = new HtmlListStrategy();
                        break;
                }
            }

            public void AppendList(IEnumerable<string> items)
            {
                if (items == null) throw new ArgumentNullException(nameof(items));
                _listStrategy.Start(_sb);
                foreach (var item in items)
                    _listStrategy.AddListItem(_sb, item);
                _listStrategy.End(_sb);
            }

            public StringBuilder Clear()
            {
                return _sb.Clear();
            }

            public override string ToString() => _sb.ToString();
        }

        

        static void Main(string[] args)
        {
            string[] list = new[] {"foo", "bar", "baz"};
            var tp = new TextProcessor();
            tp.SetOutputFormat(OutputFormat.Markdown);
            tp.AppendList(list);
            WriteLine(tp);
            WriteLine();
            tp.Clear();
            tp.SetOutputFormat(OutputFormat.Html);
            tp.AppendList(list);
            WriteLine(tp);
        }
    }
}
