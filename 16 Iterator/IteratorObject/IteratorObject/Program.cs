using System;
//using System.Collections;
//using System.Collections.Generic;
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

        public class BinaryTree<T> //: IEnumerable<Node<T>>
        {
            private readonly Lazy<InOrderTreeIterator<T>> _enumerator;

            public BinaryTree(Node<T> root)
            {
                _enumerator = new Lazy<InOrderTreeIterator<T>>(() => new InOrderTreeIterator<T>(root));
            }

            public InOrderTreeIterator<T> GetEnumerator()
            {
                return _enumerator.Value;
            }

            //IEnumerator<Node<T>> IEnumerable<Node<T>>.GetEnumerator()
            //{
            //    return _enumerator.Value;
            //}

            //IEnumerator IEnumerable.GetEnumerator()
            //{
            //    return _enumerator.Value;
            //}
        }

        public class InOrderTreeIterator<T> //: IEnumerator<Node<T>>
        {
            private Node<T> _root;
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

            //object IEnumerator.Current => Current;

            //public void Dispose()
            //{
            //    _root = null;
            //}
        }

        static void Main(string[] args)
        {
            //      1
            //     / \
            //    2   3
            var root = new Node<int>(1,
                new Node<int>(2, new Node<int>(4), new Node<int>(5)), 
                new Node<int>(3, new Node<int>(6), new Node<int>(7)));
            var bt = new BinaryTree<int>(root);
            //WriteLine(string.Join(",", bt));
            //  In order to be used inside foreach object does not need to implement IEnumerable<T>.
            //  It only needs to implement GetEnumerator method, returning object having Enumerator methods and property,
            //  i.e. bool MoveNext(), void Reset() and T Current { get; }.
            //  "foreach" does not really require implementing interfaces explicitly.
            foreach (var node in bt)
            {
                Write($"{node},");
            }
            WriteLine();
        }
    }
}
