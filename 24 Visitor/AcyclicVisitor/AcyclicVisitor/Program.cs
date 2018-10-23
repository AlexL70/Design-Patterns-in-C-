using System;
using System.Text;
using static System.Console;

namespace IntrusiveExpressionPrinting
{
    public interface IVisitor<TVisitable>
    {
        void Visit(TVisitable obj);
    }

    public interface IVisitor { }

    public abstract class Expression
    {
        public virtual void Accept(IVisitor visitor)
        {
            if (visitor is IVisitor<Expression> typed)
                typed.Visit(this);
        }
    }

    public class DoubleExpression : Expression
    {
        public double Value { get; }

        public DoubleExpression(double value)
        {
            Value = value;
        }

        public override void Accept(IVisitor visitor)
        {
            if(visitor is IVisitor<DoubleExpression> typed)
                typed.Visit(this);
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

        public override void Accept(IVisitor visitor)
        {
            if (visitor is IVisitor<AdditionExpression> typed)
            {
                typed.Visit(this);
            }
        }
    }

    public class ExpressionPrinter : IVisitor,
        IVisitor<Expression>,
        IVisitor<DoubleExpression>,
        IVisitor<AdditionExpression>
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

        public void Visit(Expression obj) {}

        public override string ToString()
        {
            return _sb.ToString();
        }
    }

    public class ExpressionCalculator : IVisitor,
        IVisitor<Expression>,
        IVisitor<DoubleExpression>,
        IVisitor<AdditionExpression>
    {
        public double Result { get; private set; }

        public void Visit(DoubleExpression de)
        {
            Result = de.Value;
        }

        public void Visit(AdditionExpression ae)
        {
            ae.Left.Accept(this);
            var left = Result;
            ae.Right.Accept(this);
            Result += left;
        }

        public void Visit(Expression obj) {}
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
            WriteLine($"{ev} = {calc.Result}");
        }
    }
}
