﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data; // DB 사용을 위해 추가
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient; // DB 사용을 위해 추가

namespace WF08_dbhandling
{
    // 데이터 베이스 쓸 때 제일 중요한 문자열
    // string connstring = "Data Source=localhost;Initial Catalog=Northwind;Persist Security Info=True;User ID=sa;Password=12345;";

    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            // TODO: 이 코드는 데이터를 'northwindDataSet.Employees' 테이블에 로드합니다. 필요 시 이 코드를 이동하거나 제거할 수 있습니다.
            this.employeesTableAdapter.Fill(this.northwindDataSet.Employees);

        }
    }
}
