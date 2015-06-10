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
        private List<Quest> quests;
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

        public List<Quest> Quests
        {
            get { return quests; }
            set { quests = value; }
        }

        public List<Message> Messages
        {
            get { return messages; }
            set { messages = value; }
        }
        #endregion
        #region constructor

        public Hunt(int id, string name, DateTime startTime, string hallId)
        {
            this.id = id;
            this.name = name;
            this.startTime = startTime;
            this.hallId = hallId;

            this.hunters = loadHunters();
            this.quests = loadQuests();
            this.messages = loadMessages();
        }
        #endregion
        #region private methods
        private List<Hunter> loadHunters()
        {
            return null;
        }
        private List<Quest> loadQuests()
        {
            return null;
        }
        private List<Message> loadMessages()
        {
            return null;
        }
        #endregion
    }
}