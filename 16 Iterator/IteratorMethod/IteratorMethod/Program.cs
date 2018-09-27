using System.Collections.Generic;
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

        public class BinaryTree<T>
        {
            private Node<T> _root;

            public BinaryTree(Node<T> root)
            {
                _root = root;
            }

            public IEnumerable<Node<T>> InOrder
            {
                get
                {
                    IEnumerable<Node<T>> Traverse(Node<T> current)
                    {
                        if (current.Left != null)
                        {
                            foreach (var left in Traverse(current.Left))
                            {
                                yield return left;
                            }
                        }

                        yield return current;

                        if (current.Right != null)
                        {
                            foreach (var right in Traverse(current.Right))
                            {
                                yield return right;
                            }
                        }
                    }

                    foreach (var node in Traverse(_root))
                    {
                        yield return node;
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            //      1
            //     / \
            //    2   3
            //   / \ / \
            //  4  5 6  7
            var root = new Node<int>(1,
                new Node<int>(2, new Node<int>(4), new Node<int>(5)),
                new Node<int>(3, new Node<int>(6), new Node<int>(7)));
            var it = new BinaryTree<int>(root);
            WriteLine(string.Join(",", it.InOrder));
        }
    }
}
