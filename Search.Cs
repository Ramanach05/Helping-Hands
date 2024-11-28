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
    public partial class Search : System.Web.UI.Page
    {
        string k = ConfigurationManager.ConnectionStrings["cs"].ToString();
        internal void Bind()
        {
            SqlConnection con = new SqlConnection(k);
            con.Open();
            SqlCommand cmd = new SqlCommand("selectblood", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            ddropbloodg.DataSource = ds;
            ddropbloodg.DataTextField = "groupname";
            ddropbloodg.DataValueField = "bid";
            ddropbloodg.DataBind();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bind();
                Bindstate();
            }
        }
        internal void Bindstate()
        {
            SqlConnection con = new SqlConnection(k);
            con.Open();
            SqlCommand cmd = new SqlCommand("instate1", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            ddropstate.DataSource = ds;
            ddropstate.DataTextField = "sname";
            ddropstate.DataValueField = "sid";
            ddropstate.DataBind();
            ddropstate.Items.Insert(0, "--select--");
        }

        protected void ddropstate_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(k);
            con.Open();
            SqlCommand cmd = new SqlCommand("discity", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@a", ddropstate.SelectedItem.Value);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            ddropcity.DataSource = ds;
            ddropcity.DataTextField = "cname";
            ddropcity.DataValueField = "cid";
            ddropcity.DataBind();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string Bgroup = ddropbloodg.SelectedItem.Text;
            string state = ddropstate.SelectedItem.Text;
            string city = ddropcity.SelectedItem.Text;
            SqlConnection con = new SqlConnection(k);
            con.Open();
       SqlCommand cmd = new SqlCommand("search1", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@a", Bgroup);
            cmd.Parameters.AddWithValue("@b", state);
            cmd.Parameters.AddWithValue("@c", city);
            int i = (int)cmd.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable ds = new DataTable();
            da.Fill(ds);
            if (ds.Rows.Count > 0)
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
            }
            else
            {
                GridView1.DataSource = null;
                Response.Write("<script>alert('Invalid Seaching Details');</script>");
            }

        }
    }
}
