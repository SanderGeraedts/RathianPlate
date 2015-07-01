using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RathianPlate
{
    /// <summary>
    /// Name: Set
    /// Contains data for a set of armor. Not yet implemented
    /// </summary>
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

        public Set(int id, string name)
        {
            this.id = id;
            this.name = name;

            this.pieces = loadPieces();
        }

        public Set(int id, string name, List<Armor> pieces)
        {
            this.id = id;
            this.name = name;

            if (checkValidity(pieces))
            {
                this.pieces = pieces;
            }
        }
        #endregion

        #region private methods

        /// <summary>
        /// Name: checkValidity
        /// Checks if there are no double armor pieces, e.g. 2 helmets.
        /// </summary>
        private bool checkValidity(List<Armor> pieces)
        {
            int helm = 0;
            int chest = 0;
            int arms = 0;
            int pants = 0;
            int boots = 0;
            int talis = 0;

            foreach (Armor piece in pieces)
            {
                switch (piece.Piece)
                {
                    case "Helm":
                        helm++;
                        break;
                    case "Chest":
                        chest++;
                        break;
                    case "Arms":
                        arms++;
                        break;
                    case "Pants":
                        pants++;
                        break;
                    case "Boots":
                        boots++;
                        break;
                    case "Talisman":
                        talis++;
                        break;
                }
            }
            if(helm <= 1 && chest <= 1 && arms <= 1 && pants <= 1 && boots <= 1 && talis <= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private List<Armor> loadPieces()
        {
            return null;
        }
        #endregion

        #region public methods
        public bool ExportSet()
        {
            return false;
        }
        #endregion
    }
}