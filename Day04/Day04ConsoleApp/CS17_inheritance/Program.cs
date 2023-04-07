using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS17_inheritance
{
    // 상속해줄 부모 클래스
    class Parent
    {
        public string Name; // 상속 할 때는 private을 쓰면 안됨

        public Parent(string Name)
        {
            this.Name = Name;
            Console.WriteLine("{0} from Parent 생성자", Name);
        }

        public void ParentMethod ()
        {
            Console.WriteLine("{0} from Parent 메서드", Name);
        }
    }
    // 상속받을 자식 클래스
    class Child : Parent
    { 
        public Child(string Name) : base(Name) // 부모 생성자 실행
        {
            // 부모 생성자 호출하고 자기 생성자 실행
            Console.WriteLine("{0} from Child 생성자", Name) ;
        }

        public void ChlidMethod()
        {
            Console.WriteLine("{0} from Child 메서드", Name);
        }
    }
 
    // 클래스 간 형변환 DB처리, DI(의존관계 주입)
    class Mammal // 포유류 클래스
    {
        public void Nurse() // 기르다
        {
            Console.WriteLine("포유류 기르다");
        }
    }

    class Dogs : Mammal
    {
        public void Bark()
        {
            Console.WriteLine("멈뭄이");
        }
    }

    class Cats : Mammal
    {
        public void Meow()
        {
            Console.WriteLine("야옹이");
        }
    }
    class Elephants : Mammal
    {
        public void Poo()
        {
            Console.WriteLine("코끼리");
        }
    }

    class Zookeeper
    {
        public void Wash(Mammal mammal)
        {
            ///
            if (mammal is Elephants)
            {
                var animal = mammal as Elephants;
                Console.WriteLine("코끼리를 씻깁니다.");
                animal.Poo();
            }
            else if (mammal is Dogs) 
            {
                var animal = mammal as Dogs;
                Console.WriteLine("멈뭄이를 씻깁니다.");
                animal.Bark();
            }
            else if (mammal is Cats)
            {
                var animal = mammal as Cats;
                Console.WriteLine("고양이를 씻깁니다.");
                animal.Meow();
                animal.Meow();
                animal.Meow();
                animal.Meow();
                animal.Meow();
            }
        }

    }


    internal class Program
    {
        static void Main(string[] args)
        {
            #region < 기본 상속 개념 >
            Parent p = new Parent("p");
            p.ParentMethod();

            Console.WriteLine("자식 클래스 생성");
            Child c = new Child("c");
            c.ParentMethod(); // 자식 클래스는 부모 클래스의 속성, 기능을 모두 쓸 수 있다.
            // 단 public, protected일 때만
            c.ChlidMethod();
            #endregion

            #region < 클래스간 형식 변환 >

            // Mammal mammal = new Mammal(); // 기본
            // 자식 클래스는 부모의 특성을 가지고 있다.
            Mammal mammal = new Dogs();
            // mammal.Bark(); // 바로 안됨 아예 안되는건 아님.

            // 확실하게 형을 비교하고 바꾸기 위해 이렇게 쓴다.
            if (mammal is Dogs) // True False 비교 할 때 
            {
                Dogs midDog = mammal as Dogs; // 형변환 할 때
                //            (Dogs)mammal
                midDog.Bark();  
            }
            // 자식이 부모를 품을 수 없음.
            // 부모 클래스는 자식 클래스의 특성을 가질 수 없음.
            // Dogs dogs = new Mammal(); 

            Dogs dog2 = new Dogs();
            Cats cat2 = new Cats();
            Elephants el2 = new Elephants();

            Zookeeper keeper = new Zookeeper();
            keeper.Wash(dog2);
            keeper.Wash(cat2);
            keeper.Wash(el2);

            #endregion




        }
    }
}
