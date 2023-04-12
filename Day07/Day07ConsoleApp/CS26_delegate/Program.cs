using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS26_delegate
{
    // 대리자 사용하겠다는 선언
    // 매개 변수는 원래 있는 매개 변수랑 동일하게 써준다.
    delegate int CalcDelegate(int a, int b);

    delegate int Compare(int a, int b); // a, b 중 뭐가 큰 지(작은 지) 비교 대리자

    class Calc
    #region < 대리자 학습 기본 >
    {
        public int Plus(int a, int b)
        {
            return a + b;
        }
        // static이 붙으면 무조건 실행 될 때 최초 메모리에 올라감
        // 프로그램 실행중에는 언제든지 접근 가능
        // 대신 private는 될 수 없다.
        public static int Minus(int a, int b) // 시작 할 때 메모리에 올라감
        {
            return a - b;
        }
    }
    #endregion
    internal class Program
    {
        static int AscendCompare(int a, int b)
        {
            if (a > b) { return 1; }
            else if (a == b) { return 0; }
            else return -1;
        }

        static int DescendCompare(int a, int b)
        {
            if (a < b) { return 1; }
            else if (a == b) { return 0; }
            else return -1;
        }

        // 오름차순, 내림차순 정렬 하나의 메서드에서 정리가능
        static void BubbleSort(int[] DataSet, Compare comparer )
        {
            int i = 0, j = 0, temp = 0;

            for ( i = 0; i < DataSet.Length - 1; i++ )
            {
                for (j = 0; j < DataSet.Length - (i + 1); j++ )
                {
                    if (comparer(DataSet[j], DataSet[j+1]) > 0) // 대리자 사용
                    {
                        temp = DataSet[j + 1];
                        DataSet[j + 1] = DataSet[j];
                        DataSet[j] = temp; // Swap
                    }
                }
            }
        }

        static void BubbleSort2(int[] DataSet, bool isAscend)
        {
            int i = 0, j = 0, temp = 0;

            for (i = 0; i < DataSet.Length - 1; i++)
            {
                for (j = 0; j < DataSet.Length - (i + 1); j++)
                {
                    if (isAscend == true) // 대리자 사용
                    {
                        if (DataSet[j] > DataSet[j + 1])
                        {
                            temp = DataSet[j + 1];
                            DataSet[j + 1] = DataSet[j];
                            DataSet[j] = temp; // Swap
                        }
                    }
                    else
                    {
                        if (DataSet[j] < DataSet[j + 1])
                        {
                            temp = DataSet[j + 1];
                            DataSet[j + 1] = DataSet[j];
                            DataSet[j] = temp; // Swap
                        }
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            #region < 대리자 기본 예 >
            // 일반적으로 클래스 사용하는 방식 - 직접 호출
            Calc nomalCalc = new Calc();
            
            int x = 10, y = 15;
            Console.WriteLine(nomalCalc.Plus(x, y));

            int res = nomalCalc.Plus(x, y);
            Console.WriteLine(res);

            // 안됌 nomalCalc.Minus();
            res = Calc.Minus(x, y);
            Console.WriteLine(res);

            // 대리자를 사용하는 방식 - 대리자가 대신 실행
            x = 30; y = 25;
            Calc delCalc = new Calc();
            CalcDelegate Callback;

            Callback = new CalcDelegate(delCalc.Plus);
            int res2 = Callback(x,y); // = Calc.plus() 대신 호출
            Console.WriteLine(res2);
            // 호출자가 메서드를 직접 부르지 않는다.
            // 쓸데 없어보이는데 쓰이는 데가 다 있다.
            // 배달 앱을 쓰는 이유랑 비슷함.

            // 이게 가능한 이유는 모든 데이터 형태가 같기 때문이다.
            // 다르면 불가능 함.
            // 값이 아닌 코드 자체를 매개변수로 넘기고 싶을 때 쓴다.
            Callback = new CalcDelegate(Calc.Minus);
            res2 = Callback(x,y);
            Console.WriteLine(res2);
            #endregion

            #region
            int[] Origin = { 4, 7, 8, 2, 9, 1 };

            Console.WriteLine("오름차순 버블 정렬");
            BubbleSort(Origin, new Compare(AscendCompare));

            foreach (int item in Origin)
            {
                Console.Write("{0} ",item);
            }
            Console.WriteLine();

            Console.WriteLine("내림차순 버블 정렬");
            BubbleSort(Origin, new Compare(DescendCompare));

            foreach (int item in Origin)
            {
                Console.Write("{0} ", item);
            }
            Console.WriteLine();

            #endregion

        }
    }
}
