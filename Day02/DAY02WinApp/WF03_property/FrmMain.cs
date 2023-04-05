using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WF03_property
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
            // 생성자에는 되도록 설정 부분을 넣지마라
            // 위에다가 아무것도 넣지 마라 어지간하면 건들지마라
            // InitializeComponent 앞에다 쓰면 안된다.
            // Form_Load() 이벤트 핸들러에 작성할 것
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            GbxMain.Text = "컨트롤 학습";
            var fonts = FontFamily.Families.ToList(); // 내 OS에 있는 폰트이름 가져오기
            foreach ( var font in fonts )
            {
                CboFontFamilly.Items.Add(font.Name);
            }

            NudFontSize.Minimum = 5;
            NudFontSize.Maximum = 40;
            // 글자 크기 최소값, 최대값 지정
            TxtResult.Text = "Hello, WinForms!!";
            NudFontSize.Value = 9; // 글자체 크기를 9로 지정
        }
        /// <summary>
        /// 글자 스타일, 크기, 글자체를 변경해주는 메서드 
        /// </summary>
        #region < 글자 속성 변경 >
        private void ChangeFontStyle()
        {
            if (CboFontFamilly.SelectedIndex < 0) return;

            FontStyle style = FontStyle.Regular; // 기본
            if (ChkBold.Checked == true)
            {
                style |= FontStyle.Bold; // 비트 연산 or
            }
            if (ChkItalic.Checked == true)
            {
                style |= FontStyle.Italic;
            }

            decimal fontSize = NudFontSize.Value;

            TxtResult.Font = new Font((string)CboFontFamilly.SelectedItem, (float)fontSize, style);
        }

        private void CboFontFamilly_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeFontStyle();
        }

        private void ChkBold_CheckedChanged(object sender, EventArgs e)
        {
            ChangeFontStyle();
        }

        private void ChkItalic_CheckedChanged(object sender, EventArgs e)
        {
            ChangeFontStyle();
        }

        private void NudFontSize_ValueChanged(object sender, EventArgs e)
        {
            ChangeFontStyle();
        }
        #endregion
    }
}
