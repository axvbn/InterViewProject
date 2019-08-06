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

                using (SqlCommand cmd = new SqlCommand())
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

        public string Insert(Categories myCategories)
        {
            string strResult = string.Empty;
            int result = 0;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "Insert Into Categories(CategoryName,Description,Picture) Values(@CategoryName,@Description,NULL) ";
                        cmd.Parameters.Add(new SqlParameter("@CategoryName", myCategories.CategoryName));
                        cmd.Parameters.Add(new SqlParameter("@Description", myCategories.Description));
                        result = cmd.ExecuteNonQuery();

                        if (result > 0)
                        {
                            strResult = "新增成功";
                        }
                        else
                        {
                            strResult = "新增失敗";
                        }
                    }
                    catch (SqlException)
                    {
                        strResult = "新增失敗";
                        throw;
                    }
                }                
            }

            return strResult;
        }
    }
}