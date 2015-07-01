using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RathianPlate
{
    public partial class Register : System.Web.UI.Page
    {
        private Control control;

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
            if (!IsPostBack)
            {
                errMessage.Visible = false;
            }
        }

        protected void btnRegister_OnClick(object sender, EventArgs e)
        {
            if (tbName.Text != "" && tbUsername.Text != "" && tbPassword.Text != "" && tbPassword2.Text != "")
            {
                if (tbPassword.Text == tbPassword2.Text)
                {
                    string name = tbName.Text;
                    string hr = tbHR.Text;
                    string username = tbUsername.Text;
                    string password = tbPassword.Text;
                    if (password.Length > 7)
                    {
                        if (control.RegisterHunter(name, username, password, hr))
                        {
                            if (control.LastPage != null || control.LastPage == "")
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
                            errMessage.InnerText = "The username is already in use. Please pick another username.";
                            errMessage.Visible = true;
                        }
                    }
                    else
                    {
                        errMessage.InnerText = "The password needs to be atleast 8 characters or more.";
                        errMessage.Visible = true;
                    }
                }
                else
                {
                    errMessage.InnerText = "The 2 passwords were different.";
                    errMessage.Visible = true;
                }
            }
            else
            {
                errMessage.InnerText = "Some required fields were not filled in.";
                errMessage.Visible = true;
            }
        }
    }
}