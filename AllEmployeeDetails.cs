using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dashboard
{
    public partial class AllEmployeeDetails : Form
    {
        public AllEmployeeDetails()
        {
            InitializeComponent();
        }

        private void AllEmployeeDetails_Load(object sender, EventArgs e)
        {
            string connStr = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Aspen.mdf;Integrated Security=True;Connect Timeout=30";
            string sqlStr = " SELECT * FROM Employee";
            SqlDataAdapter sda = new SqlDataAdapter(sqlStr, connStr);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
            sda.Dispose();

        }
    }
}
