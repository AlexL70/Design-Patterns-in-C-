using System;
using System.Text;
using static System.Console;

namespace IntrusiveExpressionPrinting
{
    public interface IExpressionVisitor
    {
        void Visit(DoubleExpression de);
        void Visit(AdditionExpression ae);
    }

    public abstract class Expression
    {
        public abstract void Accept(IExpressionVisitor visitor);
    }

    public class DoubleExpression : Expression
    {
        public double Value { get; }

        public DoubleExpression(double value)
        {
            Value = value;
        }

        public override void Accept(IExpressionVisitor visitor) => visitor.Visit(this);
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

        public override void Accept(IExpressionVisitor visitor) => visitor.Visit(this);
    }

    public class ExpressionPrinter : IExpressionVisitor
    {
        private StringBuilder _sb = new StringBuilder();

        public void Visit(DoubleExpression de)
        {
            _sb.Append(de.Value);
        }

        public void Visit(AdditionExpression ae)
        {
            _sb.Append("(");
            ae.Left.Accept(this);
            _sb.Append("+");
            ae.Right.Accept(this);
            _sb.Append(")");
        }

        public override string ToString()
        {
            return _sb.ToString();
        }
    }

    public class ExpressionCalculator : IExpressionVisitor
    {
        public double Result => _result;
        private double _result;

        public void Visit(DoubleExpression de)
        {
            _result = de.Value;
        }

        public void Visit(AdditionExpression ae)
        {
            ae.Left.Accept(this);
            var left = _result;
            ae.Right.Accept(this);
            _result += left;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var exp = new AdditionExpression(new DoubleExpression(1),
                new AdditionExpression(new DoubleExpression(2), new DoubleExpression(3)));
            var ev = new ExpressionPrinter();
            exp.Accept(ev);
            var calc = new ExpressionCalculator();
            exp.Accept(calc);
            WriteLine($"{ev.ToString()} = {calc.Result}");
        }
    }
}
