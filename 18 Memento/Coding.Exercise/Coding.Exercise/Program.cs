using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using static System.Console;

namespace Coding.Exercise
{
    public class Token
    {
        public int Value = 0;

        public Token(int value)
        {
            this.Value = value;
        }
    }

    public class Memento
    {
        public List<Token> Tokens = new List<Token>();
    }

    public class TokenMachine
    {
        public List<Token> Tokens = new List<Token>();

        public Memento AddToken(int value)
        {
            var t = new Token(value);
            return AddToken(t);
        }

        public Memento AddToken(Token token)
        {
            Tokens.Add(token);
            var m = new Memento();
            m.Tokens = Tokens.Select(t => new Token(t.Value)).ToList();
            return m;
        }

        public void Revert(Memento m)
        {
            Tokens = m.Tokens.Select(t => new Token(t.Value)).ToList();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("Hello World!");
        }
    }
}
