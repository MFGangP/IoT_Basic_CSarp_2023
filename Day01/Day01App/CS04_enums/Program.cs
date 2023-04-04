using System;

namespace CS04_enums
{
    internal class Program
    {
        // 내가 일일이 비교하면서 지정 안해도 알아서 해준다.
        enum HomeTown
        {   // 0, 1, 2, 3, 4
            // CTRL + SHIFT + U 전부 대문자
            SEOUL = 1,
            DAEJEON = 2,
            DAEGU,
            BUSAN,
            JEJU = 9
        }
        static void Main(string[] args)
        {
            HomeTown myHomeTown;
            
            myHomeTown = HomeTown.BUSAN;
            Console.WriteLine(myHomeTown);

            if (myHomeTown == HomeTown.SEOUL)
            {
                Console.WriteLine("서울 출신입니다.");
            } else if (myHomeTown == HomeTown.DAEJEON)
            {
                Console.WriteLine("대전 출신입니다.");
            } else if (myHomeTown == HomeTown.DAEGU)
            {
                Console.WriteLine("대구 출신입니다.");
            } else if (myHomeTown == HomeTown.BUSAN)
            {
                Console.WriteLine("부산 출신입니다.");
            }
        }
    }
}
