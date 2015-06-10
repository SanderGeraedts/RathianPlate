using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RathianPlate
{
    public class Message
    {
        #region fields

        private int id;
        private DateTime sentOn;
        private string text;

        #endregion

        #region properties
        public int Id { get { return id; } }
        public DateTime SentOn { get { return sentOn; } }
        public string Text { get { return text; } }
        #endregion

        #region constructor

        public Message(int id, DateTime sentOn, string text)
        {
            this.id = id;
            this.sentOn = sentOn;
            this.text = text;
        }
        #endregion

        #region private methods

        #endregion

        #region public methods

        #endregion
    }
}