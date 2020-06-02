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
using System.IO;

namespace Dashboard
{
    public partial class Employee_Interface : Form
    {
        string pwd = Class1.GetRandomPassword(20);
        string wanted_path;       
        public static SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\Aspen.mdf;Integrated Security=True;Connect Timeout=30;");
        public Employee_Interface()
        {
            InitializeComponent();
            
        }

        string Gender;

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnView_Click(object sender, EventArgs e)
        {
            AllEmployeeDetails a = new AllEmployeeDetails();
            a.ShowDialog();
          
        }

        private void Employee_Interface_Load(object sender, EventArgs e)
        {
            cc();
            cb();
            cd();
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
                cmbId.Items.Add(dr["EmpId"].ToString());
            }
            con.Close();
        }

        private void cmbId_SelectedIndexChanged(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Employee where EmpId= '" + cmbId.SelectedItem.ToString()+"'";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                txtEmpId.Text = dr["EmpId"].ToString();
                txtLastN.Text= dr["LastName"].ToString();
                txtFirstN.Text = dr["FirstName"].ToString();
                txtAddress.Text = dr["Address"].ToString();
                dateTimePickerDOB.Text = dr["DOB"].ToString();
                if (dr["Sex"].ToString() == "M")
                    radMale.Checked = true;
                else
                    radFemale.Checked = true;
                txtPhoneN.Text = dr["PhoneNumber"].ToString();
                cmbPosition.Text = dr["Position_name"].ToString();
                cmbdept.Text = dr["Dept_Name"].ToString();
                txtAge.Text = dr["Age"].ToString();
                dateTimePickerHired.Text = dr["HiredDate"].ToString();



            }
            con.Close();
        }

        private void dateTimePickerDOB_ValueChanged(object sender, EventArgs e)
        {
            DateTime from = dateTimePickerDOB.Value;
            DateTime to = DateTime.Now;
            TimeSpan TSpan = to - from;
            double days = TSpan.TotalDays;
            txtAge.Text = (days / 365).ToString("0");
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (radMale.Checked)
                Gender = "M";
            else if (radFemale.Checked)
                Gender = "F";

            string img_path;
            File.Copy(openFileDialog1.FileName, wanted_path + "\\EmpImage\\" + pwd + " .jpg");
            img_path = "EmpImage\\" + pwd + " .jpg";

            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "insert into Employee values('"+txtEmpId.Text+ "','" + txtLastN.Text + "','" + txtFirstN.Text + "','" + txtAddress.Text + "','" + dateTimePickerDOB.Text + "','" + Gender + "','" + txtPhoneN.Text + "','" + cmbPosition.Text + "','" + cmbdept.Text + "','" + txtAge.Text + "','" + dateTimePickerHired.Text + "','"+ img_path.ToString()+"')";
            cmd.ExecuteNonQuery();                   
            con.Close();

            cc();
            MessageBox.Show("New record is Saved");
       
        }

        private void radMale_CheckedChanged(object sender, EventArgs e)
        {
        
        }

        private void radFemale_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtEmpId.Text = txtLastN.Text = txtFirstN.Text = txtAddress.Text = txtPhoneN.Text= cmbdept.Text = txtAge.Text =" ";
            radFemale.Checked = radMale.Checked = false;
            cmbPosition.Text = cmbId.Text= "";
            pictureBox2.Image = null;
        }

        private void cmbPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }
        private void cb()
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
                cmbPosition.Items.Add(dr["Position_name"].ToString());
            }
            con.Close();
        }
        private void cd()
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
                cmbdept.Items.Add(dr["Dept_Name"].ToString());
            }
            con.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update Employee set LastName='" + txtLastN.Text + "',FirstName='" + txtFirstN.Text + "',Address='" + txtAddress.Text + "',PhoneNumber='" + txtPhoneN.Text + "',Position_name='" + cmbPosition.Text + "',Dept_Name='" + cmbdept.Text + "',HiredDate='" + dateTimePickerHired.Text + "'  where EmpId ='"+txtEmpId.Text +"'";
                cmd.ExecuteNonQuery();
                con.Close();


                MessageBox.Show("Record is  updated successfully");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "delete from Employee where LastName='" + txtLastN.Text + "'";
            cmd.ExecuteNonQuery();
            con.Close();

            cc();
            MessageBox.Show("Record is deleted successfully");
        }

        private void btnLoadImage_Click(object sender, EventArgs e)
        {
            wanted_path = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
            DialogResult result = openFileDialog1.ShowDialog();
            openFileDialog1.Filter= "JPG Files(*.jpg)|*.jpg|PNG Files(*.png)|*.png|All Files(*.*)|*.*";
            if(result==DialogResult.OK)
            {
                pictureBox2.ImageLocation = openFileDialog1.FileName;
                pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }
    }
}
