using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RathianPlate
{
    public partial class _default : System.Web.UI.Page
    {
        private Control control;

        protected void Page_Init(object sender, EventArgs e)
        {
            
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Controller"] != null)
            {
                control = (Control)Session["Controller"];
            }
            else
            {
                control = new Control();
            }

            control.LoadHunts();
            rptHunts.DataSource = control.Hunts;
            rptHunts.DataBind();
        }

    }
}