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

        public bool CheckLogIn(string username, string password)
        {
            return false;
        }

        public Hunter RegisterHunter(string name, string username, string password, string hr)
        {
            return null;
        }

        public void ImportSet() //might not be added
        {
            
        }
        #endregion
    }
}