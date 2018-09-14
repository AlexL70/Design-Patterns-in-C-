using System;

namespace ICloneableIsBad
{
    public class Person
    {
        public string[] Names { get; set; }
        public Address Address { get; set; }

        public Person(string[] names, Address address)
        {
            Names = names ?? throw new ArgumentNullException(nameof(names));
            Address = address ?? throw new ArgumentNullException(nameof(address));
        }

        public override string ToString()
        {
            return $"{nameof(Names)}: {string.Join(" ", Names)}, {nameof(Address)}: {Address}";
        }

        public Person(Person other)
        {
            Names = (string[]) other.Names.Clone();
            Address = new Address(other.Address);
        }
    }

    public class Address
    {
        public string StreetName { get; set; }
        public int HouseNumber { get; set; }

        public Address(string streetName, int houseNumber)
        {
            StreetName = streetName ?? throw new ArgumentNullException(nameof(streetName));
            HouseNumber = houseNumber;
        }

        public Address(Address address)
        {
            StreetName = address.StreetName;
            HouseNumber = address.HouseNumber;
        }

        public override string ToString()
        {
            return $"{nameof(StreetName)}: {StreetName}, {nameof(HouseNumber)}: {HouseNumber}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var john = new Person( new []{ "John", "Smith"}, new Address("London Road", 123));
            Person jane = new Person(john) {Names = {[0] = "Jane"}, Address = {HouseNumber = 321}};
            Console.WriteLine(john);
            Console.WriteLine(jane);
        }
    }
}
