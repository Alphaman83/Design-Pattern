using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace DesignPatterns
{
    public class Calculator<T>
    {
        public static bool AreEqual(T val1,T val2)
        {
            return val1.Equals(val2);
        }
    }
    public class GenericsTesting
    {
        static void Main(string[] args)
        {
            bool equal = Calculator<int>.AreEqual(7,7);

            bool strequal = Calculator<string>.AreEqual("Jannah","Dhuniya");

            WriteLine($"Numbers are equal:{equal}\n");
            WriteLine($"Given Strings are equal:{strequal}" );
            ReadKey();
            
        }
    }
}
