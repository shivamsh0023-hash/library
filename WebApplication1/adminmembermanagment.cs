using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
	// Token: 0x02000007 RID: 7
	public class adminmembermanagment : Page
	{
		// Token: 0x0600002D RID: 45 RVA: 0x00003FCE File Offset: 0x000021CE
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.GridView1.DataBind();
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00003FE3 File Offset: 0x000021E3
		protected void LinkButton4_Click(object sender, EventArgs e)
		{
			this.GetMemberByID();
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00003FEB File Offset: 0x000021EB
		protected void LinkButton1_Click(object sender, EventArgs e)
		{
			this.UpdateMemberStatusByID("Active");
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00003FF8 File Offset: 0x000021F8
		protected void LinkButton2_Click(object sender, EventArgs e)
		{
			this.UpdateMemberStatusByID("Pending");
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00004005 File Offset: 0x00002205
		protected void LinkButton3_Click(object sender, EventArgs e)
		{
			this.UpdateMemberStatusByID("Deactive");
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00004012 File Offset: 0x00002212
		protected void Button3_Click(object sender, EventArgs e)
		{
			this.DeleteMemberByID();
		}

		// Token: 0x06000033 RID: 51 RVA: 0x0000401C File Offset: 0x0000221C
		private bool CheckIfMemberExists()
		{
			bool flag;
			try
			{
				using (SqlConnection con = new SqlConnection(this.strcon))
				{
					con.Open();
					using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM member_master_tbl WHERE member_id=@id", con))
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

		// Token: 0x06000034 RID: 52 RVA: 0x000040F0 File Offset: 0x000022F0
		private void DeleteMemberByID()
		{
			if (this.CheckIfMemberExists())
			{
				try
				{
					using (SqlConnection con = new SqlConnection(this.strcon))
					{
						con.Open();
						using (SqlCommand cmd = new SqlCommand("DELETE FROM member_master_tbl WHERE member_id=@id", con))
						{
							cmd.Parameters.AddWithValue("@id", this.TextBox1.Text.Trim());
							cmd.ExecuteNonQuery();
						}
					}
					base.Response.Write("<script>alert('Member Deleted Successfully');</script>");
					this.ClearForm();
					this.GridView1.DataBind();
					return;
				}
				catch (Exception ex)
				{
					base.Response.Write("<script>alert('" + ex.Message.Replace("'", "\\'") + "');</script>");
					return;
				}
			}
			base.Response.Write("<script>alert('Invalid Member ID');</script>");
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000041F4 File Offset: 0x000023F4
		private void GetMemberByID()
		{
			try
			{
				using (SqlConnection con = new SqlConnection(this.strcon))
				{
					con.Open();
					using (SqlCommand cmd = new SqlCommand("SELECT * FROM member_master_tbl WHERE member_id=@id", con))
					{
						cmd.Parameters.AddWithValue("@id", this.TextBox1.Text.Trim());
						using (SqlDataReader dr = cmd.ExecuteReader())
						{
							if (dr.HasRows)
							{
								dr.Read();
								this.TextBox2.Text = dr["full_name"].ToString();
								this.TextBox7.Text = dr["account_status"].ToString();
								this.TextBox8.Text = dr["dob"].ToString();
								this.TextBox3.Text = dr["contact_no"].ToString();
								this.TextBox4.Text = dr["email"].ToString();
								this.TextBox9.Text = dr["state"].ToString();
								this.TextBox10.Text = dr["city"].ToString();
								this.TextBox11.Text = dr["pincode"].ToString();
								this.TextBox5.Text = dr["full_address"].ToString();
							}
							else
							{
								base.Response.Write("<script>alert('Invalid Member ID');</script>");
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				base.Response.Write("<script>alert('" + ex.Message.Replace("'", "\\'") + "');</script>");
			}
		}

		// Token: 0x06000036 RID: 54 RVA: 0x0000441C File Offset: 0x0000261C
		private void UpdateMemberStatusByID(string status)
		{
			if (this.CheckIfMemberExists())
			{
				try
				{
					using (SqlConnection con = new SqlConnection(this.strcon))
					{
						con.Open();
						using (SqlCommand cmd = new SqlCommand("UPDATE member_master_tbl SET account_status=@status WHERE member_id=@id", con))
						{
							cmd.Parameters.AddWithValue("@status", status);
							cmd.Parameters.AddWithValue("@id", this.TextBox1.Text.Trim());
							cmd.ExecuteNonQuery();
						}
					}
					this.GridView1.DataBind();
					base.Response.Write("<script>alert('Member Status Updated');</script>");
					return;
				}
				catch (Exception ex)
				{
					base.Response.Write("<script>alert('" + ex.Message.Replace("'", "\\'") + "');</script>");
					return;
				}
			}
			base.Response.Write("<script>alert('Invalid Member ID');</script>");
		}

		// Token: 0x06000037 RID: 55 RVA: 0x0000452C File Offset: 0x0000272C
		private void ClearForm()
		{
			this.TextBox1.Text = "";
			this.TextBox2.Text = "";
			this.TextBox7.Text = "";
			this.TextBox8.Text = "";
			this.TextBox3.Text = "";
			this.TextBox4.Text = "";
			this.TextBox9.Text = "";
			this.TextBox10.Text = "";
			this.TextBox11.Text = "";
			this.TextBox5.Text = "";
		}

		// Token: 0x04000035 RID: 53
		private string strcon
		{
			get
			{
				var conSetting = ConfigurationManager.ConnectionStrings["con"] ?? ConfigurationManager.ConnectionStrings["elibraryDBConnectionString"] ?? ConfigurationManager.ConnectionStrings["LibraryDB"];
				return conSetting != null ? conSetting.ConnectionString : "Data Source=library123.mssql.somee.com;Initial Catalog=library123;User ID=shivam12345_SQLLogin_4;Password=Radha@soami123;";
			}
		}

		// Token: 0x04000036 RID: 54
		protected TextBox TextBox1;

		// Token: 0x04000037 RID: 55
		protected LinkButton LinkButton4;

		// Token: 0x04000038 RID: 56
		protected TextBox TextBox2;

		// Token: 0x04000039 RID: 57
		protected TextBox TextBox7;

		// Token: 0x0400003A RID: 58
		protected LinkButton LinkButton1;

		// Token: 0x0400003B RID: 59
		protected LinkButton LinkButton2;

		// Token: 0x0400003C RID: 60
		protected LinkButton LinkButton3;

		// Token: 0x0400003D RID: 61
		protected TextBox TextBox8;

		// Token: 0x0400003E RID: 62
		protected TextBox TextBox3;

		// Token: 0x0400003F RID: 63
		protected TextBox TextBox4;

		// Token: 0x04000040 RID: 64
		protected TextBox TextBox9;

		// Token: 0x04000041 RID: 65
		protected TextBox TextBox10;

		// Token: 0x04000042 RID: 66
		protected TextBox TextBox11;

		// Token: 0x04000043 RID: 67
		protected TextBox TextBox5;

		// Token: 0x04000044 RID: 68
		protected Button Button3;

		// Token: 0x04000045 RID: 69
		protected SqlDataSource SqlDataSource1;

		// Token: 0x04000046 RID: 70
		protected GridView GridView1;
	}
}
