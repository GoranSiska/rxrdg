using System;

namespace RxrdgDemoApp
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Enter regular expression to generate random sequence or leave empty to quit:");
                var param = Console.ReadLine();

                if(string.IsNullOrEmpty(param)) { return; }

                try
                {
                    var rxrdg = new RegularExpressionDataGenerator.RegExpDataGenerator(param);

                    //show tree
                    Console.WriteLine();
                    Console.WriteLine(RegularExpressionDataGenerator.XmlVisitor.Visit(new RegularExpressionDataGenerator.Parser().Parse(param)));
                    Console.WriteLine("-------");
                    Console.WriteLine();

                    //output 10 examples
                    for (var i = 0; i < 10; i++)
                    {
                        Console.WriteLine(rxrdg.Next());
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Regular expression sequence not recognized.");
                    Console.WriteLine();
                }
            }
        }
    }
}
