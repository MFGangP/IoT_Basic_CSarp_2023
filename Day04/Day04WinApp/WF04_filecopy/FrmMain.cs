using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.WebRequestMethods;

namespace WF04_filecopy
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void BtnFindSource_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            var result = dialog.ShowDialog(); // 모달창
            if (result == DialogResult.OK)
            {
                TxtSource.Text = dialog.FileName;
            }
        }

        private void BtnFindTarget_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            var result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                TxtTarget.Text = dialog.FileName;
            }
        }
        // 일반적인 동기식 파일 복사
        private void BtnSyncCopy_Click(object sender, EventArgs e)
        {
            long totalCopied = CopySync(TxtSource.Text, TxtTarget.Text);
        }

        private long CopySync(string fromFile, string toFile)
        {
            BtnAsyncCopy.Enabled = false;   // 비동기 버튼 일시 비활성화
            long totalCopied = 0;

            using (FileStream fromStream = new FileStream(fromFile, FileMode.Open))
            { // 원본파일은 열고
                using (FileStream toStream = new FileStream(toFile, FileMode.Create)) 
                { // 타겟파일은 생성
                    // 이거도 다 정해져있어서 이렇게 바로 생성된다.
                    byte[] buffer = new byte[1024 * 1024]; // 1MByte 버퍼
                    int nRead = 0;
                    while ((nRead = fromStream.Read(buffer, 0, buffer.Length)) != 0) 
                    {
                        toStream.Write(buffer, 0, nRead); // 버퍼를 0부터 읽은 갯수의 길이만큼
                        totalCopied += nRead;

                        // 프로그래스 바에 표시
                        PgbCopy.Value = (int)((double)totalCopied / (double)fromStream.Length) * PgbCopy.Maximum;
                    }
                }
            }
            BtnAsyncCopy.Enabled = true;
            return totalCopied;
        }


        // 비동기는 호출할 때 await 사용, 구현할 때는 aysnc 사용
        // 비동기용 메서드가 따로있고 부를때 await, 선언부에 Async 필요
        // 반환 형식 앞에 Task
        private async Task<long> CopyAsync(string fromFile, string toFile)
        {
            BtnSyncCopy.Enabled = false; // 동기복사 버튼 비활성화
            long totalCopied = 0;

            using (FileStream fromStream = new FileStream(fromFile, FileMode.Open))
            { // 원본파일은 열고
                using (FileStream toStream = new FileStream(toFile, FileMode.Create))
                { // 타겟파일은 생성
                    // 이거도 다 정해져있어서 이렇게 바로 생성된다.
                    byte[] buffer = new byte[1024 * 1024]; // 1MByte 버퍼
                    int nRead = 0;
                    while ((nRead = await fromStream.ReadAsync(buffer, 0, buffer.Length)) != 0)
                    {
                        await toStream.WriteAsync(buffer, 0, nRead); // 버퍼를 0부터 읽은 갯수의 길이만큼
                        totalCopied += nRead;

                        // 프로그래스 바에 표시
                        PgbCopy.Value = (int)((double)totalCopied / (double)fromStream.Length) * PgbCopy.Maximum;
                    }
                }
            }

            BtnSyncCopy.Enabled = true;
            return totalCopied;
        }

        private async void BtnAsyncCopy_Click(object sender, EventArgs e)
        {
            long totalCopied = await CopyAsync(TxtSource.Text, TxtTarget.Text);
        }

    }
}
