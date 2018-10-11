using System;
using System.ComponentModel;
using static System.Console;

namespace ObservablePropertiesAndSequences
{
    public class Market
    {
        public BindingList<float> Prices { get; set; } = new BindingList<float>();

        public void AddPrice(float price)
        {
            Prices.Add(price);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var market = new Market();
            market.Prices.ListChanged += (sender, eventArgs) =>
            {
                switch (eventArgs.ListChangedType)
                {
                    case ListChangedType.ItemAdded:
                        WriteLine($"Price added {((BindingList<float>) sender)[eventArgs.NewIndex]}");
                        break;
                    case ListChangedType.ItemChanged:
                        break;
                    case ListChangedType.ItemDeleted:
                        break;
                    case ListChangedType.ItemMoved:
                        break;
                    case ListChangedType.PropertyDescriptorAdded:
                        break;
                    case ListChangedType.PropertyDescriptorChanged:
                        break;
                    case ListChangedType.PropertyDescriptorDeleted:
                        break;
                    case ListChangedType.Reset:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            };
            market.AddPrice(123);
        }
    }
}
