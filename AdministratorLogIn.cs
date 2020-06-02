using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Dashboard
{
    public partial class AdministratorLogIn : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=REEAN\SQLEXPRESS;Initial Catalog=LogIn;Integrated Security=True;Pooling=False");
        int count = 0;
        public AdministratorLogIn()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = " select * from Login where UserName='"+txtUsername.Text+"' and Password='"+txtPassword.Text+"' ";
            cmd.ExecuteNonQuery();

            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            count = Convert.ToInt32(dt.Rows.Count.ToString());
            if(count==0)
            {
                MessageBox.Show(" UserName Password does not match");

            }
            else
            {
                this.Hide();
                Employee_Interface f = new Employee_Interface();
                f.ShowDialog();

            }
        }

        private void AdministratorLogIn_Load(object sender, EventArgs e)
        {
            if(con.State==ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
        }
    }
}
