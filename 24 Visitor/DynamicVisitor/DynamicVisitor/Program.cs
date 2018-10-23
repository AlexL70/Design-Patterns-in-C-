using System;
using System.Text;
using static System.Console;

namespace IntrusiveExpressionPrinting
{
    public abstract class Expression
    {
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
        //  !!! Dangerous approach. Compiler type checking is omitted. !!!
        public static void Print(this AdditionExpression ae, StringBuilder sb)
        {
            sb.Append("(");
            Print((dynamic)ae.Left, sb);
            sb.Append("+");
            Print((dynamic)ae.Right, sb);
            sb.Append(")");
        }

        public static void Print(this DoubleExpression de, StringBuilder sb)
        {
            sb.Append(de.Value);
        }
    }

    public static class ExpressionCalculator
    {
        public static double Calculate(this AdditionExpression ae)
        {
            return Calculate((dynamic) ae.Left) + Calculate((dynamic) ae.Right);
        }

        public static double Calculate(this DoubleExpression de) => de.Value;
    }

    class Program
    {
        static void Main(string[] args)
        {
            var exp = new AdditionExpression(new DoubleExpression(1),
                new AdditionExpression(new DoubleExpression(2), new DoubleExpression(3)));
            var sb = new StringBuilder();
            exp.Print(sb);
            WriteLine($"{sb} = {exp.Calculate()}");
        }
    }
}
