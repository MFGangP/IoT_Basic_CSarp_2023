using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS19_interface
{
    // 인터페이스 쓸 때는 앞에 대문자 I를 붙여준다. 햇갈려서
    // 약속 - 이렇게 생긴건 반드시 만들어써라.
    // 앞으로 ILogger를 상속받아서 쓰는 애들은
    // WriteLog를 무조건 만들어서 써라
    interface ILogger
    {
        // ILogger 역할 이걸 구현해라
        void WriteLog(string log);
    }

    interface IFormattableLogger : ILogger 
    {
        // ILogger도 구현하고
        // 밑에 줄도 구현해라
        void WriteLog(string format, params object[] args); // args 다중 파라미터

    }


    // 클래스면 상속이라고 부르는데
    // 인터페이스를 상속하면 implement(구현)이라고 부른다.
    // 인터페이스를 가져다 쓰면 처음에 밑줄이 뜬다.
    // 만들라고 한거 안만들어놔서 밑줄 뜨는거다. 약속이기 때문에 문법오류
    class ConsoleLogger : ILogger
    {
        // ALT + Enter 하면 알아서 만들어준다.
        public void WriteLog(string log)
        {
            // throw new NotImplementedException();
            Console.WriteLine("{0}, {1}", DateTime.Now.ToLocalTime(), log);
        }
    }

    class ConsoleLogger2 : IFormattableLogger
    {
        public void WriteLog(string format, params object[] args)
        {
            // throw new NotImplementedException();
            string message = string.Format(format, args);
            Console.WriteLine("{0} {1}",DateTime.Now.ToLocalTime(), message);
        }

        public void WriteLog(string log)
        {
            // throw new NotImplementedException();
            Console.WriteLine("{0} {1}", DateTime.Now.ToLocalTime(), log);
        }
    }

    class Car
    {
        public string Name { get; set; }   
        public string Color { get; set; }
        public void Stop() 
        {
            Console.WriteLine("정지!");   
        }
    }

    interface IHoverable
    {
        void Hover(); // 물에서 달린다
    }

    interface IFlyable
    {
        void Fly(); // 날다
    }

    // C#에는 다중상속이 없음.
    // 다중 상속 문제를 해결하기 위해 만든 방식
    class FlyHoverCar : Car, IFlyable, IHoverable
    {
        public void Fly()
        {
            // throw new NotImplementedException();
            Console.WriteLine("납니다.");
        }

        public void Hover()
        {
            // throw new NotImplementedException();
            Console.WriteLine("물에서 달립니다.");
        }
    }




    internal class Program
    {
        static void Main(string[] args)
        {
            // 인터페이스는 인터페이스에 값 할당
            // 인터페이스를 구현하면 이렇게 많이 쓴다.
            ILogger logger = new ConsoleLogger();
            logger.WriteLog("안녕~!!!");

            IFormattableLogger logger2 = new ConsoleLogger2 ();
            logger2.WriteLog("{0} x {1} = {2}", 6, 5, (6 * 5));

        }
    }
}
