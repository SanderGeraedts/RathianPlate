using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Oracle.DataAccess.Client;
using System.Data;

namespace RathianPlate
{
    public class Database
    {
        private OracleConnection conn;

        public Database()
        {
            conn = new OracleConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DatabaseConnection"].ConnectionString);
        }

        private void NonQueryBase(string sql)                             //this method is used for executing nonquery based SQL statements. This drastically lowers the amount of copied code and makes the update and insert statements more readable
        {
            OracleCommand command = new OracleCommand(sql, conn);

            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                command.ExecuteNonQuery();
            }
            catch
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }


    }
}