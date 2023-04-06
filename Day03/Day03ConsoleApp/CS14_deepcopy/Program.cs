using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS14_deepcopy
{

    #region < 얕은 복사, 깊은 복사 >
    // 일반 데이터 형식을 이야기 하는게 아니다. 객체나 배열 string들 이야기 하는거임.
    // 객체를 복사 할 때 참조만 살짝 복사 - 객체는 기본이 Call by Reterence
    // 별도의 힙 공간에 객체 자체를 복사
    #endregion

    class SomeClass
    {
        public int SomeField1;
        public int SomeField2;

        public SomeClass DeepCopy()
        {
            // 
            SomeClass newCopy = new SomeClass();
            // 얘는 누가 누구 SomeField 인지 확실하게 알 수 있어서 this를 빼도 된다.
            newCopy.SomeField1 = this.SomeField1; // Call By Value
            newCopy.SomeField2 = this.SomeField2;

            return newCopy;
        }
    }

    class Employee
    {
        private string Name; // 얘가 this의 Name

        public void SetName(string Name) // 얘는 매개변수로 받는 Name
        {
            // 이때는 필수 누가 누군지 모르기 때문에
            // 멤버변수(속성)와 메서드의 매개변수 이름이 대소문자 까지 완전히 똑같을 때
            this.Name = Name; 
        }
    }

    class ThisClass
    {
        // 클래스 상속처럼 물려받는 형식
        int a, b, c;

        public ThisClass()
        {
            this.a = 1;
        }
        public ThisClass(int b) : this()
        {
            this.b = b;
        }
        public ThisClass(int b, int c) : this(b)
        {
            this.c = c;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("얕은 복사 시작");
            // source와 target이 (주소 복사) 값을 share
            SomeClass source = new SomeClass();
            source.SomeField1 = 100;
            source.SomeField2 = 200;
            // 객체 복사
            // 객체는 할당해주면 객체의 주소를 건내준다. 
            SomeClass target = source;
            target.SomeField2 = 300;
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("s.SomeField1 => {0}, s.SomeField => {1}",
                                source.SomeField1, source.SomeField2);
            Console.WriteLine("t.SomeField1 => {0}, t.SomeField => {1}",
                                target.SomeField1, target.SomeField2);
            Console.WriteLine("--------------------------------------");

            Console.WriteLine("깊은 복사 시작");
            SomeClass s = new SomeClass();
            s.SomeField1 = 100;
            s.SomeField2 = 200;
            SomeClass t = s.DeepCopy();  // 깊은 복사
            t.SomeField2 = 300;
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("s.SomeField1 => {0}, s.SomeField => {1}",
                                s.SomeField1, s.SomeField2);
            Console.WriteLine("t.SomeField1 => {0}, t.SomeField => {1}",
                    t.SomeField1, t.SomeField2);
            Console.WriteLine("--------------------------------------");

        }
    }
}
