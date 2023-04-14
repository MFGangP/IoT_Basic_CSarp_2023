using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WF14_winforms_thread
{
    public partial class FrmMain : Form
    {
        int number = 0;
        int percent = 0;
        public FrmMain()
        {
            InitializeComponent();
        }
        private void FrmMain_Load(object sender, EventArgs e)
        {
            // 속성에서 바꿔도 되고 여기서 바꿔도 된다.
            Worker.WorkerSupportsCancellation = true;
            Worker.WorkerReportsProgress = true;
        }

        // 백그라운드로 일을 진행 // Thread.Start()
        // 백그라운드 워커가 sender 값을 보낸 쪽
        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            // 값을 하나 더 받음.
            // 이건 얕은 복사라서 앞에 있는 Worker 주소를 참조하고 있음. 취소하면 같이 취소 됌
            BackgroundWorker worker = sender as BackgroundWorker; // 인자 값으로 BackgroundWorker 생성
            e.Result = Fibonacci((int)e.Argument, worker, e); // 피보나치 계산 메서드 만들 것

        }

        private long Fibonacci(int argument, BackgroundWorker worker, DoWorkEventArgs e)
        {
            // arg는 0~91 사이 91보다 크면 long 오버플로우 발생
            if (argument < 0 || argument > 91)
            {
                throw new Exception("오류 0~91사이 입력");
            }

            long result = 0;

            if(worker.CancellationPending == true)
            {
                e.Cancel = true;
            }
            else
            {
                if (argument < 2)
                {
                    result = 1;
                }
                else
                {
                    // argument 만 계산에 필요한 매개변수, worker, e는 중간에 취소할 때 필요한 BackgroudWorker의 복사본
                    result = Fibonacci(argument - 1, worker, e) + Fibonacci(argument - 2, worker, e);
                    // 여기서 프로그래스 바 표시가 진행되야 하는데 다 끝나고 넘어가서 안된거다.
                }

                // 진행 상황을 프로그래스바에 표시
                int percentComplete = (int)(argument / number * 100);
                if (percentComplete > percent)
                {
                    percent = percentComplete;
                    Worker.ReportProgress(percentComplete); // ProgressChanged 이벤트 발생
                }
            }
            return result;
        }

        // 백그라운드 스레드 테스크 종료한 뒤 처리
        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }
            else if (e.Cancelled)
            {
                LblResult.Text = "취소됨";
            }
            else
            {
                LblResult.Text = e.Result.ToString();
            }

            TxtNumber.Text = 0.ToString();
            BtnStart.Enabled = true;
            BtnCancel.Enabled = false;
        }

        // 백그라운드 스레드 진행중 프로그래스 표시
        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            PgbCalac.Value = e.ProgressPercentage; // 진행 상황 표시
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            // Button button = sender as Button; // 버튼 복사
            BtnStart.Enabled = false; // 시작 버튼은 종료시 까지 못누르게 만들어야 됌
            BtnCancel.Enabled = true;

            number = int.Parse(TxtNumber.Text);

            percent = 0;
            PgbCalac.Value = percent; // 프로그래스 바 0으로 초기화

            Worker.RunWorkerAsync(number); // 비동기로 진행
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Worker.CancelAsync(); // 비동기로 취소 시키라고 요청
            BtnCancel.Enabled=false;
            BtnStart.Enabled=true;
        }
    }
}
