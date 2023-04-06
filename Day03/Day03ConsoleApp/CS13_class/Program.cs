using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS13_class
{
    internal class Cat // private라도 같은 CS13_class 안에 있기 때문에 접근 가능
    {
        #region < 생성자 >
        // 초기화 할 거 없으면 컴파일러가 알아서 생성자 생성
        // 기본 생성자
        public Cat() 
        {
            // 디폴트 생성자 지정 방법 여러가지 중 1개
            Name = "무명이";
            Color = string.Empty;
            Age = 0;
        }

        /// <summary>
        /// 사용자 지정 생성자
        /// </summary>
        /// <param name="name"></param>
        /// <param name="color"></param>
        /// <param name="age"></param>
        public Cat(string name, string color = "흰색", sbyte age = 1)
        {
            Name = name;
            Color = color;
            Age = age;
        }
        #endregion

        #region < 종료자, 파괴자, 소멸자 >
        // 가비지 컬렉터가 객체 소멸 시점을 판단해서 종료자 호출
        // 파이썬, C#, 자바는 쓸 필요가 없다.
        #endregion

        #region < 정적 필드, 메소드 >
        // 인스턴스 소속 필드 vs 클래스 소속 필드
        // 인스턴스 - C++에서 배운 방식
        // 클래스 - 인스턴스를 만들지 않고 클래스의 이름을 통해 필드에 직접 접근
        // 클래스 - static
        #endregion

        #region < 멤버 변수 - 속성 >
        public string Name; // 고양이 이름
        public string Color; // 색상
        public sbyte Age; // 고양이 나이
        #endregion

        #region < 멤버 함수(메서드) - 기능 >
        public void Meow()
        {
            Console.WriteLine("{0} - 야옹~!!", Name);
        }

        public void Run()
        {
            Console.WriteLine("{0} 달린다.", Name);
        }
        #endregion
    }
    internal class Program
    {
        // 기본적으로 안쓰면 private 바깥에서 쓸 수 없다.
        // 클래스 자체는 같은 네임 스페이스 안에 같이 있기 때문에 가능.
        // internal은 이 프로그램 안에서 다 쓸 수 있다. 
        static void Main(string[] args)
        {
            // helloKitty라는 이름의 객체를 생성
            Cat helloKitty = new Cat(); // new = 객체 생성자
            helloKitty.Name = "헬로키티";
            helloKitty.Color = "흰색";
            helloKitty.Age = 50;
            helloKitty.Meow();
            helloKitty.Run();
            // 두 개는 다른 객체
            // 객체를 생성하면서 속성 초기화
            Cat nero = new Cat()
            {
                Name = "검은 고양이 네로",
                Color = "검은색",
                Age = 27
            };
            nero.Meow();
            nero.Run();
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("{0}의 색상은 {1}, 나이는 {2}입니다",
                helloKitty.Name, helloKitty.Color, helloKitty.Age);
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("{0}의 색상은 {1}, 나이는 {2}입니다",
                nero.Name, nero.Color, nero.Age);
            Console.WriteLine("--------------------------------------");
            Cat yaongi = new Cat()
            {
                // 기본 생성자 테스트
            };
            Console.WriteLine("{0}의 색상은 {1}, 나이는 {2}입니다",
            yaongi.Name, yaongi.Color, yaongi.Age);
            Console.WriteLine("--------------------------------------");
            Cat norangi = new Cat("노랑이", "노란색");
            Console.WriteLine("{0}의 색상은 {1}, 나이는 {2}입니다",
            norangi.Name, norangi.Color, norangi.Age);
            Console.WriteLine("--------------------------------------");


        }
    }
}
