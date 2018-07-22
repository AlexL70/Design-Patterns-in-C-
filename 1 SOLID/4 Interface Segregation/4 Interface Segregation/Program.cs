using System;

namespace _4_Interface_Segregation
{
    class Program
    {
        public class Document
        {
            public string Header { get; set; }
            public string Body { get; set; }
        }

        public interface IMachine : IPrinter, IScanner, IFax {}

        public interface IPrinter
        {
            void Print(Document d);
        }

        public interface IScanner
        {
            void Scan(Document d);
        }

        public interface IFax
        {
            void Fax(Document d);
        }

        public class MultifunctionPrinter : IMachine
        {
            private IPrinter _printer;
            private IScanner _scanner;

            public MultifunctionPrinter(IPrinter printer, IScanner scanner)
            {
                _printer = printer;
                _scanner = scanner;
            }

            public void Print(Document d)
            {
                _printer.Print(d);   //  decorator pattern
            }

            public void Scan(Document d)
            {
                _scanner.Scan(d);   //  decorator pattern
            }

            public void Fax(Document d)
            {
                //  Fax document
            }
        }

        public class OldFashionedPrinter : IPrinter
        {
            public void Print(Document d)
            {
                //  Print document
            }
        }

        public class Photocopier : IPrinter, IScanner
        {
            public void Print(Document d)
            {
                //  Print document
            }

            public void Scan(Document d)
            {
                //  Scan document
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
