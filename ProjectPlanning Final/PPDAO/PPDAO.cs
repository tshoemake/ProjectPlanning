using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace DataAccess
{

    public class PPDAO
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connstring"].ToString());
        public string GetProjName(string dbProjCode)
        {
            string dbProjName = "";
            SqlCommand cmd = new SqlCommand("SELECT name FROM [Projects] WHERE code='" + dbProjCode + "'", conn);
            SqlDataReader reader = null;
            conn.Open();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                dbProjName = reader["name"].ToString();
            }
            conn.Close();
            return dbProjName;
        }

        public string GetProjStart(string dbProjCode)
        {
            string dbStartDate = "";
            SqlCommand cmd = new SqlCommand("SELECT convert(nvarchar, start_date, 101) as start FROM [Projects] WHERE code='" + dbProjCode + "'", conn);
            SqlDataReader reader = null;
            conn.Open();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                dbStartDate = reader["start"].ToString();
            }
            conn.Close();
            return dbStartDate;
        }

        public string GetProjEnd(string dbProjCode)
        {
            string dbEndDate = "";
            SqlCommand cmd = new SqlCommand("SELECT convert(nvarchar, end_date, 101) as end1 FROM [Projects] WHERE code='" + dbProjCode + "'", conn);
            SqlDataReader reader = null;
            conn.Open();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                dbEndDate = reader["end1"].ToString();
            }
            conn.Close();
            return dbEndDate;
        }

        public string GetProjID(string dbProjCode)
        {
            string dbProjID = "";
            SqlCommand cmd = new SqlCommand("SELECT ProjectID FROM [Projects] WHERE code='" + dbProjCode + "'", conn);
            SqlDataReader reader = null;
            conn.Open();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                dbProjID = reader["ProjectID"].ToString();
            }
            conn.Close();
            return dbProjID;
        }

        public List<string> GetProjResources(string dbProjID)
        {
            using (SqlCommand cmd = new SqlCommand("  SELECT name FROM Resources JOIN Assignments ON Assignments.ResourceID = Resources.ResourceID  where Assignments.ProjectID = " + dbProjID + "", conn))
            {
                conn.Open();
                List<string> resList = new List<string>();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        resList.Add(reader.GetValue(0).ToString());
                    }
                }
                conn.Close();
                return resList;
            }
        }

        public List<string> GetProjOpenResources(string clickDate)
        {
            using (SqlCommand cmd = new SqlCommand("dbo.sp_pull_resources", conn) { CommandType = CommandType.StoredProcedure })
            {
                conn.Open();
                List<string> openList = new List<string>();
                cmd.Parameters.Add("@date", SqlDbType.VarChar).Value = clickDate;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        openList.Add(reader.GetValue(0).ToString());
                    }
                }
                conn.Close();
                return openList;
            }
        }

        public static void InsertProject(string name, string code, string start, string end)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connstring"].ToString());
            using (SqlCommand cmd = new SqlCommand("dbo.sp_insert_project", conn) { CommandType = CommandType.StoredProcedure })
            {

                //cmd.Parameters.AddWithValue("@ProjectID", projid);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@code", code);
                cmd.Parameters.AddWithValue("@start_date", start);
                cmd.Parameters.AddWithValue("@end_date", end);
                try
                {
                    conn.Open();
                    cmd.ExecuteScalar();
                    conn.Close();
                }
                catch (Exception x)
                {
                    throw;
                }
            }
        }

        public static void InsertAssignments(string name, string code, string resources)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connstring"].ToString());
            using (var cmd2 = new SqlCommand("dbo.sp_insert_assignment", conn) { CommandType = CommandType.StoredProcedure })
            {
                conn.Open();
                //cmd.Parameters.AddWithValue("@ProjectID", projid);
                cmd2.Parameters.AddWithValue("@name", name);
                cmd2.Parameters.AddWithValue("@code", code);
                cmd2.Parameters.AddWithValue("@resources", resources);
                cmd2.ExecuteNonQuery();
                conn.Close();
            }

        }
        public string GetProjDate(string dbProjCode, string pos)
        {
            string dbDate = "";
            SqlCommand cmd = new SqlCommand("sp_get_date", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@projCode", dbProjCode));
            cmd.Parameters.Add(new SqlParameter("@type", pos));

            try
            {
                conn.Open();
                dbDate = cmd.ExecuteScalar().ToString();
                conn.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
            return dbDate;
        }

        public static void UpdateProject(string oldCode, string name, string code, string start, string end)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connstring"].ToString());
            using (var cmd3 = new SqlCommand("dbo.sp_update_project", conn) { CommandType = CommandType.StoredProcedure })
            {
                conn.Open();
                cmd3.Parameters.AddWithValue("@originalCode", oldCode);
                cmd3.Parameters.AddWithValue("@name", name);
                cmd3.Parameters.AddWithValue("@code", code);
                cmd3.Parameters.AddWithValue("@start_date", start);
                cmd3.Parameters.AddWithValue("@end_date", end);
                cmd3.ExecuteNonQuery();
                conn.Close();
            }
        }

        public static void UpdateAssignments(string name, string code, string resources)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connstring"].ToString());
            using (var cmd2 = new SqlCommand("dbo.sp_update_assignment", conn) { CommandType = CommandType.StoredProcedure })
            {
                conn.Open();
                //cmd.Parameters.AddWithValue("@ProjectID", projid);
                cmd2.Parameters.AddWithValue("@name", name);
                cmd2.Parameters.AddWithValue("@code", code);
                cmd2.Parameters.AddWithValue("@resources", resources);
                cmd2.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
}
