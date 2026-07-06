using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
	// Token: 0x02000010 RID: 16
	public class viewbooks : Page
	{
		// Token: 0x0600006A RID: 106 RVA: 0x00005E0A File Offset: 0x0000400A
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.BindRepeater();
			}
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00005E1C File Offset: 0x0000401C
		private void BindRepeater()
		{
			try
			{
				using (SqlConnection con = new SqlConnection(this.strcon))
				{
					con.Open();
					string query = "SELECT * FROM book_master_tbl";
					using (SqlDataAdapter da = new SqlDataAdapter(query, con))
					{
						DataTable dt = new DataTable();
						da.Fill(dt);
						this.Repeater1.DataSource = dt;
						this.Repeater1.DataBind();
					}
				}
			}
			catch (Exception ex)
			{
				base.Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
			}
		}

		// Token: 0x0400007F RID: 127
		private string strcon
		{
			get
			{
				var conSetting = ConfigurationManager.ConnectionStrings["con"] ?? ConfigurationManager.ConnectionStrings["elibraryDBConnectionString"] ?? ConfigurationManager.ConnectionStrings["LibraryDB"];
				return conSetting != null ? conSetting.ConnectionString : "Data Source=library123.mssql.somee.com;Initial Catalog=library123;User ID=shivam12345_SQLLogin_4;Password=Radha@soami123;";
			}
		}

		// Token: 0x04000080 RID: 128
		protected Repeater Repeater1;
	}
}
