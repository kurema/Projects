using System;

using System.Numerics;

namespace Calc.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                var input = System.Console.ReadLine();
                if (input == "exit") return;

                BigInteger a = new BigInteger(145);
                System.Console.WriteLine(a * a);
            }

        }
    }
}
