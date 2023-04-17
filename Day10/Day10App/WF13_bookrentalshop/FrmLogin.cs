using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using WF13_bookrentalshop.Helpers;

namespace WF13_bookrentalshop
{
    public partial class FrmLogin : Form
    {
        private bool isLogined = false; // 로그인 성공했는지 물어보는 전체 변수
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            isLogined = LoginProcess(); // 로그인을 성공해야만 true가 됌

            if (isLogined) this.Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            // Application.Exit(); 얘 한번씩 작동 안 할 때가 있음
            Environment.Exit(0); // 가장 완벽하게 프로그램 종료
        }

        // 이게 없으면 X 버튼이나 Alt + F4로 했을 때 메인폼이 나타남
        private void FrmLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (isLogined != true)
            {
                // 로그인 안되었을 때 창을 닫으면 프로그램 모두 종료
                // Alt + f4로 꺼져야 하는데 안꺼지는 일을 이걸로 예방하는 것 예외 방지
                Environment.Exit(0);
            }
        }

        private void TxtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) // 엔터
            {
                BtnLogin_Click(sender, e); // 버튼 클릭 이벤트 핸들러 호출
            }
        }
        // 핵심
        // DB userTbl에서 정보 확인 후 로그인 처리
        private bool LoginProcess()
        {
            // Validation check
            // DB에서 notnull 들어가는 모든 요소에 다 해줘야하는 것 
            if (string.IsNullOrEmpty(TxtUserId.Text))
            {
                MessageBox.Show("유저 아이디를 입력하세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (string.IsNullOrEmpty(TxtPassword.Text))
            {
                MessageBox.Show("패스워드를 입력하세요.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            string struserId = "";
            string strPassword = "";

            try
            {
                // DB처리
                /*
                MySqlConnection conn = new MySqlConnection("");
                conn.Open();
                conn.Close();
                */
                // using 문 쓰면 Close 안해도 알아서 닫아줌.

                // 따로 만들면 된다.
                // string connectionString = "Server=localhost;Port=3306;Database=Bookrentalshop;Uid=root;Pwd=12345;";
                using (MySqlConnection conn = new MySqlConnection(Helpers.Commons.ConnString))
                {
                    conn.Open();

                    #region < DB 쿼리를 위한 구성 >
                    // 값을 치환해서 쿼리문에 대입
                    string selQuery = @"SELECT userId
                                             , password
                                          FROM usertbl
                                         WHERE userId = @userId
                                           AND password = @password;";
                    MySqlCommand selCmd = new MySqlCommand(selQuery, conn);
                    // @userId, @password 파라미터 할당
                    MySqlParameter prmuserId = new MySqlParameter("@userId", TxtUserId.Text);
                    MySqlParameter prmPassword = new MySqlParameter("@password", TxtPassword.Text);
                    // selCmd의 파라미터에 더해줘야된다.
                    selCmd.Parameters.Add(prmuserId);
                    selCmd.Parameters.Add(prmPassword);
                    #endregion
                    // 실행한 다음 userId, password 받아온다. DB에서 'manager' , '12345' 값으로 실행해주는 함수
                    MySqlDataReader reader = selCmd.ExecuteReader();
                    if (reader.Read())
                    {
                        // 널이 아니라면 userId 그대로 넣고 널이면 "-" 대입
                        struserId = reader["userId"] != null ? reader["userId"].ToString() : "-";
                        strPassword = reader["password"] != null ? reader["password"].ToString() : "-";
                        Commons.LoginID = struserId; // 프로그램 전체에서 사용
                        return true;
                    }
                    else
                    {
                        MessageBox.Show($"로그인 정보가 없습니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Commons.LoginID = string.Empty; // 비상용
                        return false;
                    }
                }
                //MessageBox.Show($"{struserId} / {strPassword}"); // 디버깅용
            }
            catch (Exception ex)
            {
                MessageBox.Show($"비정상적 오류 {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void TxtuserId_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 유저 아이디 텍스트 박스에서 엔터를 치면 패스워드 텍스트 박스로 포커스 이동
            // 엔터 눌렸을 때 다음 칸으로 넘어가게 해주는 기능
            if (e.KeyChar == 13)
            {
                TxtPassword.Focus();
            }
        }
    }
}
