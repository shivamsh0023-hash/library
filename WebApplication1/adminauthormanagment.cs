using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
	// Token: 0x02000004 RID: 4
	public class adminauthormanagment : Page
	{
		// Token: 0x06000006 RID: 6 RVA: 0x0000228A File Offset: 0x0000048A
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.BindGrid();
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000229A File Offset: 0x0000049A
		protected void Button2_Click(object sender, EventArgs e)
		{
			if (this.CheckIfAuthorExists())
			{
				base.Response.Write("<script>alert('Author with this ID already exists.');</script>");
				return;
			}
			this.AddNewAuthor();
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000022BB File Offset: 0x000004BB
		protected void Button3_Click(object sender, EventArgs e)
		{
			if (this.CheckIfAuthorExists())
			{
				this.UpdateAuthor();
				return;
			}
			base.Response.Write("<script>alert('Author does not exist.');</script>");
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000022DC File Offset: 0x000004DC
		protected void Button4_Click(object sender, EventArgs e)
		{
			if (this.CheckIfAuthorExists())
			{
				this.DeleteAuthor();
				return;
			}
			base.Response.Write("<script>alert('Author does not exist.');</script>");
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000022FD File Offset: 0x000004FD
		protected void Button1_Click(object sender, EventArgs e)
		{
			this.GetAuthorByID();
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002308 File Offset: 0x00000508
		private void BindGrid()
		{
			try
			{
				using (SqlConnection con = new SqlConnection(this.strcon))
				{
					using (SqlCommand cmd = new SqlCommand("SELECT * FROM author_master_tbl", con))
					{
						using (SqlDataAdapter da = new SqlDataAdapter(cmd))
						{
							DataTable dt = new DataTable();
							da.Fill(dt);
							this.GridView1.DataSource = dt;
							this.GridView1.DataBind();
						}
					}
				}
			}
			catch (Exception ex)
			{
				base.Response.Write("<script>alert('" + ex.Message.Replace("'", "\\'") + "');</script>");
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000023E8 File Offset: 0x000005E8
		private void GetAuthorByID()
		{
			try
			{
				using (SqlConnection con = new SqlConnection(this.strcon))
				{
					con.Open();
					using (SqlCommand cmd = new SqlCommand("SELECT author_name FROM author_master_tbl WHERE author_id=@author_id", con))
					{
						cmd.Parameters.AddWithValue("@author_id", this.TextBox1.Text.Trim());
						object result = cmd.ExecuteScalar();
						if (result != null)
						{
							this.TextBox2.Text = result.ToString();
						}
						else
						{
							base.Response.Write("<script>alert('Invalid Author ID');</script>");
						}
					}
				}
			}
			catch (Exception ex)
			{
				base.Response.Write("<script>alert('" + ex.Message.Replace("'", "\\'") + "');</script>");
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000024D8 File Offset: 0x000006D8
		private void AddNewAuthor()
		{
			try
			{
				using (SqlConnection con = new SqlConnection(this.strcon))
				{
					con.Open();
					using (SqlCommand cmd = new SqlCommand("INSERT INTO author_master_tbl(author_id, author_name) VALUES(@author_id, @author_name)", con))
					{
						cmd.Parameters.AddWithValue("@author_id", this.TextBox1.Text.Trim());
						cmd.Parameters.AddWithValue("@author_name", this.TextBox2.Text.Trim());
						cmd.ExecuteNonQuery();
					}
				}
				base.Response.Write("<script>alert('Author added successfully');</script>");
				this.ClearForm();
				this.BindGrid();
			}
			catch (Exception ex)
			{
				base.Response.Write("<script>alert('" + ex.Message.Replace("'", "\\'") + "');</script>");
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000025DC File Offset: 0x000007DC
		private void UpdateAuthor()
		{
			try
			{
				using (SqlConnection con = new SqlConnection(this.strcon))
				{
					con.Open();
					using (SqlCommand cmd = new SqlCommand("UPDATE author_master_tbl SET author_name=@author_name WHERE author_id=@author_id", con))
					{
						cmd.Parameters.AddWithValue("@author_id", this.TextBox1.Text.Trim());
						cmd.Parameters.AddWithValue("@author_name", this.TextBox2.Text.Trim());
						cmd.ExecuteNonQuery();
					}
				}
				base.Response.Write("<script>alert('Author updated successfully');</script>");
				this.ClearForm();
				this.BindGrid();
			}
			catch (Exception ex)
			{
				base.Response.Write("<script>alert('" + ex.Message.Replace("'", "\\'") + "');</script>");
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000026E0 File Offset: 0x000008E0
		private void DeleteAuthor()
		{
			try
			{
				using (SqlConnection con = new SqlConnection(this.strcon))
				{
					con.Open();
					using (SqlCommand cmd = new SqlCommand("DELETE FROM author_master_tbl WHERE author_id=@author_id", con))
					{
						cmd.Parameters.AddWithValue("@author_id", this.TextBox1.Text.Trim());
						cmd.ExecuteNonQuery();
					}
				}
				base.Response.Write("<script>alert('Author deleted successfully');</script>");
				this.ClearForm();
				this.BindGrid();
			}
			catch (Exception ex)
			{
				base.Response.Write("<script>alert('" + ex.Message.Replace("'", "\\'") + "');</script>");
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000027C4 File Offset: 0x000009C4
		private bool CheckIfAuthorExists()
		{
			bool flag;
			try
			{
				using (SqlConnection con = new SqlConnection(this.strcon))
				{
					con.Open();
					using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM author_master_tbl WHERE author_id=@author_id", con))
					{
						cmd.Parameters.AddWithValue("@author_id", this.TextBox1.Text.Trim());
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

		// Token: 0x06000011 RID: 17 RVA: 0x00002898 File Offset: 0x00000A98
		private void ClearForm()
		{
			this.TextBox1.Text = "";
			this.TextBox2.Text = "";
		}

		// Token: 0x04000005 RID: 5
		private string strcon
		{
			get
			{
				var conSetting = ConfigurationManager.ConnectionStrings["con"] ?? ConfigurationManager.ConnectionStrings["elibraryDBConnectionString"] ?? ConfigurationManager.ConnectionStrings["LibraryDB"];
				return conSetting != null ? conSetting.ConnectionString : "Data Source=library123.mssql.somee.com;Initial Catalog=library123;User ID=shivam12345_SQLLogin_4;Password=Radha@soami123;";
			}
		}

		// Token: 0x04000006 RID: 6
		protected TextBox TextBox1;

		// Token: 0x04000007 RID: 7
		protected Button Button1;

		// Token: 0x04000008 RID: 8
		protected TextBox TextBox2;

		// Token: 0x04000009 RID: 9
		protected Button Button2;

		// Token: 0x0400000A RID: 10
		protected Button Button3;

		// Token: 0x0400000B RID: 11
		protected Button Button4;

		// Token: 0x0400000C RID: 12
		protected SqlDataSource SqlDataSource1;

		// Token: 0x0400000D RID: 13
		protected GridView GridView1;
	}
}
