using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
	// Token: 0x0200000E RID: 14
	public class userprofile : Page
	{
		// Token: 0x0600005E RID: 94 RVA: 0x000052A0 File Offset: 0x000034A0
		protected void Page_Load(object sender, EventArgs e)
		{
			if (this.Session["username"] == null || string.IsNullOrEmpty(this.Session["username"].ToString()))
			{
				base.Response.Write("<script>alert('Session Expired. Login Again');</script>");
				base.Response.Redirect("UserLogin.aspx", false);
				this.Context.ApplicationInstance.CompleteRequest();
				return;
			}
			if (!base.IsPostBack)
			{
				this.getUserPersonalDetails();
			}
			this.getUserBooksData();
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00005324 File Offset: 0x00003524
		protected void Button1_Click(object sender, EventArgs e)
		{
			if (this.Session["username"] == null || string.IsNullOrEmpty(this.Session["username"].ToString()))
			{
				base.Response.Write("<script>alert('Session Expired. Login Again');</script>");
				base.Response.Redirect("UserLogin.aspx", false);
				this.Context.ApplicationInstance.CompleteRequest();
				return;
			}
			this.updateUserPersonalDetails();
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00005398 File Offset: 0x00003598
		private void updateUserPersonalDetails()
		{
			string password = (string.IsNullOrWhiteSpace(this.TextBox10.Text.Trim()) ? this.TextBox9.Text.Trim() : this.TextBox10.Text.Trim());
			if (string.IsNullOrEmpty(password))
			{
				base.Response.Write("<script>alert('Password cannot be blank');</script>");
				return;
			}
			try
			{
				using (SqlConnection con = new SqlConnection(this.strcon))
				{
					con.Open();
					SqlCommand cmd = new SqlCommand("\r\n                        UPDATE member_master_tbl \r\n                        SET full_name=@full_name, dob=@dob, contact_no=@contact_no, email=@email, \r\n                            state=@state, city=@city, pincode=@pincode, full_address=@full_address, \r\n                            password=@password\r\n                        WHERE member_id=@member_id", con);
					cmd.Parameters.AddWithValue("@full_name", this.TextBox1.Text.Trim());
					cmd.Parameters.AddWithValue("@dob", this.TextBox2.Text.Trim());
					cmd.Parameters.AddWithValue("@contact_no", this.TextBox3.Text.Trim());
					cmd.Parameters.AddWithValue("@email", this.TextBox4.Text.Trim());
					cmd.Parameters.AddWithValue("@state", this.DropDownList1.SelectedValue);
					cmd.Parameters.AddWithValue("@city", this.TextBox6.Text.Trim());
					cmd.Parameters.AddWithValue("@pincode", this.TextBox7.Text.Trim());
					cmd.Parameters.AddWithValue("@full_address", this.TextBox5.Text.Trim());
					cmd.Parameters.AddWithValue("@password", password);
					cmd.Parameters.AddWithValue("@member_id", this.Session["username"].ToString().Trim());
					int result = cmd.ExecuteNonQuery();
					if (result > 0)
					{
						base.Response.Write("<script>alert('Your Details Updated Successfully');</script>");
						this.getUserPersonalDetails();
					}
					else
					{
						base.Response.Write("<script>alert('Update Failed. Please Try Again');</script>");
					}
				}
			}
			catch (Exception ex)
			{
				base.Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000055F4 File Offset: 0x000037F4
		private void getUserPersonalDetails()
		{
			try
			{
				using (SqlConnection con = new SqlConnection(this.strcon))
				{
					con.Open();
					SqlCommand cmd = new SqlCommand("SELECT * FROM member_master_tbl WHERE member_id=@member_id", con);
					cmd.Parameters.AddWithValue("@member_id", this.Session["username"].ToString());
					SqlDataAdapter da = new SqlDataAdapter(cmd);
					DataTable dt = new DataTable();
					da.Fill(dt);
					if (dt.Rows.Count > 0)
					{
						this.TextBox1.Text = dt.Rows[0]["full_name"].ToString();
						this.TextBox2.Text = dt.Rows[0]["dob"].ToString();
						this.TextBox3.Text = dt.Rows[0]["contact_no"].ToString();
						this.TextBox4.Text = dt.Rows[0]["email"].ToString();
						this.DropDownList1.SelectedValue = dt.Rows[0]["state"].ToString().Trim();
						this.TextBox6.Text = dt.Rows[0]["city"].ToString();
						this.TextBox7.Text = dt.Rows[0]["pincode"].ToString();
						this.TextBox5.Text = dt.Rows[0]["full_address"].ToString();
						this.TextBox8.Text = dt.Rows[0]["member_id"].ToString();
						this.TextBox9.Text = dt.Rows[0]["password"].ToString();
						string status = dt.Rows[0]["account_status"].ToString().Trim().ToLower();
						this.Label.Text = dt.Rows[0]["account_status"].ToString().Trim();
						if (!(status == "active"))
						{
							if (!(status == "pending"))
							{
								if (!(status == "deactive"))
								{
									this.Label.Attributes.Add("class", "badge badge-pill badge-secondary");
								}
								else
								{
									this.Label.Attributes.Add("class", "badge badge-pill badge-danger");
								}
							}
							else
							{
								this.Label.Attributes.Add("class", "badge badge-pill badge-warning");
							}
						}
						else
						{
							this.Label.Attributes.Add("class", "badge badge-pill badge-success");
						}
					}
					else
					{
						base.Response.Write("<script>alert('User not found');</script>");
					}
				}
			}
			catch (Exception ex)
			{
				base.Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00005954 File Offset: 0x00003B54
		private void getUserBooksData()
		{
			try
			{
				using (SqlConnection con = new SqlConnection(this.strcon))
				{
					con.Open();
					SqlCommand cmd = new SqlCommand("SELECT * FROM book_issue_tbl WHERE member_id=@member_id", con);
					cmd.Parameters.AddWithValue("@member_id", this.Session["username"].ToString());
					SqlDataAdapter da = new SqlDataAdapter(cmd);
					DataTable dt = new DataTable();
					da.Fill(dt);
					this.GridView1.DataSource = dt;
					this.GridView1.DataBind();
				}
			}
			catch (Exception ex)
			{
				base.Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00005A20 File Offset: 0x00003C20
		protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			try
			{
				DateTime dueDate;
				if (e.Row.RowType == DataControlRowType.DataRow && DateTime.TryParse(e.Row.Cells[5].Text, out dueDate) && DateTime.Today > dueDate)
				{
					e.Row.BackColor = Color.PaleVioletRed;
				}
			}
			catch (Exception ex)
			{
				base.Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
			}
		}

		// Token: 0x04000063 RID: 99
		private string strcon
		{
			get
			{
				var conSetting = ConfigurationManager.ConnectionStrings["con"] ?? ConfigurationManager.ConnectionStrings["elibraryDBConnectionString"] ?? ConfigurationManager.ConnectionStrings["LibraryDB"];
				return conSetting != null ? conSetting.ConnectionString : "Data Source=library123.mssql.somee.com;Initial Catalog=library123;User ID=shivam12345_SQLLogin_4;Password=Radha@soami123;";
			}
		}

		// Token: 0x04000064 RID: 100
		protected Label Label;

		// Token: 0x04000065 RID: 101
		protected TextBox TextBox1;

		// Token: 0x04000066 RID: 102
		protected TextBox TextBox2;

		// Token: 0x04000067 RID: 103
		protected TextBox TextBox3;

		// Token: 0x04000068 RID: 104
		protected TextBox TextBox4;

		// Token: 0x04000069 RID: 105
		protected DropDownList DropDownList1;

		// Token: 0x0400006A RID: 106
		protected TextBox TextBox6;

		// Token: 0x0400006B RID: 107
		protected TextBox TextBox7;

		// Token: 0x0400006C RID: 108
		protected TextBox TextBox5;

		// Token: 0x0400006D RID: 109
		protected TextBox TextBox8;

		// Token: 0x0400006E RID: 110
		protected TextBox TextBox9;

		// Token: 0x0400006F RID: 111
		protected TextBox TextBox10;

		// Token: 0x04000070 RID: 112
		protected Button Button1;

		// Token: 0x04000071 RID: 113
		protected Label Labell;

		// Token: 0x04000072 RID: 114
		protected GridView GridView1;
	}
}
