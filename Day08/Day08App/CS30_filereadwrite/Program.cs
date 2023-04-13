using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS30_filereadwrite
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string line = string.Empty; // 텍스트를 읽어와서 출력

            StreamReader reader = null;
           
            try
            {
                reader = new StreamReader("./python.py"); // 스트림리더 파일 연결
                line = reader.ReadLine(); // 한 줄씩 읽음
            
                while (line != null)
                {
                    // 한 줄 읽은 것 출력
                    Console.WriteLine(line);
                    // 라인 바꿔서 다음 줄 읽어 출력
                    line = reader.ReadLine();
                }
                // 파일을 읽으면 무조건 닫아줘야한다.
                reader.Close();
            
            }
            catch (Exception ex)
            {
                Console.WriteLine($"예외발생 {ex.Message}");
            }
            finally
            {
                reader.Close(); // 파일 읽으면 무조건 마지막에 닫아줘야한다.
            }

            // 읽기 끝

            StreamWriter writer = null;


            writer = new StreamWriter(@"./pythonByCshar.py");

            try
            {
                writer.WriteLine("import sys");
                writer.WriteLine("");
                writer.WriteLine("print(sys.executable)");
            }
            catch(Exception ex)
            { 
                writer.Close();
            }
            Console.WriteLine("파일생성 완료!");
        }
    }
}
