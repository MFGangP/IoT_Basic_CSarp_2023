using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WF05_login
{

    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
            LabSuccess.Text = "환영합니다.";
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            if (TxtID.Text.Length != 0 && TxtPassWord.Text.Length != 0)
            {
                if (TxtID.Text == "abcd" && TxtPassWord.Text == "1234")
                {
                    LabSuccess.Text = "로그인 성공";
                    // KeyPress 이벤트 넣으면
                }
                else
                {
                    LabSuccess.Text = "로그인 실패";
                }
            }
            else
            {
                LabSuccess.Text = "아이디 비밀번호를 입력해주세요.";
            }
        }

        private void TxtPassWord_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                BtnLogin_Click (sender, e);
            }
        }
    }
}
