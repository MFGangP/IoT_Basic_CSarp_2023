using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS11_logiccondition
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int a = 20;

            if (a == 20) // 처리 해야되는 로직이 있으면 무조건 {} 써라
            {
                // if 문 밑에 문장이 하나 밖에 없으면 중괄호 없이 이렇게 써도 되긴하는데
                Console.WriteLine("20입니다."); // 초보는 이렇게 쓰지마라
            }
            else
            {
                Console.WriteLine("20이 아닙니다.");
            }
            if (a == 30) return; // 메서드를 완전히 빠져나가는 구문은 이렇게 써도 된다.
            if (a == 40) // 위애 애랑 똑같은 뜻
                return;
        }
    }
}
