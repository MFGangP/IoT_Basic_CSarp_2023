using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS28_event
{
    // 이벤트를 사용하려면
    // 1. 대리자를 생성
    // EventHandler라는 이름을 가진 대리자를 만들껀데 매개 변수로 string을 받을거야
    delegate void EventHandler(string message);

    class CustomNotifier 
    {
        // 2. 대리자를 쓰고 싶은 곳에 이벤트를 준비(대리자를 사용한)
        // 하얀색 EventHandler 델리게이트의 이름 - 이벤트 이름
        public event EventHandler SomethingChanged;
        // DoSomething이라는 멤버함수 내에 있는 특정 조건을 만족 할 때 이벤트가 발생한다.
        public void DoSomething(int number)
        {
            int temp = number % 10;

            if (temp != 0 && temp % 3 == 0) 
            {
                // 3. 특별한 이벤트가 발생 할 상황에서 이벤트를 수행
                SomethingChanged(string.Format("{0} : odd", number));
            }
        }
    }

    internal class Program
    {
        // 4. 이벤트가 대신 호출할 메서드 
        // 같은 타입이여야된다.
        static void CustomHandler(string message)
        {
            Console.WriteLine(message);
        }

        static void Main(string[] args)
        {
            // CustomNotifier 클래스의 객체 notifier를 동적할당 해서 생성
            // 그러면 힙 영역에 하나가 생기는거겠지
            CustomNotifier notifier = new CustomNotifier();
            // 대리자 체인 대리자 하나로 여러개 메소드 동시 참조 가능
            // notifier 객체의 이벤트가 호출되면 대리자가 CustomHandler를 사용해서 일을 대신 처리해줄거야.
            notifier.SomethingChanged += new EventHandler(CustomHandler);
            
            for (int i = 0; i <= 30; i++)
            {
                notifier.DoSomething(i);
            }

        }
    }
}
