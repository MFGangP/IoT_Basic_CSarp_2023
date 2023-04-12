using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient; // SQL Server용 DB연결 클라이언트 네임스페이스
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WF09_dbhandling
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }
        // WF08 파일이랑 똑같은 역할을 코딩으로만 구현하는 방법.
        private void FrmMain_Load(object sender, EventArgs e)
        {
            // 1. 연결 문자열 생성
            string connectionString = "Data Source=localhost;Initial Catalog=Northwind;Persist Security Info=True;User ID=sa;Password=12345;";
            // 2. DB연결을 위해서 Connection 객체를 생성
            SqlConnection conn = new SqlConnection(connectionString); // 연결 문자열 없이 객체 생성하면 안된다. // 첫번째 방법
            // conn.ConnectionString = connectionString; // 두번째 방법
            // 3. 객체를 통해서 DB 연결
            conn.Open();

            // 4. DB 처리를 위한 쿼리 작성 
            // 5. SqlDataAdapter 생성
            // @ 쓰면 파이썬의 '''랑 똑같은 효과
            string selQuery = @"SELECT CustomerID
                                     , CompanyName
                                     , ContactName
                                     , ContactTitle
                                     , Address
                                     , City
                                     , Region
                                     , PostalCode
                                     , Country
                                     , Phone
                                     , Fax
                                  FROM Customers";
            // SqlCommand selCmd = new SqlCommand(selQuery, conn);
            // 위 아래 둘 다 똑같다.
            // selCmd.Connection = conn;
            SqlDataAdapter adapter = new SqlDataAdapter(selQuery, conn); // 중간에서 데이터를 받아서 넘겨주는 객체
                        
            // PASS 5.리더 객체 생성, 값을 넘겨줌
            //SqlDataReader reader = selCmd.ExecuteReader();
            // PASS 6. 데이터 리더에 있는 데이터를 데이터셋으로 보내기
            //DataSet ds = new DataSet();

            // 6. 데이터 셋으로 전달
            DataSet ds = new DataSet(); // 테이블 여러개 담을 수 있음
            adapter.Fill(ds); // adapter를 통해 만들어진 데이터가 ds에 다 들어간다.

            // 7. 데이터 그리드 뷰에 바인딩 하기위한 BindingSource 생성
            BindingSource source = new BindingSource();

            // 8. 데이터 그리드 뷰 DataSource에 데이터 셋을 할당
            source.DataSource = ds.Tables[0];
            DgvNorthwind.DataSource = source;

            // 9. DB Close
            conn.Close();
        }
    }
}

