using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace project1
{
    public partial class ULogin : System.Web.UI.Page
    {
        string k = ConfigurationManager.ConnectionStrings["cs"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string uname = txtuname.Text;
            string pwd = txtpwd.Text;
            SqlConnection con = new SqlConnection(k);
            con.Open();
            SqlCommand cmd = new SqlCommand("countusers1", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@a", uname);
            cmd.Parameters.AddWithValue("@b", pwd);
            int i =(int) cmd.ExecuteScalar();
            if(i>0)
            {
                Session["uname"] = txtuname.Text;
                Session["pwd"] = txtpwd.Text;
                Response.Redirect("UWelcome.aspx");
            }
            else
            {
                lblmsg.Text = "Invalid Details";
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            Session["uname"] = txtuname.Text;
            Response.Redirect("forget.aspx");

        }
    }
}
