using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS25_exception
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] array = { 1, 2, 3 };
            try
            {
                for (var i = 0; i < 5; i++)
                {
                    Console.WriteLine(array[i]);
                }
            }
            // 모르겠다 싶으면 Exception
            catch (Exception ex) // IndexOutOfRangeException 등등 많음
            {
                Console.WriteLine(ex.Message); // Tostring() - 내용이 많음
            }
            finally
            {
                // 예외가 발생하더라도 무조건 처리해야되는 로직은 여기에 쓴다.
                // file 객체 close
                // DB 연결 close
                // 네트워크 소켓 close
                Console.WriteLine("계속 감.");
            }

            

            try
            {
                DivideTest(array[2], 0);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.WriteLine("프로그램 종료!");
            //try
            //{
            //    Console.WriteLine("TEST TEST");
            //    throw new Exception("커스텀 예외");
            //}
            //catch(Exception e)
            //{
            //    Console.WriteLine(e.Message);
            //}
        }

        private static void DivideTest(int v1, int v2)
        {
            try
            {
                Console.WriteLine(v1 / v2);
            }
            catch
            {
                // 예외 던지기 - 메서드 만들었는데 구현을 안했다 라는 예외 알려주는 애
                // 현재 메서드에서 예외 처리하지 않고 메서드를 호출한 곳에서 예외처리한다. 
                throw new Exception("DivideTest 메서드에서 예외가 발생!");
            }
        }
    }
}
