using System;

namespace CS05_nullable
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int? a = null; // int a null값을 담을 수 없음 C# 6.0 nullable
            // int a = 16;
            Console.WriteLine(a == null);
            // Console.WriteLine(a.GetType()); // 예외 발생

            // 기본적으로 값 형식(byte, short, int, long, float, double, char)들은 null 값을 넣을 수 없다.
            // null을 할당할 수 있도록 만드는 방식 type?
            int b = 0;
            Console.WriteLine(b == null);
            Console.WriteLine(b.GetType());

            float? c= null;
            Console.WriteLine(c.HasValue);
            // c에 값이 안들어가있는 상태에서 부르면 비어있거나 에러 뜬다.
            // HasValue로 값이 있는지 없는지 체크해서 있으면 부르면 된다.
            // DB랑 연동하면 null 값이 많이 쓰이니까 쓸 일이 많다.
            c = 3.14f;
            Console.WriteLine(c.HasValue);
            Console.WriteLine(c.Value);
            Console.WriteLine(c);
        }
    }
}
