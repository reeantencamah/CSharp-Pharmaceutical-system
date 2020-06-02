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

namespace WholesalerSupplierR
{
    public partial class Product : Form
    {
        public Product()
        {
            InitializeComponent();
        }

        DataTable dt = new DataTable();
        int rowIndex = 0;

        private void Product_Load(object sender, EventArgs e)
        {
            string connStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Pharma.mdf;Integrated Security=True;Connect Timeout=30";
            string sqlStr = "Select * From Product";
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(sqlStr, connStr);
            sda.Fill(dt);
            dgvProduct.DataSource = dt;
            sda.Dispose();

            dgvProduct.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

           


        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

            string searchValue = txtSearch.Text;
            
            dgvProduct.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            try
            {
                
                foreach (DataGridViewRow row in dgvProduct.Rows)
                {
                    dgvProduct.ClearSelection();


                    if (row.Cells[1].Value.ToString().Equals(searchValue))
                    {
                        row.Selected = true;
                        break;

                    }

                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }





            

            


        }

        private void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            int num, unit;
            if (txtQuantity.Text == "")
                num = 0; 
            else
                num = int.Parse(txtQuantity.Text);

            if (txtUnitCost.Text == "")
                unit = 0;
            else
                unit = int.Parse(txtUnitCost.Text);


            
            

            if (num>=0)
            {
                int total = num * unit;
                txtTotalCost.Text = total.ToString();
            }
            else if (num<0)
            {
               
                MessageBox.Show("Please insert how many of the chosen product you would order", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
                
            


        }

        private void dgvProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            DataGridViewRow row = this.dgvProduct.Rows[e.RowIndex];
            txtUnitCost.Text = row.Cells[2].Value.ToString();

        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Order of the selected product has been sent to Supplier!");
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtQuantity.Text = txtTotalCost.Text = txtUnitCost.Text = "";
        }
    }
}
