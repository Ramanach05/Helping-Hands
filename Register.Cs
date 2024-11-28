using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace project1
{
    public partial class Register : System.Web.UI.Page
    {
        string k = ConfigurationManager.ConnectionStrings["cs"].ToString();
        internal void Bind()
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
            ddropstate.Items.Insert(0, "--select state--");
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bind();
                BindBlood();
                if (Session["uid"] != null)
                {
                    txtuid.Visible = true;
                    Label11.Visible = true;
                    //txtpwd.Visible = false;
                    //Label2.Visible = false;
                    txtcpwd.Visible = false;
                    Label3.Visible = false;
                    txtuid.Text = Session["uid"].ToString();
                    txtuname.Text = Session["uname"].ToString();
                    ddropbloodg.SelectedItem.Text = Session["bgroup"].ToString();
                    ddropstate.SelectedItem.Text = Session["state"].ToString();
                    txtphno.Text = Session["phno"].ToString();
                    txtemail.Text = Session["email"].ToString();
                    btnRegister.Text = Session["value"].ToString();
                    if (Session["gender"].ToString() == "male")
                        rbtmale.Checked = true;
                    if (Session["gender"].ToString() == "female")
                        rbtfemale.Checked = true;
                    string[] lang = Session["lang"].ToString().Split();
                    foreach(string a in lang)
                    {
                        if (a == chtel.Text)
                            chtel.Checked = true;
                        if (a == cheng.Text)
                            cheng.Checked = true;
                        if (a == chhin.Text)
                            chhin.Checked = true;
                    }
                }
            }
        }    
        protected void btnRegister_Click(object sender, EventArgs e)
        {
            string uname = txtuname.Text;
            string pwd = txtpwd.Text;
            string cpwd = txtcpwd.Text;
            long phno = long.Parse(txtphno.Text);
            string email = txtemail.Text;
            string gender = "";
            if (rbtmale.Checked == true)
            {
                gender = rbtmale.Text;
            }
           else if (rbtfemale.Checked == true)
            {
                gender = rbtfemale.Text;
            }
            string lang = "";
            if (chtel.Checked == true)
            {
                lang = lang + chtel.Text + ",";
            }
            if (chhin.Checked == true)
            {
                lang = chhin.Text;
            }
            if (cheng.Checked == true)
            {
                lang = lang + cheng.Text + ",";
            }

            SqlConnection con = new SqlConnection(k);
            
            if (btnRegister.Text == "Register")
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("inusers1", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@a", uname);
                cmd.Parameters.AddWithValue("@b", pwd);
                cmd.Parameters.AddWithValue("@c", gender);
                cmd.Parameters.AddWithValue("@d", lang);
                cmd.Parameters.AddWithValue("@e", ddropstate.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@f", ddropcity.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@g", ddropbloodg.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@h", phno);
                cmd.Parameters.AddWithValue("@i", email);
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    Response.Write("<script>alert('inserted Records')</script>");
                }
                else
                {
                    Response.Write("inserted Not Records");
                }
                con.Close();
            }
            else if(btnRegister.Text=="update")
            {
                int uid = int.Parse(txtuid.Text);
                con.Open();  
               SqlCommand cmd = new SqlCommand("upd2", con);
                cmd.CommandType = CommandType.StoredProcedure;
                 cmd.Parameters.AddWithValue("@a",uid);
                 cmd.Parameters.AddWithValue("@b", uname);
                 cmd.Parameters.AddWithValue("@c",pwd);
                 cmd.Parameters.AddWithValue("@d",gender);
                cmd.Parameters.AddWithValue("@e", lang);
                cmd.Parameters.AddWithValue("@f", ddropstate.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@g", ddropcity.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@h", ddropbloodg.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@i", phno);
                cmd.Parameters.AddWithValue("@j", email);
                 int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    Response.Write("<script>alert('update Records');</script>");
                }
                else
                {
                    Response.Write("<script>alert('update Not Records');</script>");
                }
                con.Close();
            }
        }
        protected void ddropstate_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(k);
            con.Open();
            SqlCommand cmd = new SqlCommand("select*from city where sid='" + ddropstate.SelectedItem.Value + "'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            ddropcity.DataSource = ds;
            ddropcity.DataTextField = "cname";
            ddropcity.DataValueField = "cid";
            ddropcity.DataBind();
            ddropcity.Items.Insert(0, "--select city--");
          
        }
        internal void BindBlood()
        {
            SqlConnection con = new SqlConnection(k);
            con.Open();   
            SqlCommand cmd = new SqlCommand("selbloodg", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            ddropbloodg.DataTextField = "groupname";
            ddropbloodg.DataSource = ds;
            ddropbloodg.DataBind();
            ddropbloodg.Items.Insert(0, "--selectBlood--");
        }
        protected void btnclear_Click(object sender, EventArgs e)
        {
            txtuname.Text = "";
            txtpwd.Text = "";
            txtcpwd.Text = "";
            ddropbloodg.DataValueField = "bid";
            rbtfemale.Checked = false;
            rbtmale.Checked = false;
            chtel.Checked = false;
            chhin.Checked = false;
            cheng.Checked = false;
            ddropstate.SelectedIndex = 0;
            ddropcity.SelectedIndex = 0;
            ddropbloodg.SelectedIndex = 0;
            txtphno.Text = "";
            txtphno.Text = "";
        }

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if((args.Value.Length>=6)&&(args.Value.Contains("_"))||(args.Value.Contains("@")))
            {
                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
            }
        }

        protected void CustomValidator2_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if((args.Value.StartsWith("9"))||(args.Value.StartsWith("8"))
                ||(args.Value.StartsWith("7"))||(args.Value.StartsWith("6"))&&(args.Value.Length==10))
            {
                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
            }
        }

        protected void CustomValidator3_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if(args.Value.Length>=6)
            {
                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
            }
        }
    }
}
