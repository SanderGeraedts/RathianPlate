using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace RathianPlate
{
    ///<summary>
    /// Name: Database
    /// This class talks to the Database
    ///</summary>
    public class Database
    {
        private OracleConnection conn;

        public Database()
        {
            conn = new OracleConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DatabaseConnection"].ConnectionString);
        }

        ///<summary>
        /// Name: NonQueryBase
        /// This method is used for executing nonquery based SQL statements. 
        /// This drastically lowers the amount of copied code and makes the update 
        /// and insert statements more readable
        ///</summary>
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

        ///<summary>
        /// Name: CheckLogin
        /// This method checks whether or not the user used the right credentials. 
        /// It returns a filled Hunter object if true or a null Hunter object when false.
        ///</summary>
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

        ///<summary>
        /// Name: RegisterHunter
        /// This method has 2 functions. First it inserts the newly registered hunter into the database.
        /// Then it retrieves the id of the hunter to create the Hunter object. If the Id couldn't be
        /// retrieved, it creates a null object.
        ///</summary>
        public Hunter RegisterHunter(string name, string username, string password, string hr)
        {
            //insert Hunter into Database
            string sql = "INSERT INTO Hunter(Name, Username, Password, HR) VALUES (@name, @username, @password, @hr);";
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

        ///<summary>
        /// Name: LoadHunters
        /// Retrieves the hunters who are participating in a hunt.
        ///</summary>
        public List<Hunter> LoadHunters(int huntId)
        {
            string sql = "SELECT u.Id, u.Name, u.Password, u.Skype, u.HR FROM Hunter u, Party p, Hunt h WHERE p.HunterId = u.Id AND p.HuntId = @id";
            OracleCommand command = new OracleCommand(sql, conn);

            command.Parameters.Add(new OracleParameter("@id", huntId));

            List<Hunter> hunters = new List<Hunter>();
            Hunter hunter = null;

            int id = -1;
            string name = "";
            string username = "";
            string password = "";
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
                    username = Convert.ToString(reader["Username"]);
                    password = Convert.ToString(reader["Password"]);
                    hr = Convert.ToString(reader["HR"]);

                    if (id != -1)
                    {
                        hunter = new Hunter(id, name, username, password, hr);
                    }

                    hunters.Add(hunter);
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

            return hunters;
        }

        ///<summary>
        /// Name: RegisterHunt
        /// This method registers a hunt.
        ///</summary>
        public void RegisterHunt(Hunt hunt)
        {
            string sql = "INSERT INTO Hunt(StartTime, Description, HallId, QuestId) VALUES (@startTime, @description, @hallId, @questId);";
            OracleCommand command = new OracleCommand(sql, conn);

            command.Parameters.Add(new OracleParameter("@startTime", hunt.StartTime));
            command.Parameters.Add(new OracleParameter("@description", hunt.Description));
            command.Parameters.Add(new OracleParameter("@hallId", hunt.HallId));
            command.Parameters.Add(new OracleParameter("@questId", hunt.Quest.Id));

            NonQueryBase(command);
        }

        ///<summary>
        /// Name: LoadQuests
        /// Retrieves the quest for a single hunt.
        ///</summary>
        public Quest LoadQuest(int huntId)
        {
            string sql = "SELECT u.Id, u.Name, u.Password, u.Skype, u.HR FROM Hunter u, Party p, Hunt h WHERE p.HunterId = u.Id AND p.HuntId = @id";
            OracleCommand command = new OracleCommand(sql, conn);

            command.Parameters.Add(new OracleParameter("@id", huntId));

            List<Quest> quests = new List<Quest>();
            Quest quest = null;

            int id = -1;
            string name = "";
            string username = "";
            string password = "";
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
                    username = Convert.ToString(reader["Username"]);
                    password = Convert.ToString(reader["Password"]);
                    hr = Convert.ToString(reader["HR"]);

                    if (id != -1)
                    {
                        //hunter = new Hunter(id, name, username, password, hr);
                    }

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

            return quest;
        }

        ///<summary>
        /// Name: LoadMonsters
        /// Retrieves the monsters for a single quest.
        /// It also retrieves the hunter who posted the reply
        ///</summary>
        public List<Monster> LoadMonsters(int questId)
        {
            string sql = "SELECT m.Id, m.Name, m.Rank, m.SignatureMove, m.Description FROM Monster m, Quest q, MonsterPerQuest c WHERE m.Id = c.MonsterId AND c.QuestId = @id";
            OracleCommand command = new OracleCommand(sql, conn);
            
            command.Parameters.Add(new OracleParameter("@id", questId));

            int id = -1;
            string name = "";
            string rank = "";
            string signatureMove = "";
            string description = "";

            Monster monster = null;
            List<Monster> monsters = new List<Monster>();

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
                    rank = Convert.ToString(reader["Rank"]);
                    signatureMove = Convert.ToString(reader["SignatureMove"]);
                    description = Convert.ToString(reader["Description"]);

                    if (id != -1)
                    {
                        monster = new Monster(id, name, rank, signatureMove, description);
                    }

                    if (monster != null)
                    {
                        monsters.Add(monster);
                    }
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

            return monsters;
        }

        ///<summary>
        /// Name: LoadMessages
        /// Retrieves the messages for a single hunt.
        ///</summary>
        public List<Message> LoadMessages(int huntId)
        {
            return new List<Message>();
        }
    }
}