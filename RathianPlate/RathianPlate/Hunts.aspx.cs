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

        protected void Page_Load(object sender, EventArgs e)
        {
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
        }
    }
}