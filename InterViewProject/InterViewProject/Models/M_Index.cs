using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Data;
using Newtonsoft.Json;

namespace InterViewProject.Models
{
    public class M_Index
    {
        static string connectionString = WebConfigurationManager.ConnectionStrings["connect"].ConnectionString;

        public string Query()
        {
            string strJson = string.Empty;
            DataTable dtResult = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                using(SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "Select CategoryID,CategoryName,Description from Categories ";
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(dtResult);
                    sda.Dispose();
                    strJson = JsonConvert.SerializeObject(dtResult, Formatting.Indented);
                }
            }

            return strJson;
        }
    }
}