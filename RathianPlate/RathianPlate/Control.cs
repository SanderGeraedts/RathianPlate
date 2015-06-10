using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RathianPlate
{
    public class Control
    {
        #region fields

        private Hunter loggedIn;
        private Database database;

        #endregion

        #region properties

        public Hunter LoggedIn
        {
            get { return loggedIn; }
            set { loggedIn = value; }
        }
        #endregion

        #region constructor
        public Control()
        {
            
        }
        #endregion

        #region private methods

        #endregion

        #region public methods

        //<summary>
        // Name: CheckLogIn
        // This method tells the Database object to check the users credentials.
        // The database will return a filled hunter object if true or a null object
        // if false. If true, the user will be logged in.
        //</summary>
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

        //<summary>
        // Name: RegisterHunter
        // This method tells the Database object to register the hunter. If everything goes
        // right and the database object returns a (not null) hunter object, this method will
        // return true and the user will be logged in. Else it will return false.
        //</summary>
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

        public void ImportSet() //might not be added
        {
            
        }
        #endregion
    }
}