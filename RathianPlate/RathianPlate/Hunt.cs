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
        private string description;
        private DateTime startTime;
        private string hallId;
        private Quest quest;
        private List<Hunter> hunters;
        private List<Message> messages; 
        #endregion
        #region properties
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
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

        public Quest Quest
        {
            get { return quest; }
            set { quest = value; }
        }
        
        public List<Hunter> Hunters
        {
            get { return hunters; }
            set { hunters = value; }
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

        public Hunt(int id, DateTime startTime, string description, string hallId)
        {
            this.id = id;
            this.description = description;
            this.startTime = startTime;
            this.hallId = hallId;

            this.hunters = new List<Hunter>();
            this.messages = new List<Message>();
        }
        #endregion
        #region public methods

        public void RegisterHunt()
        {
            
        }
        #endregion
    }
}