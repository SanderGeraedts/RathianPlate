using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Oracle.ManagedDataAccess.Client;

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

        public Hunter CheckLogin(string username, string password)
        {
            string sql = "SELECT * FROM Hunter WHERE Username = @username AND Password = @password";
            OracleCommand command = new OracleCommand(sql, conn);

            command.Parameters.Add(new OracleParameter("@username", username));
            command.Parameters.Add(new OracleParameter("@password", password));

            Hunter hunter = null;

            int id = -1;
            string name = "";
            string hr = "";

            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                OracleDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    id = Convert.ToInt32(reader["Id"]);
                    name = Convert.ToString(reader["Name"]);
                    hr = Convert.ToString(reader["HR"]);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            if (id != -1)
            {
                hunter = new Hunter(id, name, username, password, hr);
            }

            return hunter;
        }
    }
}