using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RathianPlate
{
    public partial class Login : System.Web.UI.Page
    {
        private Control control;

        protected void Page_Init(object sender, EventArgs e)
        {
            errMessage.Visible = false;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Controller"] != null)
            {
                control = (Control) Session["Controller"];

                if (control.LoggedIn == null)
                {
                    LogIn.Visible = true;
                    LogOut.Visible = false;
                }
                else
                {
                    LogIn.Visible = false;
                    LogOut.Visible = true;
                    tbLoggedInUser.Text = control.LoggedIn.Name;
                }
            }
            else
            {
                control = new Control(); 
                LogIn.Visible = true;
                LogOut.Visible = false;
            }
        }

        protected void btnLogin_OnClick(object sender, EventArgs e)
        {
            string username = tbUsername.Text;
            string password = tbPassword.Text;
            
            if (control.CheckLogIn(username, password))
            {
                Session["Controller"] = this.control;
                if (control.LastPage != null && control.LastPage != "")
                {
                    Response.Redirect(control.LastPage);
                }
                else
                {
                    Response.Redirect("default.aspx");
                }
            }
            else
            {
                errMessage.Visible = true;
            }
            
        }

        protected void btnLogOut_OnClick(object sender, EventArgs e)
        {
            control.LoggedIn = null;
            Session["Controller"] = control;
            Response.Redirect("default.aspx");
        }
    }
}