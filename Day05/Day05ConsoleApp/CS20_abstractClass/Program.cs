using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS20_abstractClass
{
    abstract class AbstractParent
    {
        protected void MethodA()
        {
            Console.WriteLine("AbstractParent.MethodA()");
        }
        public void MethodB() // 클래스랑 동일! 
            // 재정의를 하든지 할 수 있음
        {
            Console.WriteLine("AbstractParent.MethodB()");
        }

        public abstract void MethodC();// 인터페이스랑 기능은 동일! 추상 메서드
    }

    class Child : AbstractParent
    {
        public override void MethodC() 
        // 재정의가 아니고 구현인데, 인터페이스랑 구분을 위해서 이렇게 오버라이드라고 한다.
        {
            Console.WriteLine("Child.MethodC() - 추상클래스 구현!");
            MethodA(); 
        }
    }

    abstract class Mammal // 포유류 클래스 최상위 클래스
    {
        public void Nurse()
        {
            Console.WriteLine("포유한다.");
        }

        public abstract void Sound();
    }

    class Dogs : Mammal
    {
        public override void Sound()
        {
            Console.WriteLine("멍멍!!");
        }
    }
    class Cats : Mammal
    {
        public override void Sound()
        {
            Console.WriteLine("야옹!!");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            // 부모 클래스에다 자식 클래스를 할당함.
            // 특히 추상클래스는 이렇게 많이 쓴다.
            // 부모 클래스는 MethodC() 정의가 안되어있다.
            // 다른 자식에서 일을 각각 설정해놓은 다음 부모클래스에 할당해서
            // 여러가지 방식으로 쓰는듯. 조금씩 다른 일을 할 때
            AbstractParent parent = new Child();
            parent.MethodC();
            parent.MethodB();
            // parent.MethodA(); 
            // protected 이기 때문에 못씀 자기 자신과 자식 클래스 내에서만 사용 가능
        }
    }
}
