using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using static System.Console;

namespace Coding.Exercise
{
    public class Sentence
    {
        private List<WordToken> _tokens = new List<WordToken>();
        private string _plainText;

        public Sentence(string plainText)
        {
            _plainText = plainText;
        }

        public WordToken this[int index]
        {
            get
            {
                var result = _tokens.SingleOrDefault(t => t.Index == index);
                if (result == null)
                {
                    result = new WordToken() { Index = index};
                    _tokens.Add(result);
                }

                return result;
            }
        }

        public override string ToString()
        {
            return string.Join(' ', _plainText.Split(' ').Select((value, i) => new {i, value})
                .Select(x => _tokens.Any(t => t.Index == x.i && t.Capitalize) ? x.value.ToUpper() : x.value));
        }

        public class WordToken
        {
            public bool Capitalize;
            public int Index;
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            var sentence = new Sentence("Every body loves some body.");
            sentence[3].Capitalize = true;
            WriteLine(sentence);
            sentence[1].Capitalize = true;
            WriteLine(sentence);
            sentence[4].Capitalize = true;
            sentence[3].Capitalize = false;
            WriteLine(sentence);
        }
    }
}
