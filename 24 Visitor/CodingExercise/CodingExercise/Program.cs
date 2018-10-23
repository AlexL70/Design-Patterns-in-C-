using System;
using System.Text;

namespace Coding.Exercise
{
    public abstract class ExpressionVisitor
    {
        public abstract void Visit(Value v);
        public abstract void Visit(AdditionExpression ae);
        public abstract void Visit(MultiplicationExpression me);
    }

    public abstract class Expression
    {
        public abstract void Accept(ExpressionVisitor ev);
    }

    public class Value : Expression
    {
        public readonly int TheValue;

        public Value(int value)
        {
            TheValue = value;
        }

        public override void Accept(ExpressionVisitor ev) => ev.Visit(this);
    }

    public class AdditionExpression : Expression
    {
        public readonly Expression LHS, RHS;

        public AdditionExpression(Expression lhs, Expression rhs)
        {
            LHS = lhs;
            RHS = rhs;
        }

        public override void Accept(ExpressionVisitor ev) => ev.Visit(this);
    }

    public class MultiplicationExpression : Expression
    {
        public readonly Expression LHS, RHS;

        public MultiplicationExpression(Expression lhs, Expression rhs)
        {
            LHS = lhs;
            RHS = rhs;
        }

        public override void Accept(ExpressionVisitor ev) => ev.Visit(this);
    }

    public class ExpressionPrinter : ExpressionVisitor
    {
        private StringBuilder _sb = new StringBuilder();

        public override void Visit(Value value)
        {
            _sb.Append(value.TheValue);
        }

        public override void Visit(AdditionExpression ae)
        {
            _sb.Append("(");
            ae.LHS.Accept(this);
            _sb.Append("+");
            ae.RHS.Accept(this);
            _sb.Append(")");
        }

        public override void Visit(MultiplicationExpression me)
        {
            me.LHS.Accept(this);
            _sb.Append("*");
            me.RHS.Accept(this);
        }

        public override string ToString()
        {
            return _sb.ToString();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var exp = new MultiplicationExpression(new Value(1), 
                new AdditionExpression(new Value(2), new Value(3)));
            var ev = new ExpressionPrinter();
            exp.Accept(ev);
            Console.WriteLine($"{ev.ToString()}");
        }
    }
}