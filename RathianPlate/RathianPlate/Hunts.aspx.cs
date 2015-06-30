using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RathianPlate
{
    public partial class Hunts : System.Web.UI.Page
    {
        private Control control;
        private List<Hunt> hunts;
        private Hunt hunt;

        protected void Page_Init(object sender, EventArgs e)
        {
            hunts = new List<Hunt>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //check login
            if (Session["Controller"] != null)
            {
                control = (Control)Session["Controller"];

                if (control.LoggedIn == null)
                {
                    control.LastPage = HttpContext.Current.Request.Url.PathAndQuery;
                    Session["Controller"] = control;
                    Response.Redirect("Login.aspx");
                }
            }
            else
            {
                control = new Control();
                control.LastPage = HttpContext.Current.Request.Url.PathAndQuery;
                Session["Controller"] = control;
                Response.Redirect("Login.aspx");
            }

            //check query
            if (Request.QueryString["query"] == null)
            {
                NoQuery.Visible = true;
                YesQuery.Visible = false;

                hunts = control.LoadHunts(control.LoggedIn.Id);

                rptJoinedHunts.DataSource = hunts;
                rptJoinedHunts.DataBind();
            }
            else
            {
                NoQuery.Visible = false;
                YesQuery.Visible = true;

                int query = Convert.ToInt32(Request.QueryString["query"]);

                hunt = control.GetHunt(query);

                lblQuest.Text = hunt.Quest.Objective;
                lblDescription.Text = hunt.Description;
                lblStartTime.Text = hunt.StartTime.ToString();
                lblHallId.Text = hunt.HallId;

                foreach (Hunter hunter in hunt.Hunters)
                {
                    if (hunter.Id == control.LoggedIn.Id)
                    {
                        btnJoinHunt.Text = "Leave Hunt";
                        break;
                    }
                    else
                    {
                        btnJoinHunt.Text = "Join Hunt";
                    }
                }

                rptHuntersHunt.DataSource = hunt.Hunters;
                rptHuntersHunt.DataBind();

                rptMessagesHunt.DataSource = hunt.Messages;
                rptMessagesHunt.DataBind();
            }
        }

        protected void btnMessage_OnClick(object sender, EventArgs e)
        {
            int id = 0;
            DateTime sentOn = DateTime.Now;
            string text = tbMessage.Text;
            Hunter hunter = control.LoggedIn;

            Message message = new Message(id, sentOn, text, hunter);

            control.SentMessage(message, hunt);

            Response.Redirect(HttpContext.Current.Request.Url.PathAndQuery);
        }

        protected void btnNewHunt_Click(object sender, EventArgs e)
        {
            Session["Controller"] = control;
            Response.Redirect("newHunt.aspx");
        }

        protected void btnJoinHunt_OnClick(object sender, EventArgs e)
        {
            if (btnJoinHunt.Text == "Join Hunt")
            {
                control.JoinHunt(hunt);
                Response.Redirect(HttpContext.Current.Request.Url.PathAndQuery);
            }
            else
            {
                control.LeaveHunt(hunt);
                Response.Redirect(HttpContext.Current.Request.Url.PathAndQuery);
            }
        }
    }
}