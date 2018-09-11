using System;
using System.Collections.Generic;
using System.Linq;

namespace AbstractFactory
{
    public interface IHotDrink
    {
        void Consume();
    }

    internal class Tea : IHotDrink
    {
        public void Consume()
        {
            Console.WriteLine("This tea is nice, but I'd prefer it with milk.");
        }
    }

    internal class Coffee : IHotDrink
    {
        public void Consume()
        {
            Console.WriteLine("This coffee is sensational!");
        }
    }

    public interface IHotDrinkFactory
    {
        IHotDrink Prepare(int amount);
    }

    internal class TeaFactory : IHotDrinkFactory
    {
        public IHotDrink Prepare(int amount)
        {
            Console.WriteLine($"Put in a tea bag, boil water, pour {amount} ml, add lemon, enjoy!");
            return new Tea();
        }
    }

    internal class CoffeeFactory : IHotDrinkFactory
    {
        public IHotDrink Prepare(int amount)
        {
            Console.WriteLine($"Grind some beans, boil water, pour {amount} ml, add cream and sugar, enjoy!");
            return new Coffee();
        }
    }

    public class HotDrinkMachine
    {
        private readonly Dictionary<string, IHotDrinkFactory> factories = new Dictionary<string, IHotDrinkFactory>();

        public HotDrinkMachine()
        {
            foreach (var t in typeof(HotDrinkMachine).Assembly.GetTypes()
                .Where(t => typeof(IHotDrinkFactory).IsAssignableFrom(t) && t.IsClass))
            {
                factories.Add(t.Name.Replace("Factory", string.Empty), (IHotDrinkFactory)Activator.CreateInstance(t));   
            }
        }

        public IHotDrink MakeDrink(string drink, int amount)
        {
            return factories[drink].Prepare(amount);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var machine = new HotDrinkMachine();
            var tea = machine.MakeDrink(nameof(Tea), 150);
            tea.Consume();
            var coffee = machine.MakeDrink(nameof(Coffee), 70);
            coffee.Consume();
        }
    }
}
