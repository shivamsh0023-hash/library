using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
	// Token: 0x0200000F RID: 15
	public class usersignup : Page
	{
		// Token: 0x06000065 RID: 101 RVA: 0x00002050 File Offset: 0x00000250
		protected void Page_Load(object sender, EventArgs e)
		{
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00005ACE File Offset: 0x00003CCE
		protected void Button1_Click(object sender, EventArgs e)
		{
			if (this.CheckMemberExists())
			{
				base.Response.Write("<script>alert('Member Already Exists with this ID');</script>");
				return;
			}
			this.SignUpNewMember();
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00005AF0 File Offset: 0x00003CF0
		private bool CheckMemberExists()
		{
			bool flag;
			using (SqlConnection con = new SqlConnection(this.strcon))
			{
				try
				{
					con.Open();
					SqlCommand cmd = new SqlCommand("SELECT member_id FROM member_master_tbl WHERE member_id=@member_id", con);
					cmd.Parameters.AddWithValue("@member_id", this.TextBox8.Text.Trim());
					using (SqlDataReader dr = cmd.ExecuteReader())
					{
						flag = dr.HasRows;
					}
				}
				catch (Exception ex)
				{
					base.Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
					flag = false;
				}
			}
			return flag;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00005BB0 File Offset: 0x00003DB0
		private void SignUpNewMember()
		{
			using (SqlConnection con = new SqlConnection(this.strcon))
			{
				try
				{
					con.Open();
					SqlCommand cmd = new SqlCommand("\r\n                        INSERT INTO member_master_tbl\r\n                        (full_name, dob, contact_no, email, state, city, pincode,\r\n                         full_address, member_id, password, account_status)\r\n                        VALUES\r\n                        (@full_name, @dob, @contact_no, @email, @state, @city, \r\n                         @pincode, @full_address, @member_id, @password, @account_status)", con);
					cmd.Parameters.AddWithValue("@full_name", this.TextBox1.Text.Trim());
					cmd.Parameters.AddWithValue("@dob", this.TextBox2.Text.Trim());
					cmd.Parameters.AddWithValue("@contact_no", this.TextBox3.Text.Trim());
					cmd.Parameters.AddWithValue("@email", this.TextBox4.Text.Trim());
					cmd.Parameters.AddWithValue("@state", this.DropDownList1.SelectedValue);
					cmd.Parameters.AddWithValue("@city", this.TextBox6.Text.Trim());
					cmd.Parameters.AddWithValue("@pincode", this.TextBox7.Text.Trim());
					cmd.Parameters.AddWithValue("@full_address", this.TextBox5.Text.Trim());
					cmd.Parameters.AddWithValue("@member_id", this.TextBox8.Text.Trim());
					cmd.Parameters.AddWithValue("@password", this.TextBox9.Text.Trim());
					cmd.Parameters.AddWithValue("@account_status", "pending");
					int result = cmd.ExecuteNonQuery();
					if (result > 0)
					{
						base.Response.Write("<script>alert('Signup Successful. Please Login');</script>");
						base.Response.Redirect("UserLogin.aspx", false);
						this.Context.ApplicationInstance.CompleteRequest();
					}
					else
					{
						base.Response.Write("<script>alert('Signup Failed. Try Again');</script>");
					}
				}
				catch (Exception ex)
				{
					base.Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
				}
			}
		}

		// Token: 0x04000073 RID: 115
		private string strcon
		{
			get
			{
				var conSetting = ConfigurationManager.ConnectionStrings["con"] ?? ConfigurationManager.ConnectionStrings["elibraryDBConnectionString"] ?? ConfigurationManager.ConnectionStrings["LibraryDB"];
				return conSetting != null ? conSetting.ConnectionString : "Data Source=library123.mssql.somee.com;Initial Catalog=library123;User ID=shivam12345_SQLLogin_4;Password=Radha@soami123;";
			}
		}

		// Token: 0x04000074 RID: 116
		protected TextBox TextBox1;

		// Token: 0x04000075 RID: 117
		protected TextBox TextBox2;

		// Token: 0x04000076 RID: 118
		protected TextBox TextBox3;

		// Token: 0x04000077 RID: 119
		protected TextBox TextBox4;

		// Token: 0x04000078 RID: 120
		protected DropDownList DropDownList1;

		// Token: 0x04000079 RID: 121
		protected TextBox TextBox6;

		// Token: 0x0400007A RID: 122
		protected TextBox TextBox7;

		// Token: 0x0400007B RID: 123
		protected TextBox TextBox5;

		// Token: 0x0400007C RID: 124
		protected TextBox TextBox8;

		// Token: 0x0400007D RID: 125
		protected TextBox TextBox9;

		// Token: 0x0400007E RID: 126
		protected Button Button1;
	}
}
