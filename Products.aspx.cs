using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class Products : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindProductsRepeater();
        }
    }

    protected void BindProductsRepeater()
    {
        SqlConnection con = new SqlConnection();
        con.ConnectionString = "Data Source=AJITH\\SERVER;Initial Catalog='Xotic PC';Integrated Security=True";


        con.Open();

        SqlCommand cmd = new SqlCommand("SPBindAllProducts", con);

        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        sda.Fill(dt);
        Repeater1.DataSource = dt;
        Repeater1.DataBind();
 

    }
}