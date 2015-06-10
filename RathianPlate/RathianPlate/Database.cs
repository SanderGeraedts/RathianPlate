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

        //<summary>
        // Name: NonQueryBase
        // This method is used for executing nonquery based SQL statements. 
        // This drastically lowers the amount of copied code and makes the update 
        // and insert statements more readable
        //</summary>
        private void NonQueryBase(OracleCommand command)                  
        {
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

        //<summary>
        // Name: CheckLogin
        // This method checks whether or not the user used the right credentials. 
        // It returns a filled Hunter object if true or a null Hunter object when false.
        //</summary>
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

        //<summary>
        // Name: RegisterHunter
        // This method has 2 functions. First it inserts the newly registered hunter into the database.
        // Then it retrieves the id of the hunter to create the Hunter object. If the Id couldn't be
        // retrieved, it creates a null object.
        //</summary>
        public Hunter RegisterHunter(string name, string username, string password, string hr)
        {
            //insert Hunter into Database
            string sql = "INSERT INTO Hunter(Name, Username, Password, HR) VALUES ('@name', '@username', '@password', @hr);";
            OracleCommand command = new OracleCommand(sql, conn);

            command.Parameters.Add(new OracleParameter("@name", name));
            command.Parameters.Add(new OracleParameter("@username", username));
            command.Parameters.Add(new OracleParameter("@password", password));
            command.Parameters.Add(new OracleParameter("@hr", hr));

            NonQueryBase(command);

            //retrieve id to create Hunter object
            sql = "SELECT Id FROM Hunter WHERE Username = @username;";
            command = new OracleCommand(sql, conn);

            command.Parameters.Add(new OracleParameter("@username", username));

            Hunter hunter = null;
            int id = -1;

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