﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            if(Request.Cookies["email"] != null && Request.Cookies["password"] != null)
            {
                TextBox1.Text = Request.Cookies["email"].Value;
                TextBox2.Text = Request.Cookies["password"].Value;
                
                CheckBox2.Checked = true;
            
            }


        }


    }

    protected void Login_Click(object sender,EventArgs e)
    {

        SqlConnection con = new SqlConnection();
        con.ConnectionString = "Data Source=AJITH\\SERVER;Initial Catalog='Xotic PC';Integrated Security=True";

        con.Open();

        SqlCommand cmd = new SqlCommand("select * from users where email = @email and password = @password",con);

        cmd.Parameters.AddWithValue("@email", TextBox1.Text);
        cmd.Parameters.AddWithValue("@password", TextBox2.Text);

        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();

        sda.Fill(dt);

        if(dt.Rows.Count != 0)
        {
            if (CheckBox2.Checked)
            {

                Response.Cookies["email"].Value = TextBox1.Text;
                Response.Cookies["password"].Value = TextBox2.Text;

                Response.Cookies["email"].Expires = DateTime.Now.AddDays(10);
                Response.Cookies["password"].Expires = DateTime.Now.AddDays(10);


            }

            else
            {

                Response.Cookies["email"].Expires = DateTime.Now.AddDays(-1);
                Response.Cookies["password"].Expires = DateTime.Now.AddDays(-1);


            }

            string utype;
            utype = dt.Rows[0][4].ToString().Trim();

            if (utype == "User")
            {

                Session["email"] = TextBox1.Text;

                Response.Redirect("~/User_Home.aspx");

            }

            if (utype == "Admin")
            {

                Session["email"] = TextBox1.Text;

                Response.Redirect("~/Admin_Home.aspx");

            }

        }

        else
        {
            Label3.Text = "Invalid email or password";
        }
        
        
        clear();
        con.Close();



    }

    private void clear()
    {
        TextBox1.Text = string.Empty;
        TextBox2.Text = string.Empty;
        TextBox1.Focus();
    }

    protected void ForgotPassword_Click(object sender,EventArgs e)
    {
        Response.Redirect("~/Forgot_Password.aspx");
    }


}