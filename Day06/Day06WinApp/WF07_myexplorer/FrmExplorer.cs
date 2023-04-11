using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics; // Process 클래스 객체
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WF07_myexplorer
{
    public partial class FrmExplorer : Form
    {
        public FrmExplorer()
        {
            InitializeComponent();
        }

        private void FrmExplorer_Load(object sender, EventArgs e)
        {
            // 현재 사용자 정보 출력
            WindowsIdentity identity = WindowsIdentity.GetCurrent(); //
            LblPath.Text = identity.Name;

            // 현재 컴퓨터에 존재하는 드라이브 정보 검색, 트리 뷰에 추가
            DriveInfo[] drives = DriveInfo.GetDrives();
            // 트리뷰에 전부 다 출력
            foreach (DriveInfo drive in drives)
            {
                if (drive.DriveType == DriveType.Fixed) // 실제 존재하는 하드 디스크만
                {
                    TreeNode rootNode = new TreeNode(drive.Name);
                    rootNode.ImageIndex = 0;
                    rootNode.SelectedImageIndex = 0;
                    TrvDrive.Nodes.Add(rootNode);
                    FillNodes(rootNode);
                }
            }

            TrvDrive.Nodes[0].Expand();

            // 리스트 뷰 설정
            LsvFolder.View = View.Details;

            LsvFolder.Columns.Add("이름", (LsvFolder.Width / 3), HorizontalAlignment.Left);
            LsvFolder.Columns.Add("날짜", (LsvFolder.Width / 4), HorizontalAlignment.Left);
            LsvFolder.Columns.Add("유형", (LsvFolder.Width / 5), HorizontalAlignment.Left);
            LsvFolder.Columns.Add("크기", (LsvFolder.Width / 6), HorizontalAlignment.Left);
            
            LsvFolder.FullRowSelect = true; // 한 행을 전부 선택

            CboView.SelectedIndex = 0; // View.Details로 초기화
        }
        /// <summary>
        /// 하위 폴더 검색, 트리뷰 확장
        /// </summary>
        /// <param name="curNode"></param>
        private void FillNodes(TreeNode curNode)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(curNode.FullPath);
                // 드라이브 하위 폴더
                foreach (DirectoryInfo item in dir.GetDirectories())
                {
                    TreeNode newNode = new TreeNode(item.Name);
                    newNode.ImageIndex = 1;
                    newNode.SelectedImageIndex = 2;
                    curNode.Nodes.Add(newNode);
                    newNode.Nodes.Add("*");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("트리 오류 발생", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// 트리뷰 노트 확장하기 전 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        // sender 자기자신 객체 내용,  e 이벤트와 관련된 속성포함 (이벤트 발생 했을 때 속성)
        private void TrvDrive_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.Nodes[0].Text == "*")
            {
                e.Node.Nodes.Clear();
                e.Node.ImageIndex = 1; // folder_nomal
                e.Node.SelectedImageIndex = 2; // folder_open
                FillNodes(e.Node); // 하위 폴더를 열어서 검색
            }
        }
        /// <summary>
        /// 트리뷰 접기 전 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TrvDrive_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            e.Node.ImageIndex = 1;
            e.Node.SelectedImageIndex= 1;
        }
        /// <summary>
        /// 트리 노드를 마우스로 클릭 했을 때 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TrvDrive_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            // var path = Path.GetFullPath(e.Node.FullPath);
            SetLsvFolder(e.Node.FullPath);
        }
        /// <summary>
        /// 리스트 뷰에 폴더 내 내용 그리기 메서드
        /// </summary>
        /// <param name="fullPath">선택한 폴더 경로</param>
        private void SetLsvFolder(string fullPath)
        {
            try 
            {
                TxtPath.Text = fullPath; // 현재 경로 입력
                LsvFolder.Items.Clear(); // 기존 리스트 삭제
                DirectoryInfo dir = new DirectoryInfo(fullPath); // 현재 선택한 폴더 경로 받아주기
                int dirCount = 0;
                foreach (DirectoryInfo item in dir.GetDirectories())
                {
                    ListViewItem lvi = new ListViewItem();

                    lvi.ImageIndex = 1;
                    lvi.Text = item.Name; // 0번 인덱스 이름

                    LsvFolder.Items.Add(lvi);
                    LsvFolder.Items[dirCount].SubItems.Add(item.CreationTime.ToString());
                    LsvFolder.Items[dirCount].SubItems.Add("폴더");
                    LsvFolder.Items[dirCount].SubItems.Add( string.Format("{0} files", item.GetFiles().Length.ToString()) );

                    dirCount++;
                } // 폴더 내 디텍토리 리스트뷰에 리스트 업

                // 파일목록 리스트업
                FileInfo[] files = dir.GetFiles();
                int fileCount = dirCount; // 이전 카운트가 승계 되어야함

                foreach ( FileInfo file in files)
                {
                    ListViewItem lvi = new ListViewItem();

                    lvi.Text = file.Name;
                    lvi.ImageIndex = 4; // 기본적인 아이콘
                    
                    lvi.ImageIndex = SetExtImg(file.Name);
                    LsvFolder.Items.Add(lvi);

                    if (file.LastWriteTime != null)
                    {
                        LsvFolder.Items[fileCount].SubItems.Add(file.LastWriteTime.ToString());
                    }
                    else // 최종 수정 날짜가 없을수도 있음
                    {
                        LsvFolder.Items[fileCount].SubItems.Add(file.CreationTime.ToString());
                    }
                    LsvFolder.Items[fileCount].SubItems.Add(file.Attributes.ToString());
                    LsvFolder.Items[fileCount].SubItems.Add(file.Length.ToString());

                    fileCount++;
                }
            } 
            catch
            {
                MessageBox.Show("리스트뷰 오류 발생", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// 확장자에 따라 아이콘 변경
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private int SetExtImg(string name)
        {
            // 파일 정보가 만들어짐
            FileInfo fInfo = new FileInfo(name);
            string ext = fInfo.Extension; // 파일 확장자를 가지고 옴

            var exVal = 0;

            switch (ext)
            {
                case ".exe": // 실행파일이면
                    exVal = 3; break; // exe 아이콘
                case ".png":
                case ".jpg":
                case ".gif":
                    exVal = 5; break;
                default:
                    exVal = 4; break;
            }
            return exVal;
        }

        /// <summary>
        /// 리스트 뷰 보기 변경 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CboView_SelectedIndexChanged(object sender, EventArgs e)
        {
            //0 View.Details
            //1 View.SmallIcon
            //2 View.LargeIcon
            //3 View.List
            //4 View.Tile   
            switch (CboView.SelectedIndex)
            {
                case 0: // Details
                    LsvFolder.View = View.Details; break;
                case 1: // SmallIcon
                    LsvFolder.View = View.SmallIcon; break;
                case 2: // LargeIcon
                    LsvFolder.View= View.LargeIcon; break;
                case 3: // List
                    LsvFolder.View= View.List; break;
                case 4: // Tile
                    LsvFolder.View= View.Tile; break;
                // default: // 위와 같은 경우면 default는 없어도 된다
                //     LsvFolder.View = View.Details; break;
                // 0부터 4밖에 나올 게 없기 때문에
            }
        }
        /// <summary>
        /// 디렉토리 경로를 바꿨을 때 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtPath_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) // KeyBoard 엔터키를 누르면
            {
                try
                {
                    SetLsvFolder(TxtPath.Text);
                }
                catch 
                {
                    MessageBox.Show("경로를 찾을 수 없습니다. 맞춤법을 확인하고 다시 시도하십시오.",
                                    "나의 탐색기", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        /// <summary>
        /// 리스트 뷰 파일 더블클릭 처리 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LsvFolder_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (LsvFolder.SelectedItems.Count == 1)
            {
                string processPath = TxtPath.Text + "\\" + LsvFolder.SelectedItems[0].Text;
                
                Process.Start(processPath);
            }
        }
    }
}   