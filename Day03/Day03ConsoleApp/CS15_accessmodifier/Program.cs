using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS15_accessmodifier
{
    internal class WaterHeater // class에 기본 접근 한정자 internal
    {
        protected int temp; // 내부에서 접근, 상속받은 자식 접근 가능
        // 외부 접근 불가.
        // 얘가 public이 되버리면 GET, SET을 만들 필요가 없다.
        public void SetTemp(int temp)
        {
            if (temp < -5 || temp > 40)
            {
                Console.WriteLine("범위 이탈");
                return;
            }

            this.temp = temp;
        }
        public int GetTemp() 
        { 
            return this.temp; 
        }
        internal void TurnOnHeater()
        {
            Console.WriteLine("보일러를 켭니다.\n현재 온도 : {0}", temp);
        }
    }






    internal class Program
    {
        static void Main(string[] args)
        {
            WaterHeater boiler = new WaterHeater();
            boiler.SetTemp(30);
            Console.WriteLine(boiler.GetTemp());
            boiler.TurnOnHeater();
            // boiler.temp = 30;
        }
    }
}
