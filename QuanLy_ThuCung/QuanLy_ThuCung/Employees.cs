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
    public partial class Employees : Form
    {
        public Employees()
        {
            InitializeComponent();
            DisplayEmployee();
        }

        SqlConnection sqlConnection = new SqlConnection(@"Data Source=DESKTOP-77LQJ4S;Initial Catalog=PetShopdb;Integrated Security=True");
        int key = 0;
        private void DisplayEmployee()
        {
            sqlConnection.Open();
            string query = "select * from EmployeeTbl";
            DataTable data = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(query,sqlConnection);
            adapter.Fill(data);
            EmployeeDGV.DataSource= data;
            sqlConnection.Close();
        }

        private void Clear()
        {
            EmpNameTb.Text = "";
            EmpAddTb.Text = "";
            EmpDOB.Text = "";
            EmpPhoneTb.Text = "";
            PasswordTb.Text = "";
        }
        
        private void EmployeeDGV_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            EmpNameTb.Text = EmployeeDGV.SelectedRows[0].Cells[1].Value.ToString();
            EmpAddTb.Text = EmployeeDGV.SelectedRows[0].Cells[2].Value.ToString();
            EmpDOB.Text = EmployeeDGV.SelectedRows[0].Cells[3].Value.ToString();
            EmpPhoneTb.Text = EmployeeDGV.SelectedRows[0].Cells[4].Value.ToString();
            PasswordTb.Text = EmployeeDGV.SelectedRows[0].Cells[5].Value.ToString();
            if (EmpNameTb.Text == "")
            {
                key = 0;
            }
            else
            {
                key = Convert.ToInt32(EmployeeDGV.SelectedRows[0].Cells[0].Value.ToString());
            }
        }


        #region Function

        private void Savebtn_Click(object sender, EventArgs e)
        {
            if (EmpNameTb.Text == "" || EmpAddTb.Text == "" || EmpPhoneTb.Text == "" | PasswordTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    sqlConnection.Open();
                    string query = "insert into EmployeeTbl (EmpName,EmpAdd,EmpDOB,EmpPhone,EmpPass) values(@EN,@EA,@ED,@EP,@EPA)";
                    SqlCommand cmd = new SqlCommand(query, sqlConnection);
                    cmd.Parameters.AddWithValue("@EN", EmpNameTb.Text);
                    cmd.Parameters.AddWithValue("@EA", EmpAddTb.Text);
                    cmd.Parameters.AddWithValue("@ED", EmpDOB.Text);
                    cmd.Parameters.AddWithValue("@EP", EmpPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@EPA", PasswordTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Employee added");
                    sqlConnection.Close();
                    DisplayEmployee();
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
            if (EmpNameTb.Text == "" || EmpAddTb.Text == "" || EmpPhoneTb.Text == "" | PasswordTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand("update EmployeeTbl set EmpName=@EN,EmpAdd=@EA,EmpDOB=@ED,EmpPhone=@EP,EmpPass=@EPA where EmpNum=@EKey", sqlConnection);
                    cmd.Parameters.AddWithValue("@EN", EmpNameTb.Text);
                    cmd.Parameters.AddWithValue("@EA", EmpAddTb.Text);
                    cmd.Parameters.AddWithValue("@ED", EmpDOB.Text);
                    cmd.Parameters.AddWithValue("@EP", EmpPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@EPA", PasswordTb.Text);
                    cmd.Parameters.AddWithValue("@EKey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Employee update!!!");
                    sqlConnection.Close();
                    DisplayEmployee();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void DeleteBtn_Click_1(object sender, EventArgs e)
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
                    SqlCommand cmd = new SqlCommand("delete from EmployeeTbl where EmpNum=@EKey", sqlConnection);
                    cmd.Parameters.AddWithValue("@EKey", key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Employee delete!!!");
                    sqlConnection.Close();
                    DisplayEmployee();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        private void guna2GradientCircleButton1_Click(object sender, EventArgs e)
        {
            Clear();
        }


        #endregion


        #region Events
        private void label1_Click(object sender, EventArgs e)
        {
            Products obj = new Products();
            obj.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Customers obj = new Customers();
            obj.Show();
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
            Home home = new Home();
            home.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Billings b= new Billings();
            b.Show();
            this.Hide();
        }
    }
}
