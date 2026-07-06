using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
	// Token: 0x02000006 RID: 6
	public class adminbookissuing : Page
	{
		// Token: 0x06000021 RID: 33 RVA: 0x0000372A File Offset: 0x0000192A
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.GridView1.DataBind();
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x0000373F File Offset: 0x0000193F
		protected void Button1_Click(object sender, EventArgs e)
		{
			this.GetNames();
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00003748 File Offset: 0x00001948
		protected void Button3_Click(object sender, EventArgs e)
		{
			if (!this.CheckIfBookExist() || !this.CheckIfMemberExist())
			{
				base.Response.Write("<script>alert('Invalid Book ID or Member ID');</script>");
				return;
			}
			if (this.CheckIfIssueEntryExist())
			{
				base.Response.Write("<script>alert('This Member already has this book');</script>");
				return;
			}
			this.IssueBook();
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00003798 File Offset: 0x00001998
		protected void Button4_Click(object sender, EventArgs e)
		{
			if (!this.CheckIfBookExist() || !this.CheckIfMemberExist())
			{
				base.Response.Write("<script>alert('Invalid Book ID or Member ID');</script>");
				return;
			}
			if (this.CheckIfIssueEntryExist())
			{
				this.ReturnBook();
				return;
			}
			base.Response.Write("<script>alert('This Entry Does Not Exist');</script>");
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000037E8 File Offset: 0x000019E8
		private void IssueBook()
		{
			try
			{
				using (SqlConnection con = new SqlConnection(this.strcon))
				{
					con.Open();
					using (SqlCommand cmd = new SqlCommand("\r\n                        INSERT INTO book_issue_tbl (member_id, member_name, book_id, book_name, issue_date, due_date)\r\n                        VALUES (@member_id, @member_name, @book_id, @book_name, @issue_date, @due_date)", con))
					{
						cmd.Parameters.AddWithValue("@member_id", this.TextBox2.Text.Trim());
						cmd.Parameters.AddWithValue("@member_name", this.TextBox3.Text.Trim());
						cmd.Parameters.AddWithValue("@book_id", this.TextBox1.Text.Trim());
						cmd.Parameters.AddWithValue("@book_name", this.TextBox4.Text.Trim());
						cmd.Parameters.AddWithValue("@issue_date", this.TextBox5.Text.Trim());
						cmd.Parameters.AddWithValue("@due_date", this.TextBox6.Text.Trim());
						cmd.ExecuteNonQuery();
					}
					using (SqlCommand cmd2 = new SqlCommand("UPDATE book_master_tbl SET current_stock = current_stock - 1 WHERE book_id=@book_id", con))
					{
						cmd2.Parameters.AddWithValue("@book_id", this.TextBox1.Text.Trim());
						cmd2.ExecuteNonQuery();
					}
					base.Response.Write("<script>alert('Book Issued Successfully');</script>");
					this.GridView1.DataBind();
				}
			}
			catch (Exception ex)
			{
				base.Response.Write("<script>alert('Error: " + ex.Message.Replace("'", "\\'") + "');</script>");
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000039EC File Offset: 0x00001BEC
		private void ReturnBook()
		{
			try
			{
				using (SqlConnection con = new SqlConnection(this.strcon))
				{
					con.Open();
					using (SqlCommand cmd = new SqlCommand("DELETE FROM book_issue_tbl WHERE book_id=@book_id AND member_id=@member_id", con))
					{
						cmd.Parameters.AddWithValue("@book_id", this.TextBox1.Text.Trim());
						cmd.Parameters.AddWithValue("@member_id", this.TextBox2.Text.Trim());
						int result = cmd.ExecuteNonQuery();
						if (result > 0)
						{
							using (SqlCommand cmd2 = new SqlCommand("UPDATE book_master_tbl SET current_stock = current_stock + 1 WHERE book_id=@book_id", con))
							{
								cmd2.Parameters.AddWithValue("@book_id", this.TextBox1.Text.Trim());
								cmd2.ExecuteNonQuery();
							}
							base.Response.Write("<script>alert('Book Returned Successfully');</script>");
							this.GridView1.DataBind();
						}
						else
						{
							base.Response.Write("<script>alert('Error - Invalid details');</script>");
						}
					}
				}
			}
			catch (Exception ex)
			{
				base.Response.Write("<script>alert('" + ex.Message.Replace("'", "\\'") + "');</script>");
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00003B54 File Offset: 0x00001D54
		private bool CheckIfIssueEntryExist()
		{
			bool flag;
			try
			{
				using (SqlConnection con = new SqlConnection(this.strcon))
				{
					using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM book_issue_tbl WHERE member_id=@member_id AND book_id=@book_id", con))
					{
						cmd.Parameters.AddWithValue("@member_id", this.TextBox2.Text.Trim());
						cmd.Parameters.AddWithValue("@book_id", this.TextBox1.Text.Trim());
						con.Open();
						int count = (int)cmd.ExecuteScalar();
						flag = count > 0;
					}
				}
			}
			catch
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00003C18 File Offset: 0x00001E18
		private bool CheckIfBookExist()
		{
			bool flag;
			try
			{
				using (SqlConnection con = new SqlConnection(this.strcon))
				{
					using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM book_master_tbl WHERE book_id=@book_id AND current_stock > 0", con))
					{
						cmd.Parameters.AddWithValue("@book_id", this.TextBox1.Text.Trim());
						con.Open();
						int count = (int)cmd.ExecuteScalar();
						flag = count > 0;
					}
				}
			}
			catch
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00003CBC File Offset: 0x00001EBC
		private bool CheckIfMemberExist()
		{
			bool flag;
			try
			{
				using (SqlConnection con = new SqlConnection(this.strcon))
				{
					using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM member_master_tbl WHERE member_id=@member_id", con))
					{
						cmd.Parameters.AddWithValue("@member_id", this.TextBox2.Text.Trim());
						con.Open();
						int count = (int)cmd.ExecuteScalar();
						flag = count > 0;
					}
				}
			}
			catch
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00003D60 File Offset: 0x00001F60
		private void GetNames()
		{
			try
			{
				using (SqlConnection con = new SqlConnection(this.strcon))
				{
					con.Open();
					using (SqlCommand cmd = new SqlCommand("SELECT book_name FROM book_master_tbl WHERE book_id=@book_id", con))
					{
						cmd.Parameters.AddWithValue("@book_id", this.TextBox1.Text.Trim());
						object result = cmd.ExecuteScalar();
						this.TextBox4.Text = ((result != null) ? result.ToString() : null) ?? "";
						if (result == null)
						{
							base.Response.Write("<script>alert('Wrong Book ID');</script>");
						}
					}
					using (SqlCommand cmd2 = new SqlCommand("SELECT full_name FROM member_master_tbl WHERE member_id=@member_id", con))
					{
						cmd2.Parameters.AddWithValue("@member_id", this.TextBox2.Text.Trim());
						object result2 = cmd2.ExecuteScalar();
						this.TextBox3.Text = ((result2 != null) ? result2.ToString() : null) ?? "";
						if (result2 == null)
						{
							base.Response.Write("<script>alert('Wrong Member ID');</script>");
						}
					}
				}
			}
			catch (Exception ex)
			{
				base.Response.Write("<script>alert('" + ex.Message.Replace("'", "\\'") + "');</script>");
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00003F10 File Offset: 0x00002110
		protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			try
			{
				DateTime dt;
				if (e.Row.RowType == DataControlRowType.DataRow && DateTime.TryParse(e.Row.Cells[5].Text, out dt) && DateTime.Today > dt)
				{
					e.Row.BackColor = Color.PaleVioletRed;
				}
			}
			catch (Exception ex)
			{
				base.Response.Write("<script>alert('" + ex.Message.Replace("'", "\\'") + "');</script>");
			}
		}

		// Token: 0x04000029 RID: 41
		private string strcon
		{
			get
			{
				var conSetting = ConfigurationManager.ConnectionStrings["con"] ?? ConfigurationManager.ConnectionStrings["elibraryDBConnectionString"] ?? ConfigurationManager.ConnectionStrings["LibraryDB"];
				return conSetting != null ? conSetting.ConnectionString : "Data Source=library123.mssql.somee.com;Initial Catalog=library123;User ID=shivam12345_SQLLogin_4;Password=Radha@soami123;";
			}
		}

		// Token: 0x0400002A RID: 42
		protected TextBox TextBox2;

		// Token: 0x0400002B RID: 43
		protected TextBox TextBox1;

		// Token: 0x0400002C RID: 44
		protected Button Button1;

		// Token: 0x0400002D RID: 45
		protected TextBox TextBox3;

		// Token: 0x0400002E RID: 46
		protected TextBox TextBox4;

		// Token: 0x0400002F RID: 47
		protected TextBox TextBox5;

		// Token: 0x04000030 RID: 48
		protected TextBox TextBox6;

		// Token: 0x04000031 RID: 49
		protected Button Button3;

		// Token: 0x04000032 RID: 50
		protected Button Button4;

		// Token: 0x04000033 RID: 51
		protected SqlDataSource SqlDataSource1;

		// Token: 0x04000034 RID: 52
		protected GridView GridView1;
	}
}
