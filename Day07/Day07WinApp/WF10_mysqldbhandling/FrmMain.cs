using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WF10_mysqldbhandling
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            // MySQL용 연결문자열
            // 필수
            string connectionString = "Server=localhost;Port=3306;DataBase=bookrentalshop;Uid=root;Pwd=12345;";
            MySqlConnection conn = new MySqlConnection(connectionString);
            // 필수
            conn.Open();
            // 필수
            string selQuery = @"SELECT memberIdx
                                     , Names
                                     , Levels
                                     , Addr
                                     , Mobile
                                     , Email
                                  FROM membertbl;";
            // 필수
            MySqlDataAdapter adaptor = new MySqlDataAdapter(selQuery, conn);
            // 필수
            DataSet ds = new DataSet();
            adaptor.Fill(ds);

            BindingSource source = new BindingSource();
            source.DataSource = source;

            DgvMember.DataSource = ds.Tables[0];
            // 필수
            conn.Close();
        }
    }
}
