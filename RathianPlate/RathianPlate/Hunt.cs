using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RathianPlate
{
    public class Hunt
    {
        #region fields
        private int id;
        private string name;
        private DateTime startTime;
        private string hallId;
        private List<Hunter> hunters;
        private Quest quest;
        private List<Message> messages; 
        #endregion
        #region properties
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public DateTime StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }

        public string HallId
        {
            get { return hallId; }
            set { hallId = value; }
        }
        
        public List<Hunter> Hunters
        {
            get { return hunters; }
            set { hunters = value; }
        }

        public Quest Quest
        {
            get { return quest; }
            set { quest = value; }
        }

        public List<Message> Messages
        {
            get { return messages; }
            set { messages = value; }
        }

        public int NumberHunters
        {
            get { return hunters.Count(); }
        }
        #endregion
        #region constructor

        public Hunt(int id, string name, DateTime startTime, string hallId)
        {
            this.id = id;
            this.name = name;
            this.startTime = startTime;
            this.hallId = hallId;

            this.hunters = new List<Hunter>();
            this.messages = new List<Message>();
        }
        #endregion
        #region public methods
        public void LoadHunters()
        {
            Database database = new Database();
            this.hunters = database.LoadHunters(this.id);
        }
        public void LoadQuests()
        {
            
        }
        public void LoadMessages()
        {
            
        }
        #endregion
    }
}