using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// 네임스페이스는 폴더 이름까지 다 들어간다.
namespace WF13_bookrentalshop.Helpers
{

    internal class Commons
    {
        // 모든 프로그램 상에서 다 사용능 가능
        // DB 연결 문자열은 여기서만 수정하면 된다.
        // 이렇게 쓰는 다중 문자도 있으니까 알아두는게 좋음.
        public static readonly string ConnString = "Server=localhost;"+
                                                   "Port=3306;"+
                                                   "Database=Bookrentalshop;"+
                                                   "Uid=root;"+
                                                   "Pwd=12345;";
    }
}
