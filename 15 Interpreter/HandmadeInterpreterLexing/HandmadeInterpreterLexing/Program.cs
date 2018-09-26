using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;

namespace HandmadeInterpreterLexing
{
    public class ExceptionIncorrectSyntax : Exception
    {
        public ExceptionIncorrectSyntax() { }

        public ExceptionIncorrectSyntax(char symbol, int position)
            : base($"Incorrect symbol `{symbol}` in position {position}") { }

        public ExceptionIncorrectSyntax(string msg) : base(msg) {}
    }

    public class Token
    {
        public enum Type
        {
            Integer, Plus, Minus, Lparen, Rparen
        }

        public Type TokenType { get; set; }
        public string Text { get; set; }

        public Token(Type tokenType, string text)
        {
            TokenType = tokenType;
            Text = text ?? throw new ArgumentNullException(nameof(text));
        }

        public override string ToString()
        {
            return $"`{Text}`";
        }
    }

    public static class Parser
    {
        public static List<Token> Lex(string input)
        {
            var result = new List<Token>();
            for (int i = 0; i < input.Length; i++)
            {
                if (char.IsWhiteSpace(input[i]))
                {
                    continue;
                }
                if (char.IsDigit(input[i]))
                {
                    result.Add(ParseNum(input, ref i));
                }
                else
                    switch (input[i])
                    {
                        case '+':
                            result.Add(new Token(Token.Type.Plus, "+"));
                            break;
                        case '-':
                            result.Add(new Token(Token.Type.Minus, "-"));
                            break;
                        case '(':
                            result.Add(new Token(Token.Type.Lparen, "("));
                            break;
                        case ')':
                            result.Add(new Token(Token.Type.Rparen, ")"));
                            break;
                        default:
                            throw new ExceptionIncorrectSyntax(input[i], i);
                    }
            }

            return result;
        }

        private static Token ParseNum(string input, ref int i)
        {
            if (!char.IsDigit(input[i]))
                throw new ExceptionIncorrectSyntax($"Error in position {i}: `{input[i]}` is not a digit.");

            var sb = new StringBuilder();
            while (true)
            {
                sb.Append(input[i]);
                if (i + 1 >= input.Length || !char.IsDigit(input[i + 1]))
                    break;;
                    i++;
            }
            return new Token(Token.Type.Integer, sb.ToString());
        }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            var input = "(13+ 4  ) + (12 + 1)";
            var tokens = Parser.Lex(input);
            WriteLine(string.Join("  ", tokens));
        }
    }
}
