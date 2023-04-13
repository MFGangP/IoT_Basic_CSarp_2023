using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS29_filehandling
{
    // 스트림 파일을 쪼개서 물 흐르듯 보냄
    // 
    internal class Program
    {
        static void Main(string[] args)
        {
            // Directory == Folder
            // "C:\\Dev" @"C:\Dev" 리터럴은 여러줄의 문자열을 만들 때도 사용했고, 특수문자 넣을 때도 유용하다.
            string curDirectory = @"C:\Temp"; //C:\Dev 절대 경로, . 현재 디렉토리(상대경로), .. 부모 디렉토리
            Console.WriteLine("현재 디렉토리 정보");
            // 로컬 영역에서 받는 타입이 뭔지 햇갈릴 때

            string[] dirs = Directory.GetDirectories(curDirectory);

            foreach(string dir in dirs) 
            {
                var dirInfo = new DirectoryInfo(dir);
             
                Console.Write(dirInfo.Name); // 현재 디렉토리 이름
                Console.WriteLine(" [{0}]", dirInfo.Attributes); // 현재 디렉토리 내 디렉토리들
            }

            Console.WriteLine("현재 디렉토리 파일 정보");

            string[] files = Directory.GetFiles(curDirectory);
            foreach (var file in files)
            {
                var fileInfo = new FileInfo(file);
                
                Console.Write(fileInfo.Name); // 현재 디렉토리 파일 이름
                Console.WriteLine(" [{0}]", fileInfo.Attributes); // 현재 디렉토리 내 파일들
            }

            // 특정 경로에 하위 폴더/ 하위 파일 조회

            string path = @"C:\Temp\CSharp_Bank"; // 만들고자 하는 폴더 폴더 구분자로 / 도 가능
            string sfile = @"Test.log"; // 생성할 파일
            if (Directory.Exists(path)) // 이런 디렉토리가 있으면
            {
                Console.WriteLine("파일 경로가 존재합니다. 파일을 생성합니다.");
                File.Create(path + @"/" + sfile);  // C:\temp\CSharp_Bank\Test.log
            }
            else
            {
                Console.WriteLine($"파일 경로가 존재하지 않습니다 : {path}에 생성합니다.");
                Directory.CreateDirectory(path);

                Console.WriteLine("경로를 생성하여 파일을 생성하였습니다.");
                File.Create(path + @"/" + sfile); // 이제는 / 도 가능하게 바꼈다.
            }
        }
    }
}
