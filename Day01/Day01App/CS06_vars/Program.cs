using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS06_vars
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 파이썬처럼 형식 지정 안해줘도 알아서 지가 해주는게 var
            // 선언과 동시에 초기화 필수, 지역 변수로만 사용 가능
            var a = 4000000;
            Console.WriteLine("Type = {0}, Value = {1}", a.GetType(), a);

            var b = 3.141592; // f여부에 따라서 알아서 double / float 변경
            Console.WriteLine("Type = {0}, Value = {1}", b.GetType(), b);

            var c = "Basic C#";
            Console.WriteLine("Type = {0}, Value = {1}", c.GetType(), c);
        }
    }
}
