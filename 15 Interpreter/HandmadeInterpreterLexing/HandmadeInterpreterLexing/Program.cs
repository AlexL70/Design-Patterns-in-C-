using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static System.Console;

namespace HandmadeInterpreterLexing
{
    public interface IElement
    {
        int Value { get; }
    }

    public class Integer : IElement
    {
        public int Value { get; }

        public Integer(int value)
        {
            Value = value;
        }
    }

    public class BinaryOperation : IElement
    {
        public enum Type
        {
            Addition, Subtraction
        }

        public Type OperationType { get; set; }
        public IElement Left { get; set; }
        public IElement Right { get; set; }

        public int Value
        {
            get
            {
                switch (OperationType)
                {
                    case Type.Addition:
                        return Left.Value + Right.Value;
                    case Type.Subtraction:
                        return Left.Value - Right.Value;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }

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
                    result.Add(LexNum(input, ref i));
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

        private static Token LexNum(string input, ref int i)
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

        public static IElement Parse(IReadOnlyList<Token> tokens)
        {
            var result = new BinaryOperation();
            bool hasLeftHandSide = false;
            for (int i = 0; i < tokens.Count; i++)
            {
                var token = tokens[i];
                switch (token.TokenType)
                {
                    case Token.Type.Integer:
                        var intNum = int.Parse(token.Text);
                        if (!hasLeftHandSide)
                        {
                            result.Left = new Integer(intNum);
                            hasLeftHandSide = true;
                        }
                        else
                        {
                            result.Right = new Integer(intNum);
                        }
                        break;
                    case Token.Type.Plus:
                        result.OperationType = BinaryOperation.Type.Addition;
                        break;
                    case Token.Type.Minus:
                        result.OperationType = BinaryOperation.Type.Subtraction;
                        break;
                    case Token.Type.Lparen:
                        int j = i;
                        for (; j < tokens.Count; j++)
                            if (tokens[j].TokenType == Token.Type.Rparen)
                                break;
                        var element = Parse(tokens.Skip(i + 1).Take(j - i - 1).ToList());
                        if (!hasLeftHandSide)
                        {
                            result.Left = element;
                            hasLeftHandSide = true;
                        }
                        else
                        {
                            result.Right = element;
                        }
                        i = j; //  Continue after right parenthesis
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(token.ToString());
                }
            }
            return result;
        }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            var input = "(13+ 4) + (12 + 1)";
            var tokens = Parser.Lex(input);
            WriteLine(string.Join("  ", tokens));
            var parsed = Parser.Parse(tokens);
            WriteLine($"{input} = {parsed.Value}");
        }
    }
}
