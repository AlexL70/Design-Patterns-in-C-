using System;
using System.Collections.Generic;
using static System.Console;

namespace ObservablePropertiesAndSequences
{
    public class Market
    {
        private List<float> _prices = new List<float>();

        public void AddPrice(float price)
        {
            _prices.Add(price);
            PriceAdded?.Invoke(this, price);
        }

        public event EventHandler<float> PriceAdded;
    }

    class Program
    {
        static void Main(string[] args)
        {
            var market = new Market();
            market.PriceAdded += (sender, price) => { WriteLine($"Price added {price}"); };
            market.AddPrice(123);
        }
    }
}
