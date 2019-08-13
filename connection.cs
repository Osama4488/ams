using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AMS
{
    public class connection
    {

        public static SqlConnection sql;
        public static SqlConnection getcon()
        {
            if (sql == null)
            {
                sql = new SqlConnection();
                sql.ConnectionString = ConfigurationManager.ConnectionStrings["Mycon"].ToString();
                sql.Open();
            }
            return sql;
        }
    }
}

