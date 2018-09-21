using System;
using System.Collections.Generic;
using System.Text;

namespace TextFormatting
{
    public class FormattedText
    {
        private readonly string _planeText;
        private bool[] _capitalize;

        public FormattedText(string planeText)
        {
            _planeText = planeText ?? throw new ArgumentNullException(nameof(planeText));
            _capitalize = new bool[_planeText.Length];
        }

        public void Capitalize(int start, int end)
        {
            for (int i = start; i <= end; i++)
            {
                _capitalize[i] = true;
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (var i = 0; i < _planeText.Length; i++)
            {
                sb.Append(_capitalize[i] ? Char.ToUpper(_planeText[i]) : _planeText[i]);
            }

            return sb.ToString();
        }
    }

    public class BetterFormattedText
    {
        private readonly string _planeText;
        private readonly List<TextRange> _formatting = new List<TextRange>();

        public BetterFormattedText(string planeText)
        {
            _planeText = planeText ?? throw new ArgumentNullException(nameof(planeText));
        }

        public TextRange GetRange(int start, int end)
        {
            var range = new TextRange {Start = start, End = end};
            _formatting.Add(range);
            return range;
        }

        public class TextRange
        {
            public int Start, End;
            public bool Capitalize;

            public bool Covers(int position)
            {
                return Start <= position && position <= End;
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (var i = 0; i < _planeText.Length; i++)
            {
                var c = _planeText[i];
                foreach (var range in _formatting)
                {
                    if (range.Covers(i) && range.Capitalize)
                    {
                        c = Char.ToUpper(c);
                        break;
                    }
                }
                sb.Append(c);
            }

            return sb.ToString();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var ft = new FormattedText("This is a brave new world.");
            ft.Capitalize(10, 14);
            Console.WriteLine(ft);

            var bft = new BetterFormattedText("This is a brave new world.");
            bft.GetRange(10, 14).Capitalize = true;
            Console.WriteLine(bft);
        }
    }
}
