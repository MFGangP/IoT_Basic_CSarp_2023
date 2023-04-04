using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CS03_object
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Object 형식
            int idata1 = 1024;
            /* C#에 있는 모든 것들은 클래스로 만들어져있다
            이렇게 써야되는게 맞는데 C,C++이랑 호환성을 위해서
            위에 처럼 쓸 수 있도록 만들어놨다. 
            int == System.Int32 
            long == System.Int64  */
            // 흐릿한 이유는 Int64 이렇게 쓰지 말라는 소리다.
            long val = 1111;
            long idata = 1024;
            Console.WriteLine(idata);
            Console.WriteLine(idata.GetType());

            Object iobj = (object)idata; // BOXING(박싱) : 데이터 타입의 값을 Object(오브젝트)로 담아라 
            Console.WriteLine(iobj); 
            Console.WriteLine(iobj.GetType());

            long udata = (long)iobj;
            Console.WriteLine(udata);
            Console.WriteLine(udata.GetType());

            double ddata = 3.141592;
            object pobj = (object)ddata;
            double pdata = (double)pobj; // UNBOXING(언박싱) : object 타입을 원래 타입으로 바꾸는 것
            // object에 들어간다고 object 타입으로 나오지는 않는다.
            Console.WriteLine(pobj);
            Console.WriteLine(pobj.GetType());

            Console.WriteLine(pdata);
            Console.WriteLine(pdata.GetType());

            short sdata = 32000;
            int indata = sdata;
            // 작은 데이터를 큰 데이터 타입으로 바꾸는 건 상관없다.
            Console.WriteLine(indata);
            long lndata = long.MaxValue;
            Console.WriteLine(lndata);
            indata = (int)lndata;
            // 큰 데이터를 작은 데이터 타입으로 바꾸면 오버플로우.
            Console.WriteLine(indata);

            //float double 간 형변환
            float fval = 3.141592f; // float형은 마지막에 f를 써줘야 함.
            Console.WriteLine("fval = "+fval);
            double dval = (double)fval;
            // 형변환 정밀도가 달라진다.
            Console.WriteLine("dval = " + dval);
            Console.WriteLine(fval == dval);
            Console.WriteLine(dval == 3.141592);

            // 정말 중요!! 실무에서 진짜 많이 쓴다.
            int numival = 1024;
            string strival = numival.ToString();
            Console.WriteLine(strival);
            Console.WriteLine(numival);
            //Console.WriteLine(numival == strival);
            Console.WriteLine(strival.GetType());

            double numdval = 3.14159265358979;
            string strdval = numdval.ToString();
            Console.WriteLine(strdval);

            // 반대 문자열을 숫자로 바꾸는 것
            // 문자열을 숫자로 바꿀 때 . 이 있는지 없는지가 진짜 크게 영향을 미친다.
            // 특수문자, 정수인데 점이 있거나, 
            string originster = "300000"; // "3million"은 예외 발생
            int convval = Convert.ToInt32(originster); // int.Parse()동일
            Console.WriteLine(convval);
            originster = "1.2345";
            float convfloat = float.Parse(originster);
            Console.WriteLine(convfloat);
            // 예외 발생하지 않도록 형 변환 하는 방법
            originster = "123.4f"; // 0 나온다는 소리는 Parse 했는데 비정상 종료
            float ffval; 
            // TryParse는 예외가 발행하면 값을 0대체, 예외 없으면 원래 값으로.
            float.TryParse(originster, out ffval); // 예외 발생하지않게 숫자 변환
            Console.WriteLine(ffval); // 가장 조심해야될 부분
            // 값 형식 - 스텍

            // 참조 형식 - 오브제트 객체
            const double pi = 3.14159266358970;
            Console.WriteLine(pi);

            // pi = 4.56; 상수는 못바꾼다.
        }
    }
}
