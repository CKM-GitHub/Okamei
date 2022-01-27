using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System;
using System.Text;
using System.Linq;

namespace OkameiProduction.BL
{
    public class DBAccess
    {
        private string connectionString = StaticCache.DBInfo.GetSQLConnString();
        private int commandTimeout = StaticCache.DBInfo.CommandTimeout;

        public DBAccess()
        {
        }

        public DataTable SelectDatatable(string sSQL, params SqlParameter[] para)
        {
            DataTable dt = new DataTable();

            try
            {
                using (var conn = new SqlConnection(connectionString))
                using (var adapt = new SqlDataAdapter(sSQL, conn))
                {
                    conn.Open();
                    adapt.SelectCommand.CommandTimeout = commandTimeout;
                    adapt.SelectCommand.CommandType = CommandType.StoredProcedure;

                    if (para != null)
                    {
                        para = ChangeToDBNull(para);
                        adapt.SelectCommand.Parameters.AddRange(para);
                    }

                    adapt.Fill(dt);
                    conn.Close();
                }

                return dt;
            }
            catch (Exception ex)
            {
                WriteLog(ex, sSQL);
            }

            return dt;
        }

        public DataSet SelectDataSet(string sSQL, params SqlParameter[] para)
        {
            DataSet ds = new DataSet();

            try
            {
                using (var conn = new SqlConnection(connectionString))
                using (var adapt = new SqlDataAdapter(sSQL, conn))
                {
                    conn.Open();
                    adapt.SelectCommand.CommandTimeout = commandTimeout;
                    adapt.SelectCommand.CommandType = CommandType.StoredProcedure;

                    if (para != null)
                    {
                        adapt.SelectCommand.Parameters.AddRange(para);
                    }
                    adapt.Fill(ds);
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex, sSQL);
            }

            return ds;
        }

        public bool InsertUpdateDeleteData(string sSQL, params SqlParameter[] para)
        {
            try
            {
                using (var conn = new SqlConnection(connectionString))
                using (var cmd = new SqlCommand(sSQL, conn))
                {
                    conn.Open();
                    cmd.CommandTimeout = commandTimeout;
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (para != null)
                    {
                        cmd.Parameters.AddRange(para);
                    }
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }

                return true;
            }
            catch (Exception ex)
            {
                WriteLog(ex, sSQL);
                return false;
            }
        }

        private SqlParameter[] ChangeToDBNull(SqlParameter[] para)
        {
            foreach (var p in para)
            {
                if (p.Value == null || string.IsNullOrWhiteSpace(p.Value.ToString()))
                {
                    p.Value = DBNull.Value;
                    p.SqlValue = DBNull.Value;
                }
                else
                {
                    p.Value = p.Value.ToString().Replace("\t", string.Empty);

                }
            }

            return para;
        }

        private void WriteLog(Exception ex, string sql)
        {
            Logger.GetInstance().Error(ex, sql);
        }
    }
}
