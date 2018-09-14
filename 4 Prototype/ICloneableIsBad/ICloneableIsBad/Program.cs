using System;
using System.IO;
//using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace ICloneableIsBad
{
    static class SerializationExtension
    {
        //public static T DeepCopy<T>(this T self)
        //{
        //    var stream = new MemoryStream();
        //    var formatter = new BinaryFormatter();
        //    formatter.Serialize(stream, self);
        //    stream.Seek(0, SeekOrigin.Begin);
        //    var copy = formatter.Deserialize(stream);
        //    stream.Close();
        //    return (T) copy;
        //}

        public static T DeepCopyXml<T>(this T self)
        {
            using (var stream = new MemoryStream())
            {
                var s = new XmlSerializer(typeof(T));
                s.Serialize(stream, self);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)s.Deserialize(stream);
            }
        }
    }

    //[Serializable]
    public class Person
    {
        public string[] Names { get; set; }
        public Address Address { get; set; }

        public Person(string[] names, Address address)
        {
            Names = names ?? throw new ArgumentNullException(nameof(names));
            Address = address ?? throw new ArgumentNullException(nameof(address));
        }

        protected Person() {}

        public override string ToString()
        {
            return $"{nameof(Names)}: {string.Join(" ", Names)}, {nameof(Address)}: {Address}";
        }
    }

    //[Serializable]
    public class Address
    {
        public string StreetName { get; set; }
        public int HouseNumber { get; set; }

        public Address(string streetName, int houseNumber)
        {
            StreetName = streetName ?? throw new ArgumentNullException(nameof(streetName));
            HouseNumber = houseNumber;
        }

        protected Address() {}

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
            var jane = john.DeepCopyXml();
            jane.Names[0] = "Jane";
            jane.Address.HouseNumber = 321;
            Console.WriteLine(john);
            Console.WriteLine(jane);
        }
    }
}
