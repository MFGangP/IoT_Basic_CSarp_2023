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

            if (string.IsNullOrEmpty(TxtAuthor.Text))
            {
                // 입력검증 (Validation Check)
                result = false;
                errorMsg += "● 저자명을 입력하세요\r\n";
            }
            if (CboDivision.SelectedIndex < 0)
            {
                // 입력검증 (Validation Check)
                result = false;
                errorMsg += "● 장르를 선택하세요\r\n";
            }
            if (string.IsNullOrEmpty(TxtNames.Text))
            {
                // 입력검증 (Validation Check)
                result = false;
                errorMsg += "● 책제목을 입력하세요\r\n";
            }
            if (DtpReleaseDate.Value == null)
            {
                // 입력검증 (Validation Check)
                result = false;
                errorMsg += "● 출판일자를 입력하세요\r\n";
            }
            // 책 대여점이니까 나머지 요소는 필요없다.
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
                    // 데이터 그리드 뷰 컬럼 헤더 제목
                    DgvResult.Columns[0].HeaderText = "번호";
                    DgvResult.Columns[1].HeaderText = "저자명";
                    DgvResult.Columns[2].HeaderText = "책장르";
                    DgvResult.Columns[3].HeaderText = "책장르";
                    DgvResult.Columns[4].HeaderText = "책제목";
                    DgvResult.Columns[5].HeaderText = "출판일자";
                    DgvResult.Columns[6].HeaderText = "ISBN";
                    DgvResult.Columns[7].HeaderText = "책가격";
                    // 컬럼 넓이 또는 보이기
                    DgvResult.Columns[0].Width = 35;
                    DgvResult.Columns[2].Visible = false; // 코드 영역은 보일 필요 없음
                    DgvResult.Columns[3].Width = 78;
                    DgvResult.Columns[4].Width = 170;
                    DgvResult.Columns[5].Width = 78;
                    DgvResult.Columns[7].Width = 78;
                    // 컬럼 정렬
                    DgvResult.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    DgvResult.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    DgvResult.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    
                
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"RefreshData() 비정상적 오류 {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadCboData()
        {
            // try 치고 탭탭
            try
            {
                using (MySqlConnection conn = new MySqlConnection(Commons.ConnString))
                {
                    if (conn.State == ConnectionState.Closed) { conn.Open(); }
                    var query = @"SELECT Division, Names From divtbl";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataReader reader = cmd.ExecuteReader(); // 전체 다 가져와야 하기 때문에
                    var temp = new Dictionary<string, string>();
                    while (reader.Read())
                    {
                        temp.Add(reader[0].ToString(), reader[1].ToString()); // (key)B001,(value)공포/스릴러
                    }

                    // 콤보박스에 할당 // 데이터 베이스 값 저장하는 클래스
                    CboDivision.DataSource = new BindingSource(temp, null); // divtbl은 null로 써준다.
                    // 키 값 쌍이기 때문에
                    CboDivision.DisplayMember = "Value";
                    CboDivision.ValueMember = "Key";
                    CboDivision.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"LoadCboData() 비정상적 오류 {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ClearInputs()
        {
            TxtBookIdx.Text = TxtAuthor.Text = TxtNames.Text = 
            TxtISBN.Text = TxtAuthor.Text = string.Empty;
            CboDivision.SelectedIndex = -1;
            DtpReleaseDate.Value = DateTime.Now; // 오늘 날짜로 초기화
            NudPrice.Value = 0; // 책 가격 초기화

            TxtAuthor.Focus(); // 번호는 입력 안함.
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
                        query = @"INSERT INTO bookstbl
                                         (Author,
                                         Division,
                                         Names,
                                         ReleaseDate,
                                         ISBN,
                                         Price)
                                  VALUES (@Author,
	                                     @Division,
	                                     @Names,
                                         @ReleaseDate,
                                         @ISBN,
                                         @Price)";
                    }
                    else
                    {
                        query = @"UPDATE bookstbl
                                     SET Author = @Author,
	                                     Division = @Division,
	                                     Names = @Names,
	                                     ReleaseDate = @ReleaseDate,
	                                     ISBN = @ISBN,
	                                     Price = @Price
                                   WHERE bookIdx = @bookIdx;"; // 마지막 콤마 안빼면 오류난다.
                    } // INSERT 랑 UPDATE 갯수 차이 난다.
                    
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    // 이름을 통일시켜야 안틀린다.
                    MySqlParameter prmAuthor = new MySqlParameter("@Author", TxtAuthor.Text);
                    MySqlParameter prmDivision = new MySqlParameter("@Division", CboDivision.SelectedValue.ToString());
                    MySqlParameter prmNames = new MySqlParameter("@Names", TxtNames.Text);
                    MySqlParameter prmReleaseDate = new MySqlParameter("@ReleaseDate", DtpReleaseDate.Value);
                    MySqlParameter prmISBN = new MySqlParameter("@ISBN", TxtISBN.Text);
                    MySqlParameter prmPrice = new MySqlParameter("@Price", NudPrice.Value);

                    cmd.Parameters.Add(prmAuthor);
                    cmd.Parameters.Add(prmDivision);
                    cmd.Parameters.Add(prmNames);
                    cmd.Parameters.Add(prmReleaseDate);
                    cmd.Parameters.Add(prmISBN);
                    cmd.Parameters.Add(prmPrice);

                    if (isNew == false) // UPDATE 할 때는 bookIdx 파라미터를 추가 해줘야 오류안난다.
                    {
                        MySqlParameter prmBookIdx = new MySqlParameter("@BookIdx", TxtBookIdx.Text);
                        cmd.Parameters.Add(prmBookIdx);
                    }

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

                    query = @"Delete FROM bookstbl
                                WHERE bookIdx = @bookIdx";
                    // DELETE는 WHERE 절에 키 값만 잘 넣으면 된다.
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    // 이름을 통일시켜야 안틀린다.
                    MySqlParameter prmBookIdx = new MySqlParameter("@bookIdx", TxtBookIdx.Text);
                    cmd.Parameters.Add(prmBookIdx);

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
            LoadCboData(); // 콤보 박스에 들어갈 데이터 로드 

            DtpReleaseDate.Format = DateTimePickerFormat.Custom; // 커스텀 포맷을 쓰겠다 선언 enum
            DtpReleaseDate.CustomFormat = "yyyy-MM-dd"; // 년월일 표시
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
                string strQuery = "SELECT COUNT(*) FROM rentaltbl WHERE bookIdx = @bookIdx";
            
                MySqlCommand chkCmd = new MySqlCommand(strQuery, conn);
                MySqlParameter prmBookIdx = new MySqlParameter("@bookIdx", TxtBookIdx.Text);
                chkCmd.Parameters.Add(prmBookIdx);

                // 컬럼 여러 개 일 때 Leaders, 컬럼 하나만 있을 때는 Scalar
                // 비동기도 있다.
                var result = chkCmd.ExecuteScalar();
                
                if (result.ToString() != "0")
                {
                    MessageBox.Show("이미 대여중인 책 입니다.", "삭제", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                CboDivision.SelectedValue = selData.Cells[2].Value; // B001 == B001
                // selData.Cells[3] 는 사용안함
                TxtNames.Text = selData.Cells[4].Value.ToString();
                DtpReleaseDate.Value = (DateTime)selData.Cells[5].Value; // null을 포함하고있어서 as 못씀
                TxtISBN.Text = selData.Cells[6].Value.ToString();
                NudPrice.Text = (string)selData.Cells[7].Value.ToString();

                isNew = false; // 수정
            }
        }

        private void DgvResult_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            DgvResult.ClearSelection(); // 최초에 첫번째열 셀이 선택되어있는걸 해제할 수 있음
        }
        #endregion


    }
}