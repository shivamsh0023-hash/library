using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
	// Token: 0x0200000D RID: 13
	public class Userlogin : Page
	{
		// Token: 0x0600005B RID: 91 RVA: 0x00002050 File Offset: 0x00000250
		protected void Page_Load(object sender, EventArgs e)
		{
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000050A0 File Offset: 0x000032A0
		protected void Button1_Click(object sender, EventArgs e)
		{
			using (SqlConnection con = new SqlConnection(this.strcon))
			{
				try
				{
					con.Open();
					SqlCommand cmd = new SqlCommand("SELECT * FROM member_master_tbl WHERE member_id=@member_id AND password=@password", con);
					cmd.Parameters.AddWithValue("@member_id", this.TextBox1.Text.Trim());
					cmd.Parameters.AddWithValue("@password", this.TextBox2.Text.Trim());
					using (SqlDataReader dr = cmd.ExecuteReader())
					{
						if (dr.HasRows)
						{
							dr.Read();
							string status = dr["account_status"].ToString();
							if (status != "Active")
							{
								base.Response.Write("<script>alert('Your account is not active. Contact admin.');</script>");
							}
							else
							{
								this.Session["username"] = dr["member_id"].ToString();
								this.Session["fullname"] = dr["full_name"].ToString();
								this.Session["role"] = "user";
								this.Session["status"] = status;
								base.Response.Write("<script>alert('Login Successful');</script>");
								base.Response.Redirect("homepage.aspx");
							}
						}
						else
						{
							base.Response.Write("<script>alert('Invalid Credentials');</script>");
						}
					}
				}
				catch (Exception ex)
				{
					base.Response.Write("<script>alert('" + ex.Message + "');</script>");
				}
			}
		}

		// Token: 0x0400005F RID: 95
		private string strcon
		{
			get
			{
				var conSetting = ConfigurationManager.ConnectionStrings["con"] ?? ConfigurationManager.ConnectionStrings["elibraryDBConnectionString"] ?? ConfigurationManager.ConnectionStrings["LibraryDB"];
				return conSetting != null ? conSetting.ConnectionString : "Data Source=library123.mssql.somee.com;Initial Catalog=library123;User ID=shivam12345_SQLLogin_4;Password=Radha@soami123;";
			}
		}

		// Token: 0x04000060 RID: 96
		protected TextBox TextBox1;

		// Token: 0x04000061 RID: 97
		protected TextBox TextBox2;

		// Token: 0x04000062 RID: 98
		protected Button Button1;
	}
}
