using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS10_operator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // << == *2 / >> == /2
            // 비트연산 - 한칸 왼쪽으로 밀면 2배 오른쪽으로 밀면 1/2배
            int firstval = 15; // 15 = 0x1111
            int secondval = firstval << 1; // 2진수 0001 1110
            Console.WriteLine(secondval);

            // 1111 & 1101 = > 1101 15 & 13 = > 13
            // 1010 | 0101 = > 1111

            firstval = 15;
            secondval = 13;
            Console.WriteLine(firstval & secondval);
            firstval = 10;
            secondval = 5;
            Console.WriteLine(firstval | secondval);
            Console.WriteLine(firstval ^ secondval); // XOR
            Console.WriteLine(~secondval); // 보수
            // 실무에서는 많이 안씀

            // Null 병합 연산자
            int? checkval = null;
            Console.WriteLine(checkval == null? 0 : checkval); // 3항 연산자
            Console.WriteLine(checkval ?? 0); // null 병합연산자는 3항 연산자를 더 축소

            checkval = 25; // 비교 할 수 있는 방법은 여러 개
            Console.WriteLine(checkval.HasValue ? checkval.Value : 0); // HasValue, Value
            Console.WriteLine(checkval ?? 0);
        }
    }
}
