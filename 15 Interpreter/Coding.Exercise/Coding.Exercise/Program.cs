using System;
using System.Collections.Generic;
using System.Text;

namespace Coding.Exercise
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

    public class IntVar : IElement
    {
        private char _name;
        private ExpressionProcessor _ep;

        public IntVar(char name, ExpressionProcessor ep)
        {
            _name = name;
            _ep = ep ?? throw new ArgumentNullException(nameof(ep));
        }

        public int Value
        {
            get
            {
                try
                {
                    return _ep.Variables[_name];
                }
                catch (KeyNotFoundException e)
                {
                    return 0;
                };
            }
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
                if (Left == null || Right == null)
                    return 0;
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

        public ExceptionIncorrectSyntax(char symbol, int position, string msg = null)
            : base(string.IsNullOrWhiteSpace(msg) 
                ? $"Incorrect symbol `{symbol}` in position {position}"
                : $"Incorrect symbol `{symbol}` in position {position} {msg}") {}

        public ExceptionIncorrectSyntax(string msg) : base(msg) { }
    }

    public class ExceptionParseError : Exception
    {
        public ExceptionParseError(string s) : base(s) {}
    }

    public class Token
    {
        public enum Type
        {
            IntConst, IntVar, Plus, Minus
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
            return $"`{nameof(TokenType)}: {TokenType}, {nameof(Text)}: {Text}`";
        }
    }

    public class ExpressionProcessor
    {
        public Dictionary<char, int> Variables = new Dictionary<char, int>();

        private static List<Token> Lex(string input)
        {
            var result = new List<Token>();
            for (int i = 0; i < input.Length; i++)
            {
                var token = input[i];
                if (char.IsWhiteSpace(token))
                    continue;
                if (char.IsDigit(token))
                {
                    result.Add(LexNum(input, ref i));
                }
                else if (char.IsLetter(token))
                {
                    if (i + 1 < input.Length && char.IsLetterOrDigit(input[i + 1]))
                        throw new ExceptionIncorrectSyntax(token, i, "Names longer than 1 symbol are not allowed.");
                    result.Add(new Token(Token.Type.IntVar, token.ToString()));
                }
                else switch (token)
                {
                    case '+':
                        result.Add(new Token(Token.Type.Plus, "+"));
                        break;
                    case '-':
                        result.Add(new Token(Token.Type.Minus, "-"));
                        break;
                    default:
                        throw new ExceptionIncorrectSyntax(token, i, "Unexpected symbol");
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
                    break; ;
                i++;
            }
            return new Token(Token.Type.IntConst, sb.ToString());
        }

        private IElement Parse(IReadOnlyList<Token> tokens)
        {
            var result = new BinaryOperation();
            var opDefined = false;

            bool IsFinalOperand(int index, Token token, IElement element)
            {
                if (result.Left == null && !opDefined)
                {
                    result.Left = element;
                }
                else if (!opDefined)
                {
                    throw new ExceptionParseError($"Incorrect token: {token}");
                }
                else
                {
                    result.Right = element;
                }

                return index == tokens.Count - 1;
            }

            void ResetResult()
            {
                var r0 = new BinaryOperation() { Left = result, Right = null};
                result = r0;
                opDefined = false; 
            }

            for (int i = 0; i < tokens.Count; i++)
            {
                var token = tokens[i]; 
                switch (token.TokenType)
                {
                    case Token.Type.IntConst:
                        var c = new Integer(int.Parse(token.Text));
                        if (!IsFinalOperand(i, token, c) && opDefined)
                            ResetResult();
                        break;
                    case Token.Type.IntVar:
                        var v = new IntVar(token.Text[0], this);
                        if (!IsFinalOperand(i, token, v) && opDefined)
                            ResetResult();
                        break;
                    case Token.Type.Plus:
                        opDefined = true;
                        result.OperationType = BinaryOperation.Type.Addition;
                        break;
                    case Token.Type.Minus:
                        opDefined = true;
                        result.OperationType = BinaryOperation.Type.Subtraction;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }   
            }

            return opDefined ? result : result.Left;
        }

        public int Calculate(string expression)
        {
            try
            {
                var tokens = Lex(expression);
                return Parse(tokens).Value;
            }
            catch (ExceptionIncorrectSyntax e)
            {
                return 0;
            }
        }

        
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
