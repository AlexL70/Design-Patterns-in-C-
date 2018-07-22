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

        public interface IMachine
        {
            void Print(Document d);
            void Scan(Document d);
            void Fax(Document d);
        }

        public class MultifunctionPrinter : IMachine
        {
            public void Print(Document d)
            {
                //  Print document
            }

            public void Scan(Document d)
            {
                //  Scan document
            }

            public void Fax(Document d)
            {
                //  Fax document
            }
        }

        public class OldFashionedPrinter : IMachine
        {
            public void Print(Document d)
            {
                //  Print document
            }

            public void Scan(Document d)
            {
                //  Nothing to do here
                throw new NotImplementedException();
            }

            public void Fax(Document d)
            {
                //  Nothing to do here
                throw new NotImplementedException();
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
