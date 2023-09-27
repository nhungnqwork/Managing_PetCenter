using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using PagedList;

namespace QuanLy_ThuCung
{
    public partial class Customers : Form
    {
        public Customers()
        {
            InitializeComponent();
            DisplayCustomer();
            Clear();
        }

        SqlConnection sqlConnection = new SqlConnection(@"Data Source=DESKTOP-77LQJ4S;Initial Catalog=PetShopdb;Integrated Security=True");
        int key = 0;

        private void Clear()
        {
            CustNameTb.Text = "";
            CustAddTb.Text = "";
            CustPhoneTb.Text = "";
        }


        #region Method
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

        private void CustomerDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            CustNameTb.Text = CustomerDGV.SelectedRows[0].Cells[1].Value.ToString();
            CustAddTb.Text = CustomerDGV.SelectedRows[0].Cells[2].Value.ToString();
            CustPhoneTb.Text = CustomerDGV.SelectedRows[0].Cells[3].Value.ToString();

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
        private void saveBtn_Click(object sender, EventArgs e)
        {
            if (CustNameTb.Text == "" || CustAddTb.Text == "" || CustPhoneTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand("insert into CustomerTbl (CustName,CustAdd,CustPhone) values(@CN,@CA,@CP)", sqlConnection);
                    cmd.Parameters.AddWithValue("@CN", CustNameTb.Text);
                    cmd.Parameters.AddWithValue("@CA", CustAddTb.Text);
                    cmd.Parameters.AddWithValue("@CP", CustPhoneTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer saved");
                    sqlConnection.Close();
                    DisplayCustomer();
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
            if (CustNameTb.Text == "" || CustAddTb.Text == "" || CustPhoneTb.Text == "")
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
            }
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
        Bitmap bmp;

        DataGridView dataGridView = new DataGridView();
        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            LocalReport localReport = new LocalReport();
            localReport.ReportPath = Application.StartupPath + "\\Report1.rdlc";
            //localReport.PrintToPrinter();
            /*int height = dataGridView.Height;
            dataGridView.Height = dataGridView.RowCount*dataGridView.RowTemplate.Height * 2;
            bmp = new Bitmap(dataGridView.Width, dataGridView.Height);
            dataGridView.DrawToBitmap(bmp, new Rectangle(0, 0, dataGridView.Width, dataGridView.Height));
            dataGridView.Height = height;
            printPreviewDialog1.ShowDialog();*/

            #region Cach 1
            /*printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("pprm", 285, 600);
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }*/
            #endregion


            /*DataGridViewRow newRow = new DataGridViewRow();
            newRow.CreateCells(CustomerDGV);
            newRow.Cells[0].Value = custId;
            newRow.Cells[1].Value = custName;
            newRow.Cells[2].Value = custAdd;
            newRow.Cells[3].Value = custPhone;
            //newRow.Cells[4].Value = total;
            //GrdTotal = GrdTotal + total;
            CustomerDGV.Rows.Add(newRow);
            //n++;
            //totalLbl.Text = "Rs" + GrdTotal;*/
        }

        int custId, pos = 60;
        string custName, custAdd, custPhone;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            /*e.Graphics.DrawString("Quynh Nhung Shop", new Font("Century Gothic", 13, FontStyle.Bold), Brushes.Red, new Point(70));
            e.Graphics.DrawString("ID NAME ADDRESS PHONE", new Font("Century Gothic", 11, FontStyle.Bold), Brushes.Red, new Point(26, 40));
           */ e.Graphics.DrawImage(bmp,0,0);
            /*foreach (DataGridViewRow row in CustomerDGV.Rows)
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
            }*/
        }
        private void label1_Click(object sender, EventArgs e)
        {
            Products f = new Products();
            f.Show();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            DialogResult h = MessageBox.Show("Bạn có thực sự muốn thoát không ?", "Thong bao", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (h == DialogResult.OK)
                Application.Exit();

        }
        #endregion

        

        private void label2_Click(object sender, EventArgs e)
        {
            Home h = new Home();
            h.Show();
            this.Hide();
        }

        private void label16_Click(object sender, EventArgs e)
        {
            Employees em = new Employees();
            em.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Billings b = new Billings();
            b.Show();
            this.Hide();
        }


    }
}
