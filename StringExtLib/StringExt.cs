using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringExtLib
{
    public delegate int FindSymbolStatic(string str, char symbol);      // delegate to static method FindSymbolStatic (for example, public static int FindLastSymbol(string str, char symbol) in StringExt)
    public delegate int FindSymbolExemplar(char symbol);                // delegate to exemplar method FindSymbolExemplar (for example, public int FindLastSymbol(char symbol) in StringExt)
    public class StringExt
    {
        public string ExtString { get; private set; }                                   // a string, initialized in the constructor
        public StringExt(string str)
        {
            if (str != null)
                ExtString = str;
            else
                throw new ArgumentNullException(nameof(str), "String cannot be null");
        }
        public static int FindLastSymbol(string str, char symbol)			            // finds the last entry of symbol, return index or -1 if not found
        {
            if (str != null)
            {
                int size = str.Length;
                const int notFound = -1;
                for (int i = size - 1; i >= 0; i--)
                    if (str[i] == symbol)
                        return i;
                return notFound;
            }
            else
            {
                throw new ArgumentNullException(nameof(str), "String cannot be null");
            }
        }
        public int FindLastSymbol(char symbol) => FindLastSymbol(ExtString, symbol);    // the same as previous, but exemplar
    }
}
