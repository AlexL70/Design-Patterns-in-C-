using System;
using System.Numerics;
using static System.Console;

namespace Coding.Exercise
{
    public interface IDiscriminantStrategy
    {
        double CalculateDiscriminant(double a, double b, double c);
    }

    public class OrdinaryDiscriminantStrategy : IDiscriminantStrategy
    {
        public double CalculateDiscriminant(double a, double b, double c)
        {
            return (b * b) - (4 * a * c);
        }
    }

    public class RealDiscriminantStrategy : IDiscriminantStrategy
    {

        public double CalculateDiscriminant(double a, double b, double c)
        {
            var d = (b * b) - (4 * a * c);
            return d < 0 ? Double.NaN : d;
        }
    }

    public class QuadraticEquationSolver
    {
        private readonly IDiscriminantStrategy strategy;

        public QuadraticEquationSolver(IDiscriminantStrategy strategy)
        {
            this.strategy = strategy;
        }

        public Tuple<Complex, Complex> Solve(double a, double b, double c)
        {
            var d = strategy.CalculateDiscriminant(a, b, c);
            var first = (-b + Complex.Sqrt(d))/(2 * a);
            var second = (-b - Complex.Sqrt(d)) / (2 * a);
            return Tuple.Create(first, second);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var qeq = new QuadraticEquationSolver(new OrdinaryDiscriminantStrategy());
            WriteLine(qeq.Solve(1, 3, 2));
        }
    }
}
