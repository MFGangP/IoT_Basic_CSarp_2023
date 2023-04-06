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
        Random rnd = new Random();

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
            if (CboFontFamilly.SelectedIndex < 0)
            {
                // 디폴트는 나눔 고딕
                CboFontFamilly.SelectedIndex = 276; // 학교 컴퓨터 - 나눔 고딕 번호
            }
            
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
        void ChangeIndent()
        {
            if (RdoNormal.Checked)  // 라디오 버튼 추가 이벤트
            {
                TxtResult.Text = TxtResult.Text.Trim();
            }
            else if (RdoIndent.Checked)
            {
                TxtResult.Text = "    " + TxtResult.Text;
            }
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

        private void TrbDummy_Scroll(object sender, EventArgs e)
        {
            PgbDummy.Value = TrbDummy.Value;
        }

        private void BtnModal_Click(object sender, EventArgs e)
        {
            Form frm = new Form()
            {
                Text = "Modal Form", 
                Width = 300,
                Height = 200,
                Left = 10,
                Top = 20,
                BackColor = Color.Red,
                StartPosition = FormStartPosition.CenterParent,
            };
            // 자식창이 떠있는 동안 부모 창을 못눌리게 하는 기능
            frm.ShowDialog(); // 자식 창 띄우기
        }

        private void BtnModaless_Click(object sender, EventArgs e)
        {
            Form frm = new Form() 
            { 
                Text = "Modaless Form",
                Width = 300,
                Height = 200,
                StartPosition = FormStartPosition.CenterParent, // Modaless는 CenterParert 안먹힘
                BackColor = Color.GreenYellow,

            };
            // 자식창이 떠있어도 부모 창을 컨트롤 가능
            frm.Show();
        }

        private void BtnMsgBox_Click(object sender, EventArgs e)
        {
            // MessageBox는 기본이 Modal
            // MessageBox.Show(TxtResult.Text); // 가장 기본
            // MessageBox.Show(TxtResult.Text, caption: "메시지창"); // 캡션 사용 방법
            // MessageBox.Show(TxtResult.Text, caption: "메시지창", MessageBoxButtons.AbortRetryIgnore); // 버튼 추가
            // MessageBox.Show(TxtResult.Text, caption: "메시지창", 
            //             MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.);// 아이콘 추가
            // 여기까지는 알고 있어야함
            MessageBox.Show(TxtResult.Text, caption: "메시지창",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning,
                        MessageBoxDefaultButton.Button2);// 버튼 기본 포커스 위치 변경
            // MessageBox.Show(TxtResult.Text, caption: "메시지창",
            //             MessageBoxButtons.YesNo, MessageBoxIcon.Warning,
            //             MessageBoxDefaultButton.Button2, MessageBoxOptions.RightAlign); // 글자 오른쪽 정렬
        }

        private void BtnAddRoot_Click(object sender, EventArgs e)
        {
            TrvDummy.Nodes.Add(rnd.Next(45).ToString());
            TreeToList();
        }

        private void BtnAddChild_Click(object sender, EventArgs e)
        {
            if(TrvDummy.SelectedNode != null)
            {
                TrvDummy.SelectedNode.Nodes.Add(rnd.Next(50, 100).ToString());
                TrvDummy.SelectedNode.Expand(); // 트리 노드 하위 항목 펼쳐주기. 반대 = Collapse
                                                // TrvDummy.SelectedNode.Collapse
                TreeToList();
            }
        }

        void TreeToList()
        {
            LsvDummy.Items.Clear(); // 대부분의 리스트뷰, 트리뷰 모든 아이템을 제거하고 초기화 해주는 메서드
            foreach (TreeNode item in TrvDummy.Nodes)
            {
                TreeToList(item);
            }
        }

        private void TreeToList(TreeNode item)
        {
            LsvDummy.Items.Add(
                new ListViewItem (new[] { item.Text, item.FullPath.ToString() }));

            foreach (TreeNode node in item.Nodes)
            {
                TreeToList(node); // 재귀호출
            }
        }

        private void RdoNormal_CheckedChanged(object sender, EventArgs e)
        {
            ChangeIndent();
        }

        private void RdoIndent_CheckedChanged(object sender, EventArgs e)
        {
            ChangeIndent();
        }

        private void BtnLoad_Click(object sender, EventArgs e)
        {
            PcbDummy.Image = Bitmap.FromFile("cat.png");
        }

        private void PcbDummy_Click(object sender, EventArgs e)
        {
            if (PcbDummy.SizeMode == PictureBoxSizeMode.Normal)
            {
                PcbDummy.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            else
            {
                PcbDummy.SizeMode = PictureBoxSizeMode.Normal;
            }
        }
    }
}
