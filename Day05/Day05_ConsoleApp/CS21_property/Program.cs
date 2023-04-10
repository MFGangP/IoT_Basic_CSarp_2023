using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CS21_property
{

    class Boiler
    {
        // 안붙이면 protected
        private int temp; // 멤버 변수 소문자

        public int Temp // 프로퍼티(속성) 대문자로 시작
        {
            get { return temp; }
            set { if (value <= 10 || value >= 70)
                {
                    temp = 10;
                }
                else
                {
                    temp = value;
                }
            }
        }

        // 위의 get; set; 과 비교 // 아래의 Get 메서드와 Set 메서드는 Java에서 주로 사용, C#은 거의 안씀
        public void SetTemp(int temp)
        {
            if (temp <= 10 || temp >= 70) 
            {
                Console.WriteLine("수온 설정 값이 너무 낮거나 높습니다. 10~70도 사이로 지정해주세요");
                return;
            }
            else
            {
                this.temp = temp;
            }
        }
        public int GetTemp() { return this.temp; } 
    }

    class Car
    {
        // string name; 지우고
        // string color;
        int year;
        string fuelType;
        int door; // 프로퍼티에 get set으로 만들면 안써줘도 된다.
        string tireType;
        string company;
        int price;
        string carIdNumber;
        string carPlateNumber;
        // 전부 끌어다가 알트 엔터 하면 자동으로 만들어준다.
        // 조건만 밑에 붙이면 된다.

        //public Car (string name)
        //{

        //}

        /*
        private int door;
        public int Door
        {
            get { return door; }
            set
            {
                if (value != 2 || value != 4)
                {
                    value = 4;
                }
                door = value;
            }
        }*/
        // 오토 프로퍼티
        public string Name { get; set; } // 이렇게 하면 입력 불가. 

        // string color; 이렇게 쓰면 컨트롤 스페이스 했을 때 안뜸
        public string Color { get ; set; } // 필터링이 필요 없으면 멤버 변수 없이 프로퍼티로 대체
        // 들어오는 데이터를 필터링 할 때는 private 멤버 변수와 public 프로퍼티를 둘 다 사용
        public int Year { get => year; // 변수인데 부를 때는 프로퍼티라고 부른다
            // => == get {return year;} 줄여서 쓰게 만들어 줌 - 람다식.
            set {
                if (value <= 1990 || value >= 2023)
                {
                    year = 2023;
                }
                else
                {
                    year = value;
                }
            } 
        }
        public string FuelType
        {
            get => fuelType;
            set
            {
                if (value != "휘발유" || value != "경유")
                {
                    value = "휘발유";
                }
                else
                {
                    fuelType = value;
                }
            }

        }
        public int Door { get; set; }
        public string TireType { get ; set; }
        public string Company { get; set; } = "현대자동차"; // 오토 프로퍼티에 기본 할당.
        public int Price { get => price; set => price = value; }
        public string CarIdNumber { get; set; }
        public string CarPlateNumber { get; set; }
    }

    interface Iproduct
    {
        string ProductName { get; set; }
        void Produce();

    }

    class MyProduct : Iproduct
    {
        private string productName;
        // 인터페이스를 상속하면 인터페이스에서 한 약속을 지키라는 의미로
        // 인터페이스에 밑줄이 그어짐
        // 
        public string ProductName
        {
            get { return productName; }
            set { productName = value; }
        }

        public void Produce()
        {
            Console.WriteLine("{0}을(를) 생산합니다.", ProductName);
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Boiler kitturami = new Boiler();
            /*
            kitturami.temp = 60;

            //...
            kitturami.temp = 300000; // 보일러 물 수온이 30만도
            kitturami.temp = -120;
            // 이렇게 되면 안되니까 get set을 쓰는거다.
            */
            kitturami.SetTemp(50);


            Boiler navien = new Boiler();
            navien.Temp = 5000; // 오른쪽에 있는 값이 value 무조건 정해져 있는 형식
            Console.WriteLine(navien.Temp);

            Car ionic = new Car(); // get 만 살려놓으면
            // ionic.Name = "아이오닉"; // 데이터 오염을 막을 수 있다. 값 입력 불가
            Console.WriteLine(ionic.Name); // get을 안쓰면 Console.WriteLine 에서 쓸 수가 없다.

            // 생성할 때 프로퍼티로 초기화
            Car genesis = new Car()
            {
                // 프로퍼티는 스패너 모양으로 나온다. 컨트롤 스페이스
                
                Name = "제네시스",
                FuelType = "휘발유",
                Color = "흰색",
                Door = 4,
                TireType = "광폭타이어",
                Year = 0,
            };
            Console.WriteLine("자동차 제작회사는 {0}입니다.", genesis.Company);
            Console.WriteLine("자동차 제작년도는 {0}입니다.", genesis.Year);
        }
    }
}
