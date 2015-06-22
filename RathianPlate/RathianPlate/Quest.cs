using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RathianPlate
{
    ///<summary>
    /// Name: Quest
    /// This class is required to post a hunt. 
    ///</summary>
    public class Quest
    {
        #region fields
        private int id;
        private string mapName;
        private Quest subQuest;
        private string objective;
        private string reward;
        private string fee;
        private bool keyQuest;
        private string rank;
        private string type;
        private List<Monster> monsters;
        #endregion
        #region properties
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string MapName
        {
            get { return mapName; }
            set { mapName = value; }
        }

        public Quest SubQuest
        {
            get { return subQuest; }
            set { subQuest = value; }
        }

        public string Objective
        {
            get { return objective; }
            set { objective = value; }
        }

        public string Reward
        {
            get { return reward; }
            set { reward = value; }
        }

        public string Fee
        {
            get { return fee; }
            set { fee = value; }
        }

        public bool KeyQuest
        {
            get { return keyQuest; }
            set { keyQuest = value; }
        }

        public string Rank
        {
            get { return rank; }
            set { rank = value; }
        }

        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        public List<Monster> Monsters
        {
            get { return monsters; }
            set { monsters = value; }
        }
        #endregion
        #region constructor

        public Quest(int id, string mapName, string objective, string reward, string fee, bool keyQuest, string rank, string type)
        {
            this.id = id;
            this.mapName = mapName;
            this.objective = objective;
            this.reward = reward;
            this.fee = fee;
            this.keyQuest = keyQuest;
            this.rank = rank;
            this.type = type;

            this.monsters = new List<Monster>();
        }
        #endregion
        #region private methods
        public List<Monster> LoadMonsters()
        {
            Database database = new Database();
            database.LoadMonsters(this.id);
        }

        public override string ToString()
        {
            string result = "";

            for (int i = 0; i < Monsters.Count()-1; i++)
            {
                result +=  monsters[i].Name + ", ";
            }

            result += monsters.Last().Name;

            return result;
        }
        #endregion
    }
}