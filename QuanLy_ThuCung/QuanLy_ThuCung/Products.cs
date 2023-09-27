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

namespace QuanLy_ThuCung
{
    public partial class Products : Form
    {
        public Products()
        {
            InitializeComponent();
            DisplayProducts();
            Clear();
        }

        SqlConnection sqlConnection = new SqlConnection(@"Data Source=DESKTOP-77LQJ4S;Initial Catalog=PetShopdb;Integrated Security=True");
        int key = 0;
        private void DisplayProducts()
        {
            sqlConnection.Open();
            string query = "select * from ProductTbl";
            DataTable data = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(query, sqlConnection);
            adapter.Fill(data);
            ProductDGV.DataSource = data;
            sqlConnection.Close();
        }

        private void Clear()
        {
            PrNameTb.Text = "";
            QTyTb.Text = "";
            PriceTb.Text = "";
            CatCb.SelectedIndex = 0;
        }

        private void ProductDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            PrNameTb.Text = ProductDGV.SelectedRows[0].Cells[1].Value.ToString();
            CatCb.Text = ProductDGV.SelectedRows[0].Cells[2].Value.ToString();
            QTyTb.Text = ProductDGV.SelectedRows[0].Cells[3].Value.ToString();
            PriceTb.Text = ProductDGV.SelectedRows[0].Cells[4].Value.ToString();
            if (PrNameTb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(ProductDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        #region Events
        private void label16_Click(object sender, EventArgs e)
        {
            Employees em = new Employees();
            em.Show();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            DialogResult h = MessageBox.Show("Bạn có thực sự muốn thoát không ?", "Thong bao", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (h == DialogResult.OK)
                Application.Exit();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Home h = new Home();
            h.Show();
            this.Hide();
        }


        private void label15_Click(object sender, EventArgs e)
        {
            Customers c = new Customers();
            c.Show();
            this.Hide();
        }
        private void label5_Click(object sender, EventArgs e)
        {
            Billings b = new Billings();
            b.ShowDialog();
        }

        #endregion


        #region Method
        private void Savebtn_Click(object sender, EventArgs e)
        {
            if (PrNameTb.Text == "" || CatCb.SelectedIndex == -1 || QTyTb.Text == "" | PriceTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand("insert into ProductTbl (PrName,PrCat,PrQty,PrPrice) values(@PN,@PC,@PQ,@PP)", sqlConnection);
                    cmd.Parameters.AddWithValue("@PN", PrNameTb.Text);
                    cmd.Parameters.AddWithValue("@PC",CatCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@PQ", QTyTb.Text);
                    cmd.Parameters.AddWithValue("@PP", PriceTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Products added");
                    sqlConnection.Close();
                    DisplayProducts();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void Editbtn_Click(object sender, EventArgs e)
        {
            if (PrNameTb.Text == "" || CatCb.SelectedIndex == -1 || QTyTb.Text == "" | PriceTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand("update ProductTbl set PrName=@PN,PrCat=@PC,PrQty=@PQ,PrPrice=@PP where PrId=@PKey", sqlConnection);
                    cmd.Parameters.AddWithValue("@PN", PrNameTb.Text);
                    cmd.Parameters.AddWithValue("@PC", CatCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@PQ", QTyTb.Text);
                    cmd.Parameters.AddWithValue("@PP", PriceTb.Text);
                    cmd.Parameters.AddWithValue("@PKey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Product updated!!!");
                    sqlConnection.Close();
                    DisplayProducts();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void deletebtn_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Select an employee");
            }
            else
            {
                try
                {
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand("delete from ProductTbl where PrId=@PKey", sqlConnection);
                    cmd.Parameters.AddWithValue("@PKey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Product deleted!!!");
                    sqlConnection.Close();
                    DisplayProducts();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        #endregion



    }
}
