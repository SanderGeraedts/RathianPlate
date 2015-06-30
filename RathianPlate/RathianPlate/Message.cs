using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RathianPlate
{
    ///<summary>
    /// Name: Message
    /// This class contains a message for a hunt
    ///</summary>
    public class Message
    {
        #region fields

        private int id;
        private DateTime sentOn;
        private string text;
        private Hunter hunter;

        #endregion

        #region properties
        public int Id { get { return id; } }
        public DateTime SentOn { get { return sentOn; } }
        public string Text { get { return text; } }
        public Hunter Hunter { get { return hunter; } set { hunter = value; }}
        #endregion

        #region constructor

        public Message(int id, DateTime sentOn, string text, Hunter hunter)
        {
            this.id = id;
            this.sentOn = sentOn;
            this.text = text;
            this.hunter = hunter;
        }
        #endregion

        #region private methods

        #endregion

        #region public methods

        #endregion
    }
}