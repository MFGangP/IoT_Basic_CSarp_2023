using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WF13_bookrentalshop
{
    public partial class FrmGenre : Form
    {
        bool isNew = false; // false(업데이트) / true(INSERT)
        public FrmGenre()
        {
            InitializeComponent();
        }

        private void FrmGenre_Load(object sender, EventArgs e)
        {
            isNew = true; // 신규부터 시작
            // DB divtbl 데이터를 조회 한 다음 DgvResult 그리드뷰에 나타냄
            RefreshData();
        }

        private void RefreshData()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(Helpers.Commons.ConnString))
                {
                    // 추가 편집 삭제 사용 해제해야 수정이 안된다.
                    if (conn.State == ConnectionState.Closed) { conn.Open(); } // DB연결을 좀 더 정확하게 해주는 부분

                    // 쿼리 작성
                    var selQuery = @"SELECT Division
	                                      , Names
                                       FROM divtbl";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(selQuery, conn); // 데이터 통채로 가져올땐 어댑터 쓰면 된다
                    DataSet ds = new DataSet(); // 
                    adapter.Fill(ds, "divtbl"); // divtbl으로 DataSet 접근가능

                    DgvResult.DataSource = ds.Tables[0];
                    // DgvResult.DataSource = ds;
                    // DgvResult.DataMember = "divtbl";

                    DgvResult.Columns[0].HeaderText = "장르코드";
                    DgvResult.Columns[1].HeaderText = "장르명";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"비정상적 오류 {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnNew_Click(object sender, EventArgs e)
        {
            ClearInputs();
        }

        private void ClearInputs()
        {
            TxtDivision.Text = TxtNames.Text = string.Empty;
            TxtDivision.ReadOnly = false; // 신규 일때는 입력이 가능해야한다.
            TxtDivision.Focus();
            isNew = true; // 신규
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (CheckValidation() != true) return;

            SaveData(); // 데이터 저장, 수정
            RefreshData(); // 데이터 재조회
            ClearInputs(); // 입력창 클리어
        }

        // 입력 검증 - 나중에 많은 일을 해야한다.
        private bool CheckValidation()
        {
            var result = true;
            var errorMsg = string.Empty;

            if (string.IsNullOrEmpty(TxtDivision.Text))
            {
                // 입력검증 (Validation Check)
                result = false;
                errorMsg += "● 장르 코드를 입력하세요\r\n";
            }

            if (string.IsNullOrEmpty(TxtNames.Text))
            {
                // 입력검증 (Validation Check)
                result = false;
                errorMsg += "● 장르명을 입력하세요\r\n";
            }

            if (result == false)
            {
                // 입력검증 (Validation Check)
                MessageBox.Show(errorMsg, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return result;
            }

            else
            {
              return result;
            }
        }

        // isNew = true INSERT / false UPDATE
        private void SaveData()
        {
            // INSERT부터 시작
            try
            {
                using (MySqlConnection conn = new MySqlConnection(Helpers.Commons.ConnString))
                {
                    if (conn.State == ConnectionState.Closed) conn.Open();

                    var query = "";
                    

                    if (isNew == true)
                    {
                        query = @"INSERT INTO divtbl
	                              VALUES (@Division
                                       , @Names)";
                    }
                    else
                    {
                        query = @"UPDATE divtbl
                                     SET Names = @Names
                                   WHERE Division = @Division";
                    }
                    
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    // 이름을 통일시켜야 안틀린다.
                    MySqlParameter prmDivision = new MySqlParameter("@Division", TxtDivision.Text);
                    MySqlParameter prmNames = new MySqlParameter("@Names", TxtNames.Text);
                    cmd.Parameters.Add(prmDivision);
                    cmd.Parameters.Add(prmNames);
                    // 데이터를 넣을 때는 
                    var result = cmd.ExecuteNonQuery(); // INSERT, UPDATE, DELETE
                                                        // 결과를 돌려받는다. 1, 2, 3

                    if (result != 0)
                    {
                        // 저장 성공
                        MessageBox.Show($"성공적으로 저장 되었습니다", "저장", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                    {
                        // 저장 실패
                        MessageBox.Show($"저장에 실패하였습니다.", "저장", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"비정상적 오류 {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (isNew == true) // 신규 일 때는 삭제 누르면 안된다.
            {
                MessageBox.Show($"삭제 할 데이터를 선택하세요.", "저장", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // 삭제 여부를 물을 때 아니요를 누르면 삭제 진행 취소
            if (MessageBox.Show(this, "삭제하시겠습니까?", "삭제", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            
            // SaveData()에 있는 로직 복사 -> 수정
            // Yes를 누르면 게속 진행
            try
            {
                using (MySqlConnection conn = new MySqlConnection(Helpers.Commons.ConnString))
                {
                    if (conn.State == ConnectionState.Closed) conn.Open();

                    var query = "";

                    query = @"Delete FROM divtbl
                                WHERE Division = @Division";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    // 이름을 통일시켜야 안틀린다.
                    MySqlParameter prmDivision = new MySqlParameter("@Division", TxtDivision.Text);
                    cmd.Parameters.Add(prmDivision);

                    // 데이터를 넣을 때는 
                    var result = cmd.ExecuteNonQuery(); // INSERT, UPDATE, DELETE
                                                        // 결과를 돌려받는다. 1, 2, 3

                    if (result != 0)
                    {
                        // 삭제 성공
                        MessageBox.Show($"성공적으로 삭제 되었습니다", "삭제", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                    {
                        // 삭제 실패
                        MessageBox.Show($"삭제 실패하였습니다.", "삭제", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show($"비정상적 오류 {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            RefreshData();
            ClearInputs();
        }

        // sender는 버튼 클릭 그 자체, args (e)를 더 많이 씀
        // 그리드 뷰 클릭하면 발생하는 이벤트
        private void DgvResult_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex > -1) // 아무것도 선택 안했을 때 -1
            {
                // 모든 인덱스는 0부터 시작
                var selData = DgvResult.Rows[e.RowIndex];
                // MessageBox.Show($"{selData}");
                // Debug.WriteLine(selData); 편하게 값 보는법
                Debug.WriteLine(selData.Cells[0].Value);
                Debug.WriteLine(selData.Cells[1].Value);
                // TxtDivision.Text = (string)(selData.Cells[0].Value);
                // TxtNames.Text = (string)(selData.Cells[1].Value);
                TxtDivision.Text = selData.Cells[0].Value.ToString();
                TxtNames.Text = selData.Cells[1].Value.ToString();
                TxtDivision.ReadOnly = true; // PK는 수정하면 안된다.


                isNew = false; // 수정
            }
        }
    }
}
