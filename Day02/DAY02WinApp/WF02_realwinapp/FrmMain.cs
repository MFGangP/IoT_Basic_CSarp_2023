using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WF02_realwinapp
{
    public partial class FrmMain : Form // partial 빠지면 큰일난다. 같은 클래스라는걸 알려줌
    {
        public FrmMain()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 버튼 OK 클릭 이벤트에 대한 처리 메서드 만듦
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOK_Click(object sender, EventArgs e)
        {
            MessageBox.Show("버튼클릭!!!", "클릭", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            BtnOK.Text ="클릭됌!";
            return; // void는 이렇게 되어있다.
        }

        private void BtnOK_MouseHover(object sender, EventArgs e)
        {
            MessageBox.Show("마우스만 올려도 이벤트가 발생해요!");
        }
    }
}
