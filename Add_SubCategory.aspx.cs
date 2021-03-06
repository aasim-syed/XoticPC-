using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class Add_SubCategory : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindMainCat();
            BindSubCategoryRepeater();
        }
    }

    protected void SubCategory_SelectedIndexChanged(object sender,EventArgs e)
    {

        int maincatid = int.Parse(DropDownList1.SelectedItem.Value);


    }

    protected void Addsubcategory_Click(object sender, EventArgs e)
    {

        SqlConnection con = new SqlConnection();
        con.ConnectionString = "Data Source=AJITH\\SERVER;Initial Catalog='Xotic PC';Integrated Security=True";


        con.Open();

        string signup = "insert into SubCategory values('" + TextBox1.Text + "','" + int.Parse(DropDownList1.SelectedValue) + "');";

        SqlCommand cmd = new SqlCommand(signup, con);
        int n = cmd.ExecuteNonQuery();

        if (n > 0)
        {
            Response.Write("<script> alert('Sub Category added successfully');</script>");
            clear();
        }

        else
        {
            Response.Write("<script> alert('Unfortunately sub category could not be added ');</script>");
            clear();
        }

        con.Close();

        DropDownList1.ClearSelection();
        DropDownList1.Items.FindByValue("0").Selected = true;

        TextBox1.Focus();

        BindSubCategoryRepeater(); 
    }

    private void clear()
    {
        TextBox1.Text = string.Empty;
    }


    private void BindMainCat()
    {

        SqlConnection con = new SqlConnection();
        con.ConnectionString = "Data Source=AJITH\\SERVER;Initial Catalog='Xotic PC';Integrated Security=True";


        con.Open();

        SqlCommand cmd = new SqlCommand("select * from Category", con);
        SqlDataAdapter sda = new SqlDataAdapter(cmd);

        DataTable dt = new DataTable();

        sda.Fill(dt);

        if(dt.Rows.Count != 0)
        {
            DropDownList1.DataSource = dt;
            DropDownList1.DataTextField = "catname";
            DropDownList1.DataValueField = "catid";
            DropDownList1.DataBind();
            DropDownList1.Items.Insert(0, new ListItem("-Select-","0"));
        }


    }

    private void BindSubCategoryRepeater()
    {
        SqlConnection con = new SqlConnection();
        con.ConnectionString = "Data Source=AJITH\\SERVER;Initial Catalog='Xotic PC';Integrated Security=True";


        con.Open();

        SqlCommand cmd = new SqlCommand("select A.*,B.* from SubCategory A inner join Category B on B.catid = A.maincatid", con);

        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        sda.Fill(dt);
        Repeater1.DataSource = dt;
        Repeater1.DataBind();


    }



}