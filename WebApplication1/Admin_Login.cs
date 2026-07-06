using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
	// Token: 0x02000003 RID: 3
	public class Admin_Login : Page
	{
		// Token: 0x06000003 RID: 3 RVA: 0x0000205A File Offset: 0x0000025A
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.Session.Clear();
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002070 File Offset: 0x00000270
		protected void Button1_Click(object sender, EventArgs e)
		{
			try
			{
				using (SqlConnection con = new SqlConnection(this.strcon))
				{
					con.Open();
					string query = "SELECT username, full_name FROM admin_login_tbl WHERE username=@username AND password=@password";
					using (SqlCommand cmd = new SqlCommand(query, con))
					{
						cmd.Parameters.AddWithValue("@username", this.TextBox1.Text.Trim());
						cmd.Parameters.AddWithValue("@password", this.TextBox2.Text.Trim());
						using (SqlDataReader dr = cmd.ExecuteReader())
						{
							if (dr.HasRows)
							{
								dr.Read();
								string username = ((dr["username"] != DBNull.Value) ? dr["username"].ToString() : "");
								string fullname = ((dr["full_name"] != DBNull.Value) ? dr["full_name"].ToString() : "");
								this.Session["username"] = username;
								this.Session["fullname"] = fullname;
								this.Session["role"] = "admin";
								base.Response.Redirect("homepage.aspx");
							}
							else
							{
								base.Response.Write("<script>alert('Invalid credentials');</script>");
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				base.Response.Write("<script>alert('Error: " + ex.Message.Replace("'", "\\'") + "');</script>");
			}
		}

		// Token: 0x04000001 RID: 1
		private string strcon
		{
			get
			{
				var conSetting = ConfigurationManager.ConnectionStrings["con"] ?? ConfigurationManager.ConnectionStrings["elibraryDBConnectionString"] ?? ConfigurationManager.ConnectionStrings["LibraryDB"];
				return conSetting != null ? conSetting.ConnectionString : "Data Source=library123.mssql.somee.com;Initial Catalog=library123;User ID=shivam12345_SQLLogin_4;Password=Radha@soami123;";
			}
		}

		// Token: 0x04000002 RID: 2
		protected TextBox TextBox1;

		// Token: 0x04000003 RID: 3
		protected TextBox TextBox2;

		// Token: 0x04000004 RID: 4
		protected Button Button1;
	}
}
