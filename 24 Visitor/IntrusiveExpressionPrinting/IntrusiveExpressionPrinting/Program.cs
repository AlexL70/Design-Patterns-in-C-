using System;
using System.Text;
using static System.Console;

namespace IntrusiveExpressionPrinting
{
    public abstract class Expression
    {
        public abstract void Print(StringBuilder sb);
    }

    public class DoubleExpression : Expression
    {
        private readonly double _value;

        public DoubleExpression(double value)
        {
            _value = value;
        }

        public override void Print(StringBuilder sb)
        {
            sb.Append(_value);
        }
    }

    public class AdditionExpression : Expression
    {
        private Expression _left, _right;

        public AdditionExpression(Expression left, Expression right)
        {
            _left = left ?? throw new ArgumentNullException(nameof(left));
            _right = right ?? throw new ArgumentNullException(nameof(right));
        }

        public override void Print(StringBuilder sb)
        {
            sb.Append("(");
            _left.Print(sb);
            sb.Append("+");
            _right.Print(sb);
            sb.Append(")");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var exp = new AdditionExpression(new DoubleExpression(1),
                new AdditionExpression(new DoubleExpression(2), new DoubleExpression(3)));
            var sb = new StringBuilder();
            exp.Print(sb);
            WriteLine(sb.ToString());
        }
    }
}
