using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day01_Variable
{
    internal class Program
    {
        static void Main(string[] args)
        {
            byte bdata = byte.MaxValue;
            Console.WriteLine(bdata);

            bdata = byte.MinValue;
            Console.WriteLine(bdata);

            long ldata = long.MaxValue;
            Console.WriteLine(ldata);

            ldata = long.MinValue;
            Console.WriteLine(ldata);

            ldata--;
            Console.WriteLine(ldata);

            int binval = 0b11111111; // 2진수
            Console.WriteLine(binval);

            int hexval = 0xff;
            Console.WriteLine(hexval); // 16진수

            float fdata = float.MaxValue;
            Console.WriteLine(fdata);

            fdata = float.MinValue;
            Console.WriteLine(fdata);  

            double ddata = double.MaxValue;
            Console.WriteLine(ddata);

            ddata = double.MinValue;     
            Console.WriteLine(ddata);
        }
    }
}
