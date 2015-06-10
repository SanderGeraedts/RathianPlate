using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RathianPlate
{
    public class Set
    {
        #region fields

        private int id;
        private string name;
        private List<Armor> pieces;

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

        public List<Armor> Pieces
        {
            get { return pieces; }
            set { pieces = value; }
        }
        #endregion

        #region constructor

        #endregion

        #region private methods

        #endregion

        #region public methods

        #endregion
    }
}