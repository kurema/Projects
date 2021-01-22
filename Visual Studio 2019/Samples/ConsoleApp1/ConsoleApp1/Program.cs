using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine((int)(-1).ToString(new System.Globalization.CultureInfo("ar"))[0]);
            Console.WriteLine((-1).ToString(new System.Globalization.CultureInfo("ar")));
            Console.WriteLine((-1.2).ToString(new System.Globalization.CultureInfo("fr")));
        }
    }
}
