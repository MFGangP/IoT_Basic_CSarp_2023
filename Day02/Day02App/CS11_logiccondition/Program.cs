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
            // #region 제목 
            // #endregion
            #region < If 구문 > 
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
            #endregion

            #region < 데이터 타입 비교 Switch 구문 >
            // 데이터 타입 비교 switch문 (C# 7.0부터 .NET framework 4.7 / 4.8)
            object obj = null;

            string inputs = Console.ReadLine(); // 콘솔에서 입력
            if (int.TryParse(inputs, out int iouput)) // 예외가 발생하면 0
            {
                obj = iouput; // 입력한 값이 정수라서 문자열을 정수로 변환
            }
            else if (float.TryParse(inputs, out float foutput))
            {
                obj = foutput; // 입력한 값이 실수라서 문자열을 실수로 변환
            }
            else
            {
                obj = inputs; // 이도 저도 아니다.
            }

            Console.WriteLine(obj);

            switch (obj) 
            {
                case int i: // 지금 들어온 값이 정수라면
                    Console.WriteLine("{0}는 int 형식입니다.", i);
                    break;
                case float f: // 들어온 값이 실수라면
                    Console.WriteLine("{0}는 float 형식입니다.", f);
                    break;
                case string s: // 들어온 값이 문자열이면
                    Console.WriteLine("{0}는 string 형식입니다.", s);
                    break;
                default: // 이외 나머지
                    Console.WriteLine("몰?루");
                    break;
            }
            #endregion

            #region < 이중 For문 >

            for (int x = 2; x <= 9; x++)
            {
                for (int y = 1; y <= 9; y++)
                {
                    Console.WriteLine("{0} x {1} = {2}", x, y, x * y);
                }
            }

            #endregion

            #region < Foreach 문 >
            // C#은 배열이랑 리스트랑 아예 다르다.
            int[] ary = { 2, 4, 6, 8, 10 };
            // for 문을 써도 된다.
            foreach (int i in ary) // 배열이나 컬렉션(리스트) 
            {
                Console.WriteLine("{0}^2 = {1}", i, i*2);
            }
            // for 문보다 훨씬 쓰기 편하기 때문에 쓰는거다.
            for (int i = 0; i < ary.Length; i++)
            {
                Console.WriteLine("{0}^2 = {1}", ary[i], ary[i] * 2);
            }

            #endregion
        }
    }
}
