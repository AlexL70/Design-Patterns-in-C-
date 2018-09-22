using System;
using static System.Console;

namespace ProtectionProxy
{
    public interface ICar
    {
        void Drive();
    }

    public class Car : ICar {
        public void Drive() => WriteLine("The car is being driven");
    }

    public class Driver
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    //  Protection proxy class demo
    public class CarProxy : ICar
    {
        private Driver _driver;
        private Car _car;

        public CarProxy(Driver driver, Car car)
        {
            _driver = driver ?? throw new ArgumentNullException(nameof(driver));
            _car = car ?? throw new ArgumentNullException(nameof(car));
        }

        public void Drive()
        {
            if (_driver.Age >= 16)
            {
                _car.Drive();
            }
            else
            {
                WriteLine("You are too young to drive!");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var driver = new Driver {Name = "Joe", Age = 12};
            ICar car = new CarProxy( driver, new Car());
            car.Drive();
            driver.Age = 18;
            car.Drive();
        }
    }
}
