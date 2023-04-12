using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS27_delegatechain
{
    delegate void ThereIsAFire(string location); // 불 났을 때 대신해주는 대리자

    delegate int Calc(int a, int b); // 

    delegate string Concatenate(string[] args);
    #region < 클래스 Sample 람다식 프로퍼티 >
    class Sample
    {
        private int valueA; // 맴버변수 - 외부에서 접근 불가

        public int ValueA // 프로퍼티
        {
            //get { return valueA; }
            //set { valueA = value; }
            get => valueA; // 람다식
            set => valueA = value; // { valueA = value; } 일반식
        }
    }
    #endregion

    internal class Program
    {
        #region < 대리자 함수 >
        static void Call119(string location)
        {
            Console.WriteLine("소방서죠? {0}에 불났어요", location);
        }
        static void ShoutOut(string location)
        {
            Console.WriteLine("{0}에 불났어요", location);
        }
        static void Escape(string location)
        {
            Console.WriteLine("{0}에서 탈출합니다.", location);
        }
        #endregion
        static string ProcConcate(string[] args)
        {
            string result = string.Empty; // == ""
            foreach (string s in args)
            {
                result += s + "/";
            }
            return result;
        }

        static void Main(string[] args)
        {
            #region < 대리자 체인 영역 >
            /*
            // 직접 메서드를 다 호출 해야하는 경우
            var loc = "우리집";
            Call119(loc);
            ShoutOut(loc);
            Escape(loc);

            // 대리자 쓰는 경우
            // 불이 날 수도 있으니까 미리 준비
            var otherloc = "경찰서";
            ThereIsAFire Fire = new ThereIsAFire(Call119);
            Fire += new ThereIsAFire(ShoutOut);
            Fire += new ThereIsAFire(Escape); // 대리자에 메서드 추가

            Fire(otherloc);

            Fire -= new ThereIsAFire(ShoutOut); // 대리자에서 메서드 삭제

            Fire("다른집");

            // 익명함수
            Calc puls = delegate (int a, int b)
            {
                return a + b;
            };

            Console.WriteLine(puls(6,7));
            // 이렇게도 가능한데 이건 람다식을 알아야 할 수 있다.
            // 잠시 쓰다 버릴 함수 만들 때 익명 함수를 쓴다.
            Calc Minus = (a, b) => { return a - b; };

            Console.WriteLine(Minus(67, 9));
            // (익명)함수를 간결하게 쓰는게 목적
            Calc simpleMinus = (a, b) => a - b; // 람다식
            */
            #endregion
            Concatenate concat = new Concatenate(ProcConcate);
            var result = concat(args);

            Console.WriteLine(result);

            Console.WriteLine("람다식으로");
            Concatenate concat2 = (arr) =>
            {
                string res = string.Empty; // == ""
                foreach (string s in args)
                {
                    res += s + "/";
                }
                return res;
            };
            Console.WriteLine(concat2(args));
        }
    }
}
