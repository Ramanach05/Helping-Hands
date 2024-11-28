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
    public partial class Login : System.Web.UI.Page
    {
        string k = ConfigurationManager.ConnectionStrings["cs"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnlogin_Click(object sender, EventArgs e)
        {
            int userid = int.Parse(txtid.Text);
            string pwd = txtpwd.Text;
            SqlConnection con = new SqlConnection(k);
            con.Open();
            SqlCommand cmd = new SqlCommand("pro_check1", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@a", userid);
            cmd.Parameters.AddWithValue("@pwd", pwd);
            int i = (int)cmd.ExecuteScalar();
            if(i>0)
            {
                Session["id"] = txtid.Text;
                //lblmsg.Text = Session["id"].ToString();
                Response.Redirect("AWelcome.aspx");
            }
            else
            {
                lblmsg.Text = "invalid Details";
            }
        }
    }
}
