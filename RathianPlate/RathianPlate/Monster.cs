using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RathianPlate
{
    /// <summary>
    /// Name: Monster
    /// Stores the needed data for a Monster. Although it's not fully used in the application layer, it's ready to be implemented.
    /// </summary>
    public class Monster
    {
        #region fields

        private int id;
        private string name;
        private string rank;
        private string signatureMove;
        private string description;

        #endregion

        #region properties
        public int Id { get { return id; } }
        public string Name { get { return name; } }
        public string Rank { get { return rank; } }
        public string SignatureMove { get { return signatureMove; } }
        public string Description { get { return description; } }
        #endregion

        #region constructor

        public Monster(int id, string name, string rank, string signatureMove, string description)
        {
            this.id = id;
            this.name = name;
            this.rank = rank;
            this.signatureMove = signatureMove;
            this.description = description;
        }
        #endregion

        #region private methods

        #endregion

        #region public methods

        #endregion
    }
}