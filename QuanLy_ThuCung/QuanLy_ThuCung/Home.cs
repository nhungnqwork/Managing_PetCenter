using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLy_ThuCung
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Products p = new Products();
            p.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Employees em = new Employees();
            em.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Customers c = new Customers();
            c.Show(); this.Hide(); 
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Billings b = new Billings();
            b.Show(); this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            DialogResult h = MessageBox.Show("Bạn có thực sự muốn thoát?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (h == DialogResult.OK)
            {
                Application.Exit();
            }
        }

    }
}
