using System;
using System.Collections.Generic;
using static System.Console;

namespace PropertyProxy
{
    public class Property<T> : IEquatable<Property<T>> where T : new()
    {
        private T _value;

        public T Value
        {
            get => _value;
            set {
                if (!Equals(_value, value))
                {
                    WriteLine($"Assigning {value} to {nameof(Value)}");
                    _value = value;
                }
            }
        }

        public Property() => _value = new T();

        public Property(T value) => _value = value;

        public static implicit operator T(Property<T> prop)
        {
            return prop.Value;
        }

        public static implicit operator Property<T>(T value)
        {
            return new Property<T>(value);
        }

        public static bool operator ==(Property<T> property1, Property<T> property2)
        {
            return EqualityComparer<Property<T>>.Default.Equals(property1, property2);
        }

        public static bool operator !=(Property<T> property1, Property<T> property2)
        {
            return !(property1 == property2);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Property<T>);
        }

        public bool Equals(Property<T> other)
        {
            return other != null &&
                   EqualityComparer<T>.Default.Equals(_value, other._value);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_value);
        }
    }

    public class Creature
    {
        // if you assigned straight to the Property<int> like c._agility = 10, you would have
        // c.Agility = new Property<int>(10) - that is how it really works,
        // because of implicit conversion defined in Property<T> class
        private readonly Property<int> _agility = new Property<int>();
        // Therefore you need separate property which redirects assignment to _agility.Value
        // this way assignment really works as assignment;
        public int Agility { get => _agility.Value; set => _agility.Value = value; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var c = new Creature();
            WriteLine($"Agility of new creature is {c.Agility}");
            c.Agility = 10;
            c.Agility = 10;
            c.Agility = 5;
            c.Agility = 7;
            c.Agility = 7;
        }
    }
}
