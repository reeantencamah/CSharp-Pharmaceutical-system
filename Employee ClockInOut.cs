using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dashboard
{
    public partial class Employee_ClockInOut : Form
    {
        Bitmap bitmap1, bitmap2;
        public static SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Aspen.mdf;Integrated Security=True;Connect Timeout=30;");
        public Employee_ClockInOut()
        {
            InitializeComponent();
        }
        private void cc()
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Employee";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                cmbId2.Items.Add(dr["EmpId"].ToString());
            }
            con.Close();
        }

        private void btnLoad1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openflg = new OpenFileDialog();
            if(openflg.ShowDialog()==DialogResult.OK)
            {
                pictureBox1.ImageLocation = openflg.FileName;
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                bitmap1 = new Bitmap(openflg.FileName);
            }

        }

        private void btnLoad2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openflg = new OpenFileDialog();
            if (openflg.ShowDialog() == DialogResult.OK)
            {
                pictureBox2.ImageLocation = openflg.FileName;
                pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
                bitmap2 = new Bitmap(openflg.FileName);
            }

        }

        private void Employee_ClockInOut_Load(object sender, EventArgs e)
        {
            cc();
        }

        private void btnCompare_Click(object sender, EventArgs e)
        {
            bool compare = ImageCompareString(bitmap1, bitmap2);
            if (compare == true)
            {
                MessageBox.Show($" {cmbId2.SelectedItem} clock in at {DateTime.Now} ");
            }
            else
                MessageBox.Show("Not match");
        }

        private void btnCin_Click(object sender, EventArgs e)
        {
            bool compare = ImageCompareString(bitmap1, bitmap2);
            if (compare == true)
            {
                MessageBox.Show($" {cmbId2.SelectedItem} clock in at {DateTime.Now} ");
            }
            else
                MessageBox.Show("Not match");
            pictureBox1.Image = pictureBox2.Image = null;
        }

        private void btnCout_Click(object sender, EventArgs e)
        {
            bool compare = ImageCompareString(bitmap1, bitmap2);
            if (compare == true)
            {
                MessageBox.Show($" {cmbId2.SelectedItem} clock out at {DateTime.Now} ");
            }
            else
                MessageBox.Show("Not match");
            pictureBox1.Image = pictureBox2.Image = null;
        }

        private bool ImageCompareString(Bitmap bitmap1, Bitmap bitmap2)
        {
            //throw new NotImplementedException();

            MemoryStream ms = new MemoryStream();
            bitmap1.Save(ms, ImageFormat.Png);
            string firstbitmap = Convert.ToBase64String(ms.ToArray());
            ms.Position=0;
            bitmap2.Save(ms, ImageFormat.Png);
            string secondbitmap = Convert.ToBase64String(ms.ToArray());
            if (firstbitmap.Equals(secondbitmap))
            {
                return true;
            }
            else return false;

        }
    }
}
