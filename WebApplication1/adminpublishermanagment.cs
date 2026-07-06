using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
	// Token: 0x02000008 RID: 8
	public class adminpublishermanagment : Page
	{
		// Token: 0x06000039 RID: 57 RVA: 0x000045FB File Offset: 0x000027FB
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.GridView1.DataBind();
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00004610 File Offset: 0x00002810
		protected void Button2_Click(object sender, EventArgs e)
		{
			if (this.CheckIfPublisherExists())
			{
				base.Response.Write("<script>alert('Publisher with this ID already exists.');</script>");
				return;
			}
			this.AddNewPublisher();
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00004631 File Offset: 0x00002831
		protected void Button3_Click(object sender, EventArgs e)
		{
			if (this.CheckIfPublisherExists())
			{
				this.UpdatePublisher();
				return;
			}
			base.Response.Write("<script>alert('Publisher does not exist.');</script>");
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00004652 File Offset: 0x00002852
		protected void Button4_Click(object sender, EventArgs e)
		{
			if (this.CheckIfPublisherExists())
			{
				this.DeletePublisher();
				return;
			}
			base.Response.Write("<script>alert('Publisher does not exist.');</script>");
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00004673 File Offset: 0x00002873
		protected void Button1_Click(object sender, EventArgs e)
		{
			this.GetPublisherByID();
		}

		// Token: 0x0600003E RID: 62 RVA: 0x0000467C File Offset: 0x0000287C
		private void GetPublisherByID()
		{
			try
			{
				using (SqlConnection con = new SqlConnection(this.strcon))
				{
					con.Open();
					using (SqlCommand cmd = new SqlCommand("SELECT publisher_name FROM publisher_master_tbl WHERE publisher_id=@id", con))
					{
						cmd.Parameters.AddWithValue("@id", this.TextBox1.Text.Trim());
						object result = cmd.ExecuteScalar();
						if (result != null)
						{
							this.TextBox2.Text = result.ToString();
						}
						else
						{
							base.Response.Write("<script>alert('Invalid Publisher ID');</script>");
						}
					}
				}
			}
			catch (Exception ex)
			{
				base.Response.Write("<script>alert('" + ex.Message.Replace("'", "\\'") + "');</script>");
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x0000476C File Offset: 0x0000296C
		private void DeletePublisher()
		{
			try
			{
				using (SqlConnection con = new SqlConnection(this.strcon))
				{
					con.Open();
					using (SqlCommand cmd = new SqlCommand("DELETE FROM publisher_master_tbl WHERE publisher_id=@id", con))
					{
						cmd.Parameters.AddWithValue("@id", this.TextBox1.Text.Trim());
						cmd.ExecuteNonQuery();
					}
				}
				base.Response.Write("<script>alert('Publisher Deleted Successfully');</script>");
				this.ClearForm();
				this.GridView1.DataBind();
			}
			catch (Exception ex)
			{
				base.Response.Write("<script>alert('" + ex.Message.Replace("'", "\\'") + "');</script>");
			}
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00004854 File Offset: 0x00002A54
		private void UpdatePublisher()
		{
			try
			{
				using (SqlConnection con = new SqlConnection(this.strcon))
				{
					con.Open();
					using (SqlCommand cmd = new SqlCommand("UPDATE publisher_master_tbl SET publisher_name=@name WHERE publisher_id=@id", con))
					{
						cmd.Parameters.AddWithValue("@name", this.TextBox2.Text.Trim());
						cmd.Parameters.AddWithValue("@id", this.TextBox1.Text.Trim());
						cmd.ExecuteNonQuery();
					}
				}
				base.Response.Write("<script>alert('Publisher Updated Successfully');</script>");
				this.ClearForm();
				this.GridView1.DataBind();
			}
			catch (Exception ex)
			{
				base.Response.Write("<script>alert('" + ex.Message.Replace("'", "\\'") + "');</script>");
			}
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00004960 File Offset: 0x00002B60
		private void AddNewPublisher()
		{
			try
			{
				using (SqlConnection con = new SqlConnection(this.strcon))
				{
					con.Open();
					using (SqlCommand cmd = new SqlCommand("INSERT INTO publisher_master_tbl(publisher_id, publisher_name) VALUES(@id,@name)", con))
					{
						cmd.Parameters.AddWithValue("@id", this.TextBox1.Text.Trim());
						cmd.Parameters.AddWithValue("@name", this.TextBox2.Text.Trim());
						cmd.ExecuteNonQuery();
					}
				}
				base.Response.Write("<script>alert('Publisher Added Successfully');</script>");
				this.ClearForm();
				this.GridView1.DataBind();
			}
			catch (Exception ex)
			{
				base.Response.Write("<script>alert('" + ex.Message.Replace("'", "\\'") + "');</script>");
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00004A6C File Offset: 0x00002C6C
		private bool CheckIfPublisherExists()
		{
			bool flag;
			try
			{
				using (SqlConnection con = new SqlConnection(this.strcon))
				{
					con.Open();
					using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM publisher_master_tbl WHERE publisher_id=@id", con))
					{
						cmd.Parameters.AddWithValue("@id", this.TextBox1.Text.Trim());
						int count = (int)cmd.ExecuteScalar();
						flag = count > 0;
					}
				}
			}
			catch (Exception ex)
			{
				base.Response.Write("<script>alert('" + ex.Message.Replace("'", "\\'") + "');</script>");
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00004B40 File Offset: 0x00002D40
		private void ClearForm()
		{
			this.TextBox1.Text = "";
			this.TextBox2.Text = "";
		}

		// Token: 0x04000047 RID: 71
		private string strcon
		{
			get
			{
				var conSetting = ConfigurationManager.ConnectionStrings["con"] ?? ConfigurationManager.ConnectionStrings["elibraryDBConnectionString"] ?? ConfigurationManager.ConnectionStrings["LibraryDB"];
				return conSetting != null ? conSetting.ConnectionString : "Data Source=library123.mssql.somee.com;Initial Catalog=library123;User ID=shivam12345_SQLLogin_4;Password=Radha@soami123;";
			}
		}

		// Token: 0x04000048 RID: 72
		protected TextBox TextBox1;

		// Token: 0x04000049 RID: 73
		protected Button Button1;

		// Token: 0x0400004A RID: 74
		protected TextBox TextBox2;

		// Token: 0x0400004B RID: 75
		protected Button Button2;

		// Token: 0x0400004C RID: 76
		protected Button Button3;

		// Token: 0x0400004D RID: 77
		protected Button Button4;

		// Token: 0x0400004E RID: 78
		protected SqlDataSource SqlDataSource1;

		// Token: 0x0400004F RID: 79
		protected GridView GridView1;
	}
}
