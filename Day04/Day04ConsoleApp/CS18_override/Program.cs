using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS18_override
{
    class ArmorSuite // 토니스타크 v.2
    {
        public virtual void Init() // public virtual
        {
            Console.WriteLine("기본 아머 슈트");
        }
    }

    class IronMan : ArmorSuite 
    {
        public override void Init()
        {
            base.Init(); // 부모 클래스의 Init 실행
            Console.WriteLine("리펄서 빔");
        }
    }
    class WarMachine : ArmorSuite
    {
        public override void Init()
        {
            // base.Init(); 주석처리하면 부모 클래스 Init 실행 안함
            base.Init(); // 부모 클래스의 Init 실행
            Console.WriteLine("미니건");
            Console.WriteLine("돌격소총");
        }
    }

    class Parents
    {
        public void CurrentMethod()
        {
            Console.WriteLine("부모 클래스 메서드");
        }
    }

    class Child : Parents
    {
        public new void CurrentMethod() // new 부모 클래스 메서드 완전히 숨기기
        {
            Console.WriteLine("자식클래스 메서드");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("아머슈트 생산");
            ArmorSuite suite = new ArmorSuite();   
            suite.Init();

            Console.WriteLine("워머신 생산");
            WarMachine machine = new WarMachine();
            machine.Init();

            Console.WriteLine("아이언맨 생산");
            IronMan iron = new IronMan();
            iron.Init();    

            Parents parent = new Parents();
            parent.CurrentMethod();

            Child child = new Child(); 
            child.CurrentMethod();
        }
    }
}
