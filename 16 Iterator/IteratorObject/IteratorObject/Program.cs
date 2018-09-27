using System;
using static System.Console;

namespace IteratorObject
{
    class Program
    {
        public class Node<T>
        {
            public T Value { get; set; }
            public Node<T> Left { get; set; }
            public Node<T> Right { get; set; }
            public Node<T> Parent { get; set; }

            public Node(T value)
            {
                Value = value;
            }

            public Node(T value, Node<T> left, Node<T> right) : this(value)
            {
                Left = left;
                Right = right;
                Value = value;
                left.Parent = right.Parent = this;
            }

            public override string ToString()
            {
                return Value.ToString();
            }
        }

        public class InOrderTreeIterator<T>
        {
            private readonly Node<T> _root;
            public Node<T> Current { get; private set; }
            private bool _yieldedStart;

            public InOrderTreeIterator(Node<T> root)
            {
                _root = root;
                Reset();
            }

            public bool MoveNext()
            {
                if (!_yieldedStart)
                {
                    _yieldedStart = true;
                    return true;
                }

                if (Current.Right != null)
                {
                    Current = Current.Right;
                    while (Current.Left != null)
                        Current = Current.Left;
                    return true;
                }
                else
                {
                    var p = Current.Parent;
                    while (p != null && Current == p.Right)
                    {
                        Current = p;
                        p = p.Parent;
                    }

                    Current = p;
                    return Current != null;
                }
            }

            public void Reset()
            {
                Current = _root;
                _yieldedStart = false;
                while (Current.Left != null)
                {
                    Current = Current.Left;
                }
            }
        }

        static void Main(string[] args)
        {
            //      1
            //     / \
            //    2   3
            var root = new Node<int>(1,
                new Node<int>(2, new Node<int>(4), new Node<int>(5)), 
                new Node<int>(3, new Node<int>(6), new Node<int>(7)));
            var it = new InOrderTreeIterator<int>(root);
            it.Reset();
            var first = it.Current;
            while (it.MoveNext())
            {
                var c = it.Current;
                if (c != first)
                {
                    Write(",");
                }
                Write(c);
            }
            WriteLine();
            it.Reset();
            first = it.Current;
            while (it.MoveNext())
            {
                var c = it.Current;
                if (c != first)
                {
                    Write(",");
                }
                Write(c);
            }
            WriteLine();
        }
    }
}
