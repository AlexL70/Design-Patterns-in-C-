using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;

namespace IntrusiveExpressionPrinting
{
    using DictType = Dictionary<Type, Action<Expression, StringBuilder>>;

    public abstract class Expression
    {
        //public abstract void Print(StringBuilder sb);
    }

    public class DoubleExpression : Expression
    {
        public double Value { get; }

        public DoubleExpression(double value)
        {
            Value = value;
        }
    }

    public class AdditionExpression : Expression
    {
        public Expression Left { get; }
        public Expression Right { get; }

        public AdditionExpression(Expression left, Expression right)
        {
            Left = left ?? throw new ArgumentNullException(nameof(left));
            Right = right ?? throw new ArgumentNullException(nameof(right));
        }
    }

    public static class ExpressionPrinter
    {
        private static DictType _actions = new DictType()
        {
            [typeof(DoubleExpression)] = (e, sb) => sb.Append(((DoubleExpression)e).Value),
            [typeof(AdditionExpression)] = (e, sb) =>
            {
                var ae = (AdditionExpression)e;
                sb.Append("(");
                Print(ae.Left, sb);
                sb.Append("+");
                Print(ae.Right, sb);
                sb.Append(")");
            }
        };
        public static void Print(this Expression e, StringBuilder sb)
        {
            _actions[e.GetType()](e, sb);
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
