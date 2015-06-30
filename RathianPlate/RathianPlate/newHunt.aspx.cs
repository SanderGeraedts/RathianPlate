using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RathianPlate
{
    public partial class newHunt : System.Web.UI.Page
    {
        private Control control;
        private List<Quest> quests;

        protected void Page_Init(object sender, EventArgs e)
        {
            quests = new List<Quest>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            errMessage.Visible = false;
            errDescription.Visible = false;

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

            quests = control.LoadQuests();
            if (!IsPostBack)
            {
                ddlQuests.Items.Clear();
                foreach (Quest quest in quests)
                {
                    ddlQuests.Items.Add(quest.MapName);
                }
            }
        }

        protected void btnCallHunt_OnClick(object sender, EventArgs e)
        {
            if (tbHours.Text != "" && tbHallId.Text != "" && tbDescription.Text.Length < 255)
            {
                double hours = Convert.ToDouble(tbHours.Text);
                string hallid = tbHallId.Text;
                string text = tbDescription.Text;
                Quest quest = null;

                foreach (Quest q in quests)
                {
                    if (ddlQuests.SelectedItem.Text == q.MapName)
                    {
                        quest = q;
                        break;
                    }
                }

                DateTime timeHunt = DateTime.Now.AddHours(hours);

                Hunt hunt = new Hunt(0, timeHunt, text, hallid);

                hunt.Quest = quest;

                hunt = control.CallHunt(hunt);

                Response.Redirect("Hunts.aspx?query=" + hunt.Id);
            }
            else
            {
                if (tbHours.Text == "" || tbHallId.Text == "")
                {
                    errMessage.Visible = true;
                }
                if (tbDescription.Text.Length >= 255)
                {
                    errDescription.Visible = true;
                }
            }
        }
    }
}