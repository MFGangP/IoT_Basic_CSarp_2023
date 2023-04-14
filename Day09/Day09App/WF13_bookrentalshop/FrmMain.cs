using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WF13_bookrentalshop
{
    public partial class FrmMain : Form
    {
        #region < 각화면 폼 >
        FrmGenre frmGenre = null; // 책장르관리 객체 변수
        #endregion
        #region < 생성자 >
        private int childFormNumber = 0;
        public FrmMain()
        {
            InitializeComponent();
        }
        #endregion

        #region < 이벤트 핸들러 영역 >
        private void FrmMain_Load(object sender, EventArgs e)
        {
            FrmLogin frm = new FrmLogin();
            frm.StartPosition = FormStartPosition.CenterScreen; 
            frm.ShowDialog();
        }

        // 실수로 X눌려서 프로그램 끌 때. 창 끄기 전에 일어나야하는 이벤트라서 여기 넣는다.
        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("종료하시겠습니까?", "확인", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                e.Cancel = false;
                Environment.Exit(0);
            }
            else
            {
                e.Cancel = true;
            }
        }
        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit(); // 전체 프로그램 종료!
        }

        private void MniGenre_Click(object sender, EventArgs e)
        {
            //FrmGenre frm = new FrmGenre();
            //frm.TopLevel = false;
            //this.Controls.Add(frm);
            //frm.Show();
            frmGenre = ShowActiveForm(frmGenre, typeof(FrmGenre)) as FrmGenre;
        }

        private void MniBookInfo_Click(object sender, EventArgs e)
        {

        }

        private void MniMember_Click(object sender, EventArgs e)
        {

        }

        private void MniRental_Click(object sender, EventArgs e)
        {

        }
        #endregion
       
        private Form ShowActiveForm(Form form, Type type)
        {
            if (form == null) // 한번도 자식 창을 안열었으면 새로 만들어라
            {
                form = (Form)Activator.CreateInstance(type); // 리플렉션으로 타입에 맞는 창을 새로 생성
                form.MdiParent = this; // FrmMain이 MDI 부모라는 의미
                form.WindowState = FormWindowState.Normal;
                form.Show();
            }
            else
            {
                // 한번 닫혔다.
                if (form.IsDisposed) // 창이 완전히 삭제가 되지않고 남아있으면
                {
                    form = (Form)Activator.CreateInstance(type); // 리플렉션으로 타입에 맞는 창을 새로 생성
                    form.MdiParent = this; // FrmMain이 MDI 부모라는 의미
                    form.WindowState = FormWindowState.Normal;
                    form.Show(); // 다시 열어라
                }
                // 창이 열려있으면
                else
                {
                    form.Activate(); // 화면이 있으면 그 화면을 활성화
                }
            }
            return form;
        }
    }
}
