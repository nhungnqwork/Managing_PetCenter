using Microsoft.Reporting.WinForms;
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
    public partial class FormReport : Form
    {
        public FormReport()
        {
            InitializeComponent();
        }
        Modify modify = new Modify();

        private void FormReport_Load(object sender, EventArgs e)
        {
            reportViewer1.LocalReport.ReportEmbeddedResource = "QuanLy_ThuCung.Report1.rdlc";
            ReportDataSource reportDataSource = new ReportDataSource();
            reportDataSource.Name = "DataSet1";
            // reportDataSource.Value = modify.Table("select * from Customer where Total >= 12000.00");
            reportDataSource.Value = modify.Table("select * from BillTbl");
            reportViewer1.LocalReport.DataSources.Add(reportDataSource);
            this.reportViewer1.RefreshReport();
        }
    }
}
