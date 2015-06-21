using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RathianPlate
{
    public class Hunter
    {
        #region fields

        private int id;
        private string name;
        private string username;
        private string password;
        private string hr;
        private List<Set> sets;
        private List<Hunt> hunts;

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
        public string Username
        {
            get { return username; }
            set { username = value; }
        }
        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        public string HR
        {
            get { return hr; }
            set { hr = value; }
        }
        public List<Set> Sets
        {
            get { return sets; }
            set { sets = value; }
        }
        public List<Hunt> Hunts
        {
            get { return hunts; }
            set { hunts = value; }
        }
        #endregion
        #region constructor

        public Hunter(int id, string name, string username, string password, string hr)
        {
            this.id = id;
            this.name = name;
            this.username = username;
            this.password = password;
            this.hr = hr;

            this.sets = new List<Set>();
            this.hunts = new List<Hunt>();
        }
        #endregion
        #region private methods
        #endregion
        #region public methods
        public void LoadSets()
        {

        }

        public void LoadHunts()
        {

        }
        public void JoinHunt(Hunt hunt)
        {
            
        }

        public void CallHunt(Hunt hunt)
        {
            
        }

        public void PlaceMessage(Message message, Hunt hunt)
        {
            
        }
        #endregion
    }
}