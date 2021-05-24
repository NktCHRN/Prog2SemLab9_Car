using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StringExtLib;

namespace Lab9StringDelConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Lab №9";
            ProgramInfo();
            Console.WriteLine();
            Console.WriteLine("Enter your string");
            string str = Console.ReadLine();
            StringExt strExt = new StringExt(str);
            FindSymbolStatic delToFindLastStatic = StringExt.FindLastSymbol;
            FindSymbolExemplar delToFindLastExemplar = strExt.FindLastSymbol;
            bool run;
            string entered;
            do
            {
                run = false;
                char symbol;
                Console.WriteLine("\nEnter the symbol you want to find: ");
                do
                    entered = Console.ReadLine();
                while (entered.Length < 1);
                symbol = entered[0];
                int index = delToFindLastStatic.Invoke(str, symbol);
                Console.WriteLine("Delegate to static method: ");
                if (index != -1)
                    Console.WriteLine($"Index of your symbol (starting from 0): {index}");
                else
                    Console.WriteLine("Your symbol was not found");
                index = delToFindLastExemplar.Invoke(symbol);
                Console.WriteLine("Delegate to exemplar method: ");
                if (index != -1)
                    Console.WriteLine($"Index of your symbol (starting from 0): {index}");
                else
                    Console.WriteLine("Your symbol was not found");
                Console.WriteLine("\nDo you want to enter one more symbol? [Y/n]");
                do
                    entered = Console.ReadLine();
                while (entered.Length < 1);
                symbol = entered[0];
                while (char.ToLower(symbol) != 'n' && char.ToLower(symbol) != 'y')
                {
                    Console.WriteLine("You entered the wrong letter");
                    Console.WriteLine("Do you want to enter one more symbol? [Y/n]");
                    do
                        entered = Console.ReadLine();
                    while (entered.Length < 1);
                    symbol = entered[0];
                }
                if (char.ToLower(symbol) == 'y')
                    run = true;
            } while (run);
        }
        static void ProgramInfo()                                       // prints information about the program
        {
            Console.WriteLine("Lab №9. Nikita Chernikov, IS-02");
            Console.WriteLine("Researching of pointers to functions C++ and delegates C#");
            Console.WriteLine("Variant 15");
        }
    }
}
