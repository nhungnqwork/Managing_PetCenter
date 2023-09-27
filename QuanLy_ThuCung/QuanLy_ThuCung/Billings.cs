using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace QuanLy_ThuCung
{
    public partial class Billings : Form
    {
        public Billings()
        {
            InitializeComponent();
            DisplayProducts();
            DisplayCustomer();
            /*GetCustomer();
            DisplayProduct();
            GetCustName();
            UpdateStock();*/
        }

        SqlConnection sqlConnection = new SqlConnection(@"Data Source=DESKTOP-77LQJ4S;Initial Catalog=PetShopdb;Integrated Security=True");

        int key = 0, Stock = 0;

        private void Clear()
        {
            //CustNameTb.Text = "";
            //CustIdCb.SelectedItem= 0;
            PrNameTb.Text = "";
            PriceTb.Text = "";
            QTyTb.Text = "";
        }


        #region Method
        private void DisplayProducts()
        {
            sqlConnection.Open();
            string query = "select * from ProductTbl";
            DataTable data = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(query, sqlConnection);
            adapter.Fill(data);
            ProDTGV.DataSource = data;
            sqlConnection.Close();
        }

        private void DisplayCustomer()
        {
            sqlConnection.Open();
            string query = "select * from CustomerTbl";
            DataTable data = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(query, sqlConnection);
            adapter.Fill(data);
            CustomerDGV.DataSource = data;
            sqlConnection.Close();
        }

        private void ProductDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            PrNameTb.Text = ProDTGV.SelectedRows[0].Cells[1].Value.ToString();
            CatCb.Text = ProDTGV.SelectedRows[0].Cells[2].Value.ToString();
            QTyTb.Text = ProDTGV.SelectedRows[0].Cells[3].Value.ToString();
            PriceTb.Text = ProDTGV.SelectedRows[0].Cells[4].Value.ToString();
            if (PrNameTb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(ProDTGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void CustomerDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            CustNameTb.Text = CustomerDGV.SelectedRows[0].Cells[1].Value.ToString();
            
            if (CustNameTb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(CustomerDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        #endregion


        #region Function

        private void guna2GradientButton1_Click(object sender, EventArgs e)
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
                    cmd.Parameters.AddWithValue("@PC", CatCb.SelectedItem.ToString());
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

        private void editbtn_Click(object sender, EventArgs e)
        {
            /*if (CustNameTb.Text == "" || CustAddTb.Text == "" || CustPhoneTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand("update CustomerTbl set CustName=@CN,CustAdd=@CA,CustPhone=@CP where CustId=@CKey", sqlConnection);
                    cmd.Parameters.AddWithValue("@CN", CustNameTb.Text);
                    cmd.Parameters.AddWithValue("@CA", CustAddTb.Text);
                    cmd.Parameters.AddWithValue("@CP", CustPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@CKey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer edited");
                    sqlConnection.Close();
                    DisplayCustomer();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }*/
        }

        private void deleteBtn_Click(object sender, EventArgs e)
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
                    SqlCommand cmd = new SqlCommand("delete from CustomerTbl where CustId=@CKey", sqlConnection);
                    cmd.Parameters.AddWithValue("@CKey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer deleted!!!");
                    sqlConnection.Close();
                    DisplayCustomer();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        #endregion



        #region Events
        private void label19_Click(object sender, EventArgs e)
        {
            Home h = new Home();
            h.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Products p = new Products();
            p.Show();
            this.Hide();
        }

        private void label16_Click(object sender, EventArgs e)
        {
            Employees em = new Employees();
            em.Show();
            this.Hide();
        }

        private void Reset()
        {
            PrNameTb.Text = "";
            QTyTb.Text = "";
            Stock = 0;
            key = 0;
        }

        /*private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            if (QtyTb.Text == "" || Convert.ToInt32(QtyTb.Text) > Stock)
            {
                MessageBox.Show("No enough in house");
            }
            else if (QtyTb.Text == "" || Key == 0)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                int total = Convert.ToInt32(QtyTb.Text) * Convert.ToInt32(PrPriceTb.Text);
                DataGridViewRow newRow = new DataGridViewRow();
                newRow.CreateCells(BillDGV);
                newRow.Cells[0].Value = n + 1;
                newRow.Cells[1].Value = PrNameTb.Text;
                newRow.Cells[2].Value = QtyTb.Text;
                newRow.Cells[3].Value = PrPriceTb.Text;
                newRow.Cells[4].Value = total;
                GrdTotal = GrdTotal + total;
                BillDGV.Rows.Add(newRow);
                n++;
                totalLbl.Text = "Rs" + GrdTotal;
                UpdateStock();
                Reset();
            }
        }*/

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void guna2DataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            /*PrNameTb.Text = ProductDGV.SelectedRows[0].Cells[1].Value.ToString();
            Stock = Convert.ToInt32(ProductDGV.SelectedRows[0].Cells[3].Value.ToString());
            PrPriceTb.Text = ProductDGV.SelectedRows[0].Cells[4].Value.ToString();
            if (PrNameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(ProductDGV.SelectedRows[0].Cells[0].Value.ToString());
            }*/
        }

        string prodname;

        private void label15_Click(object sender, EventArgs e)
        {
            Customers c = new Customers();
            c.Show();
            this.Hide();
        }

        private void guna2ProgressBar2_ValueChanged(object sender, EventArgs e)
        {

        }
        int custId, pos = 60;
        string custName, custAdd, custPhone;
        private void PrintBtn_Click(object sender, EventArgs e)
        {
            #region Cach 1
            Graphics g = this.CreateGraphics();
            bmp = new Bitmap(this.Size.Width, this.Size.Height, g);
            Graphics mg = Graphics.FromImage(bmp);
            mg.CopyFromScreen(this.Location.X, this.Location.Y, 0, 0, this.Size);
            printPreviewDialog1.ShowDialog();
            #endregion

            printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("pprm", 285, 600);
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
            DataGridViewRow newRow = new DataGridViewRow();
            newRow.CreateCells(CustomerDGV);
            newRow.Cells[0].Value = custId;
            newRow.Cells[1].Value = custName;
            newRow.Cells[2].Value = custAdd;
            newRow.Cells[3].Value = custPhone;
            //newRow.Cells[4].Value = total;
            //GrdTotal = GrdTotal + total;
            CustomerDGV.Rows.Add(newRow);
            //n++;
            //totalLbl.Text = "Rs" + GrdTotal;

        }

        Bitmap bmp;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("Quynh Nhung Shop", new Font("Century Gothic", 13, FontStyle.Bold), Brushes.Red, new Point(70));
            e.Graphics.DrawString("ID NAME ADDRESS PHONE", new Font("Century Gothic", 11, FontStyle.Bold), Brushes.Red, new Point(26, 40));
            foreach (DataGridViewRow row in CustomerDGV.Rows)
            {
                custId = Convert.ToInt32(row.Cells["Column1"].Value);
                custName = "" + row.Cells["Column2"].Value;
                custAdd = "" + row.Cells["Column3"].Value;
                custPhone = "" + row.Cells["Column4"].Value;
                e.Graphics.DrawString("" + custId, new Font("Century Gothic", 9, FontStyle.Bold), Brushes.Blue, new Point(26, pos));
                e.Graphics.DrawString("" + custName, new Font("Century Gothic", 9, FontStyle.Bold), Brushes.Blue, new Point(45, pos));
                e.Graphics.DrawString("" + custAdd, new Font("Century Gothic", 9, FontStyle.Bold), Brushes.Blue, new Point(120, pos));
                e.Graphics.DrawString("" + custPhone, new Font("Century Gothic", 9, FontStyle.Bold), Brushes.Blue, new Point(170, pos));
                //e.Graphics.DrawString("" + custPhone, new Font("Century Gothic", 9, FontStyle.Bold), Brushes.Blue, new Point(235, pos));
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {
            DialogResult h = MessageBox.Show("Bạn có thực sự muốn thoát không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (h == DialogResult.OK)
            {
                Application.Exit();
            }
        }
        #endregion





    }
}
