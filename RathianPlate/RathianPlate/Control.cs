using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RathianPlate
{
    ///<summary>
    /// Name: Control
    /// This class controls connection between the presentation, business and database layer 
    /// of the application.
    ///</summary>
    public class Control
    {
        #region fields
        private Database database;

        private Hunter loggedIn;
        private string lastPage;

        private List<Hunt> hunts; 

        #endregion

        #region properties

        public Hunter LoggedIn
        {
            get { return loggedIn; }
            set { loggedIn = value; }
        }

        public String LastPage
        {
            get { return lastPage; }
            set { lastPage = value; }
        }

        public List<Hunt> Hunts
        {
            get { return hunts; }
            set { hunts = value; }
        }
        #endregion

        #region constructor
        public Control()
        {
            database = new Database();
        }
        #endregion

        #region private methods

        #endregion

        #region public methods

        ///<summary>
        /// Name: CheckLogIn
        /// This method tells the Database object to check the users credentials.
        /// The database will return a filled hunter object if true or a null object
        /// if false. If true, the user will be logged in.
        ///</summary>
        public bool CheckLogIn(string username, string password)
        {
            Hunter hunter = database.CheckLogin(username, password);
            if (hunter != null)
            {
                loggedIn = hunter;
                return true;
            }
            else
            {
                return false;
            }
        }

        ///<summary>
        /// Name: RegisterHunter
        /// This method tells the Database object to register the hunter. If everything goes
        /// right and the database object returns a (not null) hunter object, this method will
        /// return true and the user will be logged in. Else it will return false.
        ///</summary>
        public bool RegisterHunter(string name, string username, string password, string hr)
        {
            Hunter hunter = database.RegisterHunter(name, username, password, hr);
            if (hunter != null)
            {
                loggedIn = hunter;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void LoadHunts()
        {
            this.Hunts = database.LoadHunts();
        }

        public List<Hunt> LoadHunts(int hunterId)
        {
            return database.LoadHunts(hunterId);
        }

        public List<Quest> LoadQuests()
        {
            return database.LoadQuests();
        }

        public Hunt GetHunt(int huntId)
        {
            return database.LoadHunt(huntId);
        }

        public Hunt CallHunt(Hunt hunt)
        {
            return database.CallHunt(hunt, loggedIn);
        }

        public void JoinHunt(Hunt hunt)
        {
            database.JoinHunt(hunt, loggedIn);
        }

        public void LeaveHunt(Hunt hunt)
        {
            database.LeaveHunt(hunt, loggedIn);
        }

        public void SentMessage(Message message, Hunt hunt)
        {
            database.SentMessage(message, hunt.Id);
        }

        public void ImportSet() //might not be added
        {
            
        }
        #endregion

        
    }
}