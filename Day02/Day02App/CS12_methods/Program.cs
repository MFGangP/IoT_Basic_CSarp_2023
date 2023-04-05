using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS12_methods
{
    class Calculator
    {
        // static(정적) - 최초 실행에 메모리 올라감.
        // 프로그램이 시작하자마자 메모리에 올라간다.
        // 클래스의 객체를 만들 필요가 없음. 언제든지 접근 가능 like new Calc();
        public static int Plus(int a, int b)
        {
            return a + b;
        }

        public int Minus(int a, int b)
        { 
            return a - b; 
        }

    }
    internal class Program
    {
        /// <summary>
        /// 실행시 최초 메모리에 올라가야 되기 때문에 static이 되어야하고
        /// 메서드 이름이 Main 이면 실행될 때 알아서 제일 처음에 시작된다.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            #region < static 메서드 >

            int result = Calculator.Plus(1, 2);
            // static이 아니면 이렇게 되야한다. 동적 할당 heap(힙) 영역.
            // int result = new Calculator().Plus(1, 2);
            // Calc.Minus(3, 2); // Minus는 static이 아니기 때문에 접근 불가(객체생성해야 접근가능)
            result = new Calculator().Minus(3, 2);
            Console.WriteLine(result);
            #endregion

            #region < Call by Reference vs Call by Value 비교 >
            int x = 10; int y = 3;
            // ref가 없으면 Call By Value
            Swap(ref x, ref y); // x, y가 가지고 있는 주소를 전달 Call By Reference == pointer

            Console.WriteLine("x = {0}, y = {1}", x, y);

            Console.WriteLine(GetNum());
            #endregion

            #region < out 매개변수 >

            int divid = 10;
            int divor = 3;
            int rem= 0;
            Divide(divid, divor, out result, out rem); // ref를 쓰든 out을 쓰든 똑같다.
            Console.WriteLine("나누기 값 : {0}, 나머지 : {1}", result, rem);

            (result, rem) = Divide(20, 6);
            Console.WriteLine("나누기 값 : {0}, 나머지 : {1}", result, rem);

            #endregion

            #region < 가변길이 매개변수 >

            Console.WriteLine(Sum(1, 3, 5, 7, 9));

            #endregion

        }

        //static int Divide(int x, int y)
        //{
        //    return x / y;
        //}
        //static int Reminder(int x, int y)
        //{
        //    return x % y;
        //}
        // 메서드 두개 만들걸 아래처럼 하나로
        //static void Divide(int x, int y, ref int val, ref int rem)
        static void Divide(int x, int y, out int val, out int rem)
        {
            val = x / y;
            rem = x % y;
        }
        // 둘 다 똑같음 아래는 튜플
        static (int result, int rem) Divide(int x, int y)
        {
            return (x / y, x % y); // C# 7.0
        }

        static (float result, int rem) Divide(float x, float y)
        {
            return (x / y, (int)(x % y)); // C# 7.0
        }
        // Main 메서드랑 같은 레벨에 있는 메서드들은 public은 안되고
        // 전부 static이 되어야 한다. ( 무조건 )

        // 단일 포인터 - 1차원 배열
        // 2중 포인터 - 2차원 배열
        // 3중 포인터 - 3차원 배열
        public static void Swap(ref int a, ref int b)
        {
            int temp = 0;
            temp = a; // 5 : temp = 5
            a = b; // 4 : a = 4
            b = temp; // 5
        }

        static int val = 100;
        // static메서드에 접근할 수 있는 변수는 static 밖에 없다.
        public static ref int GetNum()
        {
            return ref val;
        }
        public static int Sum(params int[] args) // Python 가변길이 매개변수 (*args) 랑 비교
        {
            int sum = 0;

            foreach(var item in args)
            {
                sum += item;
            }
            return sum;
        }
    }
}
