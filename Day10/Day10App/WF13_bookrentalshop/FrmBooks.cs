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
using WF13_bookrentalshop.Helpers;
// 같은 프로젝트라도 네임 스페이스가 다르면 이렇게 들어간다.
namespace WF13_bookrentalshop
{
    public partial class FrmBooks : Form
    {
        #region < 멤버 변수 >
        bool isNew = false; // false(업데이트) / true(INSERT)
        #endregion

        #region < 생성자 >
        public FrmBooks()
        {
            InitializeComponent();
        }
        #endregion

        #region < 커스텀 메서드 >
        private bool CheckValidation()
        {   
            // 입력 검증 - 나중에 많은 일을 해야한다.
            var result = true;
            var errorMsg = string.Empty;

            if (string.IsNullOrEmpty(TxtBookIdx.Text))
            {
                // 입력검증 (Validation Check)
                result = false;
                errorMsg += "● 장르 코드를 입력하세요\r\n";
            }

            if (string.IsNullOrEmpty(TxtAuthor.Text))
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
        private void RefreshData()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(Helpers.Commons.ConnString))
                {
                    // 추가 편집 삭제 사용 해제해야 수정이 안된다.
                    if (conn.State == ConnectionState.Closed) { conn.Open(); } // DB연결을 좀 더 정확하게 해주는 부분

                    // 쿼리 작성
                    var selQuery = @"SELECT b.bookIdx,
                                            b.Author,
                                            b.Division,
                                            d.Names AS DivNames,
                                            b.Names,
                                            b.ReleaseDate,
                                            b.ISBN,
                                            b.Price
                                       FROM bookstbl AS b
                                      INNER JOIN divtbl AS d
                                         ON b.Division = d.Division
                                      ORDER BY b.bookIdx ASC";// 정렬 때문에 추가
                    MySqlDataAdapter adapter = new MySqlDataAdapter(selQuery, conn); // 데이터 통채로 가져올땐 어댑터 쓰면 된다
                    DataSet ds = new DataSet(); // 
                    adapter.Fill(ds, "bookstbl"); // bookstbl으로 DataSet 접근가능

                    DgvResult.DataSource = ds.Tables[0];

                    DgvResult.Columns[0].HeaderText = "번호";
                    DgvResult.Columns[1].HeaderText = "저자명";
                    DgvResult.Columns[2].HeaderText = "책장르";
                    DgvResult.Columns[3].HeaderText = "책장르";
                    DgvResult.Columns[4].HeaderText = "책제목";
                    DgvResult.Columns[5].HeaderText = "출판일자";
                    DgvResult.Columns[6].HeaderText = "ISBN";
                    DgvResult.Columns[7].HeaderText = "책가격";

                    DgvResult.Columns[0].Width = 35;
                    DgvResult.Columns[2].Visible = false; // 코드 영역은 보일 필요 없음
                    DgvResult.Columns[3].Width = 78;
                    DgvResult.Columns[4].Width = 170;
                    DgvResult.Columns[5].Width = 78;
                    DgvResult.Columns[7].Width = 78;


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"비정상적 오류 {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ClearInputs()
        {
            TxtBookIdx.Text = TxtAuthor.Text = string.Empty;
            TxtBookIdx.ReadOnly = false; // 신규 일때는 입력이 가능해야한다.
            TxtBookIdx.Focus();
            isNew = true; // 신규
        }
        private void SaveData()
        {
            // isNew = true INSERT / false UPDATE
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
                    MySqlParameter prmDivision = new MySqlParameter("@Division", TxtBookIdx.Text);
                    MySqlParameter prmNames = new MySqlParameter("@Names", TxtAuthor.Text);
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
        private void DeleteData()
        {
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
                    MySqlParameter prmDivision = new MySqlParameter("@Division", TxtBookIdx.Text);
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
        }
        #endregion

        #region < 이벤트 핸들러 >
        // 이벤트 핸들러 이름 굳이 바꾸지마라 
        private void FrmGenre_Load(object sender, EventArgs e)
        {
            isNew = true; // 신규부터 시작
            // DB divtbl 데이터를 조회 한 다음 DgvResult 그리드뷰에 나타냄
            RefreshData();
        }
        private void BtnNew_Click(object sender, EventArgs e)
        {
            ClearInputs();
        }
        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (CheckValidation() != true) return;

            SaveData(); // 데이터 저장, 수정
            RefreshData(); // 데이터 재조회
            ClearInputs(); // 입력창 클리어
        }
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (isNew == true) // 신규 일 때는 삭제 누르면 안된다.
            {
                MessageBox.Show($"삭제 할 데이터를 선택하세요.", "저장", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 자식이 부모를 참조하고있으니까 삭제되면 안된다.
            // FK 제약조건으로 지울 수 없는 데이터인지 먼저 확인
            using (MySqlConnection conn = new MySqlConnection(Commons.ConnString))
            {
                if (conn.State == ConnectionState.Closed) conn.Open();
                // 해당 하는 값이 얼마나 사용됐는지 알아보는 쿼리문
                string strQuery = "SELECT COUNT(*) FROM bookstbl WHERE Division = @Division";
            
                MySqlCommand chkCmd = new MySqlCommand(strQuery, conn);
                MySqlParameter prmDivision = new MySqlParameter("@Division", TxtBookIdx.Text);
                chkCmd.Parameters.Add(prmDivision);

                // 컬럼 여러 개 일 때 Leaders, 컬럼 하나만 있을 때는 Scalar
                // 비동기도 있다.
                var result = chkCmd.ExecuteScalar();
                
                if (result.ToString() != "0")
                {
                    MessageBox.Show("이미 사용중인 코드 입니다.", "삭제", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

            }

            // 삭제 여부를 물을 때 아니요를 누르면 삭제 진행 취소
            if (MessageBox.Show(this, "삭제하시겠습니까?", "삭제", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            // SaveData()에 있는 로직 복사 -> 수정
            DeleteData();
            RefreshData();
            ClearInputs();
        }
        private void DgvResult_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // sender는 버튼 클릭 그 자체, args (e)를 더 많이 씀
            // 그리드 뷰 클릭하면 발생하는 이벤트
            if (e.RowIndex > -1) // 아무것도 선택 안했을 때 -1
            {
                // 모든 인덱스는 0부터 시작
                var selData = DgvResult.Rows[e.RowIndex];
                // MessageBox.Show($"{selData}");
                // Debug.WriteLine(selData); 편하게 값 보는법
                Debug.WriteLine(selData.Cells[0].Value);
                Debug.WriteLine(selData.Cells[1].Value);
                // TxtDivision.Text = (string)(selData.Cells[0].Value);
                // TxtNames.Text = (string)(selData.Cells[1].Value);
                TxtBookIdx.Text = selData.Cells[0].Value.ToString();
                TxtAuthor.Text = selData.Cells[1].Value.ToString();
                TxtBookIdx.ReadOnly = true; // PK는 수정하면 안된다.


                isNew = false; // 수정
            }
        }
        #endregion
    }
}