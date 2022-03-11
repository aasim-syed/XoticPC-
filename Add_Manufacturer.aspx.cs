using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class Add_Manufacturer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        BindManufacturerRepeater();
    }

    protected void Addmanufacturer_Click(object sender, EventArgs e)
    {

        SqlConnection con = new SqlConnection();
        con.ConnectionString = "Data Source=AJITH\\SERVER;Initial Catalog='Xotic PC';Integrated Security=True";


        con.Open();

        string signup = "insert into manufacturer values('" + TextBox1.Text + "');";

        SqlCommand cmd = new SqlCommand(signup, con);
        int n = cmd.ExecuteNonQuery();

        if (n > 0)
        {
            Response.Write("<script> alert('Manufacturer added successfully');</script>");
            clear();
        }

        else
        {
            Response.Write("<script> alert('Unfortunately manufacturer could not be added ');</script>");
            clear();
        }

        con.Close();


        
        TextBox1.Focus();


    }

    private void clear()
    {
        TextBox1.Text = string.Empty;
    }

    private void BindManufacturerRepeater()
    {
        SqlConnection con = new SqlConnection();
        con.ConnectionString = "Data Source=AJITH\\SERVER;Initial Catalog='Xotic PC';Integrated Security=True";


        con.Open();

        SqlCommand cmd = new SqlCommand("select * from Manufacturer", con);

        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        sda.Fill(dt);
        Repeater1.DataSource = dt;
        Repeater1.DataBind();
        

    }

    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {

    }
}