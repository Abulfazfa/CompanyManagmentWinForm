using System;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess
{
    public class DBContext
    {
        private readonly string connectionString = "Data Source=.;" + "Initial Catalog=Spotify;" + "Integrated Security=SSPI;";

        public DataTable ExecuteQuery(string query)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(query, con);
                    DataTable table = new DataTable();
                    dataAdapter.Fill(table);
                    return table;
                }
                catch (Exception ex)
                {
                    // Handle exception or log the error
                    throw ex;
                }
            }
        }

        public int ExecuteNonQuery(string query)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(query, con);
                    return cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    // Handle exception or log the error
                    throw ex;
                }
            }
        }
    }
}
