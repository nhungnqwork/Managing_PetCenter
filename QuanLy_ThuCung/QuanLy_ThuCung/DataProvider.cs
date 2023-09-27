using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLy_ThuCung
{
    internal class DataProvider
    {
        SqlConnection sqlConnection = new SqlConnection(@"Data Source=DESKTOP-77LQJ4S;Initial Catalog=PetShopdb;Integrated Security=True");
        public DataProvider() { }
        public DataTable Table(string query)
        {
            DataTable data = new DataTable();
            //sqlConnection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(query, sqlConnection);
            adapter.Fill(data);
            //sqlConnection.Close();
            return data;
        }
    }
}
