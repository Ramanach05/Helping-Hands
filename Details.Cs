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
    public partial class Details : System.Web.UI.Page
    {
        string k = ConfigurationManager.ConnectionStrings["cs"].ToString();
        internal void Bind()
        {
            SqlConnection con = new SqlConnection(k);
            SqlCommand cmd = new SqlCommand("insertusers1", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds,"users1");
            gridviewusers1.DataSource = ds.Tables["users1"];
            gridviewusers1.DataBind();

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bind();

            }
        }
        protected void gridviewusers1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            SqlConnection con = new SqlConnection(k);
            if (e.CommandName == "cmddelete")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = (gridviewusers1).Rows[index];
                Label l1 = (Label)row.FindControl("Label1");
                con.Open();
                int @uid = int.Parse(l1.Text);
                SqlCommand Cmd = new SqlCommand("deleted", con);
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@a", uid);
                int i = Cmd.ExecuteNonQuery();
                if (i == 1)
                {
                    lblmsg.Visible = true;
                    lblmsg.Text = "delete susessefully";
                }
                else
                {
                    lblmsg.Visible = true;
                    lblmsg.Text = "deletefailed";
                }
                con.Close();
            }
            if (e.CommandName == "cmdedit")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = (gridviewusers1).Rows[index];
                Label l1 = (Label)row.FindControl("Label1");
                Label l2 = (Label)row.FindControl("Label2");
                Label l3 = (Label)row.FindControl("Label3");
                Label l4 = (Label)row.FindControl("Label4");
                Label l5 = (Label)row.FindControl("Label5");
                Label l6 = (Label)row.FindControl("Label6");
                Label l7 = (Label)row.FindControl("Label7");
                Label l8 = (Label)row.FindControl("Label8");
                Label l9 = (Label)row.FindControl("Label9");
                Session["uid"] = l1.Text;
                Session["uname"] = l2.Text;
                Session["bgroup"] = l3.Text;
                Session["phno"] = l4.Text;
                Session["email"] = l5.Text;
                Session["city"] = l6.Text;
                Session["value"] = "update";
                Session["gender"] = l7.Text;
                Session["lang"] = l9.Text;
                Session["state"] = l8.Text;
               Response.Redirect("Register.aspx");
            }
        }
    }
}
