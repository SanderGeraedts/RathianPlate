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
            conn = new OracleConnection("Data Source=//fhictora01.fhict.local:1521/fhictora;User ID=dbi289783;Password=ftyACFwVgk");
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

        /// <summary>
        /// Name: timestampToDateTime
        /// Converts a timestamp string to a DateTime
        /// </summary>
        private DateTime timestampToDateTime(string timestamp)
        {
            DateTime dateTime;

            int day = 0;
            int month = 3;
            int year = 6;

            int hour = 11;
            int min = 14;

            day = Convert.ToInt32(timestamp.Substring(day, 2));
            month = Convert.ToInt32(timestamp.Substring(month, 2));
            year = Convert.ToInt32(timestamp.Substring(year, 4));
            hour = Convert.ToInt32(timestamp.Substring(hour, 2));
            min = Convert.ToInt32(timestamp.Substring(min, 2));

            dateTime = new DateTime(year, month, day, hour, min, 0);

            return dateTime;
        }

        /// <summary>
        /// Name: dateTimeToTimestamp
        /// Converts a DateTime to a timestamp string 
        /// </summary>
        private string dateTimeToTimestamp(DateTime dateTime)
        {
            string timestamp = "";

            long ticks = dateTime.ToUniversalTime().Ticks - DateTime.Parse("01/01/1970 00:00:00").Ticks;
            ticks /= 10000000;//Convert windows ticks to seconds

            timestamp = ticks.ToString();

            return timestamp;
        }

        ///<summary>
        /// Name: CheckLogin
        /// This method checks whether or not the user used the right credentials. 
        /// It returns a filled Hunter object if true or a null Hunter object when false.
        ///</summary>
        public Hunter CheckLogin(string username, string password)
        {
            string sql = "SELECT * FROM Hunter WHERE Username = :username AND Password = :password";
            OracleCommand command = new OracleCommand(sql, conn);

            command.Parameters.Add(new OracleParameter("username", username));
            command.Parameters.Add(new OracleParameter("password", password));

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
            string sql = "INSERT INTO Hunter(Name, Username, Password, HR) VALUES (:name, :username, :password, :hr)";
            OracleCommand command = new OracleCommand(sql, conn);

            command.Parameters.Add(new OracleParameter("name", name));
            command.Parameters.Add(new OracleParameter("username", username));
            command.Parameters.Add(new OracleParameter("password", password));
            command.Parameters.Add(new OracleParameter("hr", hr));

            NonQueryBase(command);

            //retrieve id to create Hunter object
            sql = "SELECT Id FROM Hunter WHERE Username = :username";
            command = new OracleCommand(sql, conn);

            command.Parameters.Add(new OracleParameter("username", username));

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
            string sql = "SELECT DISTINCT u.Id, u.Name, u.Username, u.Password, u.HR FROM Hunter u, Party p, Hunt h WHERE p.HunterId = u.Id AND p.HuntId = :id";
            OracleCommand command = new OracleCommand(sql, conn);

            command.Parameters.Add(new OracleParameter("id", huntId));

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

        public Hunter LoadHunter(int hunterId)
        {
            string sql = "SELECT * FROM Hunter WHERE Id = :id";
            OracleCommand command = new OracleCommand(sql, conn);

            command.Parameters.Add(new OracleParameter("id", hunterId));

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

            return hunter;
        }

        ///<summary>
        /// Name: LoadHunts
        /// Retrieves all the hunts from the database. This also retrieves the Hunters and the messages of that hunt
        /// </summary>
        public Hunt LoadHunt(int huntId)
        {
            string sql = "SELECT DISTINCT * FROM HUNT WHERE Id = :id";
            OracleCommand command = new OracleCommand(sql, conn);

            command.Parameters.Add(new OracleParameter("id", huntId));

            Hunt hunt = null;

            int id = -1;
            DateTime dateTime = new DateTime();
            string description = "";
            string hallId = "";

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
                    dateTime = timestampToDateTime(Convert.ToString(reader["StartTime"]));
                    description = Convert.ToString(reader["Description"]);
                    hallId = Convert.ToString(reader["HallId"]);
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

            hunt = new Hunt(id, dateTime, description, hallId);

            hunt.Quest = LoadQuest(hunt.Id);
            hunt.Messages = LoadMessages(hunt.Id);
            hunt.Hunters = LoadHunters(hunt.Id);

            return hunt;
        }

        ///<summary>
        /// Name: LoadHunts
        /// Retrieves all the hunts from the database. This also retrieves the Hunters and the messages of that hunt
        /// </summary>
        public List<Hunt> LoadHunts()
        {
            string sql = "SELECT * FROM HUNT";
            OracleCommand command = new OracleCommand(sql, conn);

            List<Hunt> hunts = new List<Hunt>();
            Hunt hunt = null;

            int id = -1;
            DateTime dateTime = new DateTime();
            string description = "";
            string hallId = "";

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
                    dateTime = timestampToDateTime(Convert.ToString(reader["StartTime"]));
                    description = Convert.ToString(reader["Description"]);
                    hallId = Convert.ToString(reader["HallId"]);

                    hunt = new Hunt(id, dateTime, description, hallId);
                    hunts.Add(hunt);
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

            foreach (Hunt h in hunts)
            {
                h.Quest = LoadQuest(h.Id);
                h.Messages = LoadMessages(h.Id);
                h.Hunters = LoadHunters(h.Id);
            }

            return hunts;
        }

        /// <summary>
        /// Name: LoadHunts
        /// This methods retrieves the registered hunts of a single hunter
        /// </summary>
        /// <param name="hunterId"></param>
        /// <returns></returns>
        public List<Hunt> LoadHunts(int hunterId)
        {
            string sql = "SELECT DISTINCT h.Id, h.StartTime, h.Description, h.HallId FROM HUNT h, Party p WHERE p.HuntId = h.Id AND p.HunterId = :id";
            OracleCommand command = new OracleCommand(sql, conn);

            command.Parameters.Add(new OracleParameter("id", hunterId));

            List<Hunt> hunts = new List<Hunt>();
            Hunt hunt = null;

            int id = -1;
            DateTime dateTime = new DateTime();
            string description = "";
            string hallId = "";

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
                    dateTime = timestampToDateTime(Convert.ToString(reader["StartTime"]));
                    description = Convert.ToString(reader["Description"]);
                    hallId = Convert.ToString(reader["HallId"]);

                    hunt = new Hunt(id, dateTime, description, hallId);
                    hunts.Add(hunt);
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

            foreach (Hunt h in hunts)
            {
                h.Quest = LoadQuest(h.Id);
                h.Messages = LoadMessages(h.Id);
                h.Hunters = LoadHunters(h.Id);
            }

            return hunts;
        }

        ///<summary>
        /// Name: RegisterHunt
        /// This method registers a hunt.
        ///</summary>
        public void RegisterHunt(Hunt hunt)
        {
            string sql = "INSERT INTO Hunt(StartTime, Description, HallId, QuestId) VALUES (:startTime, :description, :hallId, :questId);";
            OracleCommand command = new OracleCommand(sql, conn);

            command.Parameters.Add(new OracleParameter("startTime", dateTimeToTimestamp(hunt.StartTime)));
            command.Parameters.Add(new OracleParameter("description", hunt.Description));
            command.Parameters.Add(new OracleParameter("hallId", hunt.HallId));
            command.Parameters.Add(new OracleParameter("questId", hunt.Quest.Id));

            NonQueryBase(command);
        }

        public List<Quest> LoadQuests()
        {
            string sql = "SELECT DISTINCT * FROM Quest";
            OracleCommand command = new OracleCommand(sql, conn);

            List<Quest> quests = new List<Quest>();
            Quest quest = null;

            int id = -1;
            string name = "";
            string objective = "";
            string reward = "";
            string fee = "";
            bool keyQuest = false;
            string rank = "";
            string type = "";

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
                    objective = Convert.ToString(reader["Objective"]);
                    reward = Convert.ToString(reader["Reward"]);
                    fee = Convert.ToString(reader["Fee"]);

                    if (Convert.ToString(reader["KeyQuest"]) == "Y")
                    {
                        keyQuest = true;
                    }
                    else
                    {
                        keyQuest = false;
                    }

                    rank = Convert.ToString(reader["Rank"]);
                    type = Convert.ToString(reader["Type"]);

                    quest = new Quest(id, name, objective, reward, fee, keyQuest, rank, type);
                    quests.Add(quest);
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
            foreach (Quest q in quests)
            {
                q.Monsters = LoadMonsters(q.Id);
            }
                
            return quests;
        }

        ///<summary>
        /// Name: LoadQuest
        /// Retrieves the quest for a single hunt.
        ///</summary>
        public Quest LoadQuest(int huntId)
        {
            string sql = "SELECT DISTINCT q.Id, q.Name, q.Objective, q.Reward, q.Fee, q.KeyQuest, q.Rank, q.Type FROM Quest q, HuntQuary h WHERE h.QuestId = q.Id AND h.HuntId = :id";
            OracleCommand command = new OracleCommand(sql, conn);

            command.Parameters.Add(new OracleParameter("id", huntId));

            Quest quest = null;

            int id = -1;
            string name = "";
            string objective = "";
            string reward = "";
            string fee = "";
            bool keyQuest = false;
            string rank = "";
            string type = "";

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
                    objective = Convert.ToString(reader["Objective"]);
                    reward = Convert.ToString(reader["Reward"]);
                    fee = Convert.ToString(reader["Fee"]);

                    if (Convert.ToString(reader["KeyQuest"]) == "Y")
                    {
                        keyQuest = true;
                    }
                    else
                    {
                        keyQuest = false;
                    }

                    rank = Convert.ToString(reader["Rank"]);
                    type = Convert.ToString(reader["Type"]);
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

            quest = new Quest(id, name, objective, reward, fee, keyQuest, rank, type);
            quest.Monsters = LoadMonsters(quest.Id);
            return quest;
        }

        ///<summary>
        /// Name: LoadMonsters
        /// Retrieves the monsters for a single quest.
        ///</summary>
        public List<Monster> LoadMonsters(int questId)
        {
            string sql = "SELECT DISTINCT m.Id, m.Name, m.Rank, m.SignatureMove, m.Description FROM Monster m, Quest q, MonstersPerQuest c WHERE m.Id = c.MonsterId AND c.QuestId = :id";
            OracleCommand command = new OracleCommand(sql, conn);
            
            command.Parameters.Add(new OracleParameter("id", questId));

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
        /// It also retrieves the hunter who posted the reply
        ///</summary>
        public List<Message> LoadMessages(int huntId)
        {
            string sql = "SELECT * FROM Message WHERE HUNTID = :id";
            OracleCommand command = new OracleCommand(sql, conn);

            command.Parameters.Add(new OracleParameter("id", huntId));

            List<Message> messages = new List<Message>();
            Message message = null;

            int id = -1;
            DateTime sentOn = DateTime.Now;
            string text = "";
            Hunter hunter = null;

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
                    sentOn = timestampToDateTime(Convert.ToString(reader["SentOn"]));
                    text = Convert.ToString(reader["Message"]);
                    hunter = new Hunter(Convert.ToInt32(reader["HunterId"]), "", "", "", "");

                    message = new Message(id, sentOn, text, hunter);
                    messages.Add(message);
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

            foreach (Message m in messages)
            {
                m.Hunter = LoadHunter(m.Hunter.Id);
            }

            return messages;
        }

        public void SentMessage(Message message, int huntId)
        {
            string sql = "INSERT INTO Message(HunterId, HuntId, SentOn, Message) VALUES (:hunterId, :huntId, :sentOn, :message)";
            OracleCommand command = new OracleCommand(sql, conn);

            command.Parameters.Add("hunterId", message.Hunter.Id);
            command.Parameters.Add("huntId", huntId);
            command.Parameters.Add("sentOn", message.SentOn);
            command.Parameters.Add("message", message.Text);

            NonQueryBase(command);
        }

        public int GetNextHuntId()
        {
            string sql = "SELECT MAX(id) FROM HUNT";
            OracleCommand command = new OracleCommand(sql, conn);

            int id = 0;

            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                id = Convert.ToInt32(command.ExecuteScalar());
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

            return id;
        }

        public Hunt CallHunt(Hunt hunt, Hunter hunter)
        {
            //Inserting the Hunt
            string sql = "INSERT INTO Hunt(StartTime, Description, HallId) VALUES (:starttime, :description, :hallId)";
            OracleCommand command = new OracleCommand(sql, conn);

            command.Parameters.Add("starttime", hunt.StartTime);
            command.Parameters.Add("description", hunt.Description);
            command.Parameters.Add("hallId", hunt.HallId);

            NonQueryBase(command);

            //retrieving the huntId
            hunt.Id = GetNextHuntId();

            //inserting Party
            sql = "INSERT INTO Party(HuntId, HunterId) VALUES (:huntid, :hunterId)";
            command = new OracleCommand(sql, conn);

            command.Parameters.Add("huntId", hunt.Id);
            command.Parameters.Add("hunterId", hunter.Id);

            NonQueryBase(command);

            //inserting Quest
            sql = "INSERT INTO HuntQuary(HuntId, QuestId) VALUES (:huntId, :questId)";
            command = new OracleCommand(sql, conn);

            command.Parameters.Add("huntId", hunt.Id);
            command.Parameters.Add("questId", hunt.Quest.Id);

            NonQueryBase(command);

            return hunt;
        }

        public void JoinHunt(Hunt hunt, Hunter hunter)
        {
            string sql = "INSERT INTO Party(HuntId, HunterId) VALUES (:huntid, :hunterid)";
            OracleCommand command = new OracleCommand(sql, conn);

            command.Parameters.Add("huntid", hunt.Id);
            command.Parameters.Add("hunterid", hunter.Id);

            NonQueryBase(command);
        }

        public void LeaveHunt(Hunt hunt, Hunter hunter)
        {
            string sql = "DELETE FROM Party WHERE HuntId = :huntid AND HunterId = :hunterid";
            OracleCommand command = new OracleCommand(sql, conn);

            command.Parameters.Add("huntid", hunt.Id);
            command.Parameters.Add("hunterid", hunter.Id);

            NonQueryBase(command);
        }
    }
}