using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLy_ThuCung
{
    internal class Connection
    {
        private static string stringConnection = @"Data Source=DESKTOP-77LQJ4S;Initial Catalog=PetShopdb;Integrated Security=True";
        public static SqlConnection GetSqlConnection()
        {
            return new SqlConnection(stringConnection);
        }
    }
}
