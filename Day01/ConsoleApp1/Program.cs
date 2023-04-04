using System;
// include, import
// 패키지랑 같은 의미
// 안에 여러개 클래스가 존재한다.
// 네임스페이스 ConsoleApp1
namespace ConsoleApp1
{
    /// <summary>
    /// XML 주석
    /// 코드를 문서화 할 때 자동화 해주는 기능
    /// 프로그램 클래스
    /// </summary>
    // private, public 같은 애
    // private랑 비슷하다.
    // 파일명과 클래스명이 같지 않을 수도 있음.
    // 근데 동일하게 써라 관리하기 편하니까.
    // 확장자 .cs c#
    internal class Program
    {
        /// <summary>
        /// 메인 엔트리 포인트
        /// </summary>
        /// <param name="args">콘솔 매개 변수</param>
        static void Main(string[] args)
        {
            // 이 콘솔에 글자를 보여주는 애는 System에 들어있다.
            // using 문이 활성화 안되있는 이유는 using을 안써서 그렇다.
            // System을 빼면 애가 활성화 된다.
            // 전구 모양은 VS 로잘린이 내 코딩을 도와주는 역할을 할 수 있다는 표시.
            // ALT + ANTER
            Console.WriteLine("Hello C#!!");
        }
    }
}
