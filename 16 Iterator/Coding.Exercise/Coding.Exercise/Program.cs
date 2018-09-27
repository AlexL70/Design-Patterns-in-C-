using System;
using System.Collections.Generic;
using static System.Console;

namespace Coding.Exercise
{
    public class Node<T>
    {
        public T Value;
        public Node<T> Left, Right;
        public Node<T> Parent;

        public Node(T value)
        {
            Value = value;
        }

        public Node(T value, Node<T> left, Node<T> right)
        {
            Value = value;
            Left = left;
            Right = right;

            left.Parent = right.Parent = this;
        }

        public IEnumerable<T> PreOrder
        {
            get
            {
                IEnumerable<T> Traverse(Node<T> node)
                {
                    yield return node.Value;

                    if (node.Left != null)
                        foreach (var n in Traverse(node.Left))
                            yield return n;
                    if (node.Right != null)
                        foreach (var n in Traverse(node.Right))
                            yield return n;
                }

                return Traverse(this);
            }
        }
    }

    class Program
    {
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
            WriteLine(string.Join(",", root.PreOrder));
        }
    }
}
