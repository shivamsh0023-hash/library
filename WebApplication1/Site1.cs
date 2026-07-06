using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace WebApplication1
{
	// Token: 0x0200000B RID: 11
	public class Site1 : MasterPage
	{
		// Token: 0x06000049 RID: 73 RVA: 0x00004D70 File Offset: 0x00002F70
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				string role = ((base.Session["role"] != null) ? base.Session["role"].ToString() : "");
				if (role == "user")
				{
					this.ShowUserLinks();
					return;
				}
				if (role == "admin")
				{
					this.ShowAdminLinks();
					return;
				}
				this.ShowGuestLinks();
			}
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00004DE4 File Offset: 0x00002FE4
		private void ShowGuestLinks()
		{
			this.LinkButton1.Visible = true;
			this.LinkButton2.Visible = true;
			this.LinkButton6.Visible = true;
			this.LinkButton11.Visible = false;
			this.LinkButton12.Visible = false;
			this.LinkButton8.Visible = false;
			this.LinkButton9.Visible = false;
			this.LinkButton10.Visible = false;
			this.LinkButton3.Visible = false;
			this.LinkButton7.Visible = false;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00004E6C File Offset: 0x0000306C
		private void ShowUserLinks()
		{
			this.LinkButton1.Visible = false;
			this.LinkButton2.Visible = false;
			this.LinkButton3.Visible = true;
			this.LinkButton7.Visible = true;
			LinkButton linkButton = this.LinkButton7;
			string text = "Hello ";
			object obj = base.Session["username"];
			linkButton.Text = text + ((obj != null) ? obj.ToString() : null);
			this.LinkButton6.Visible = true;
			this.LinkButton11.Visible = false;
			this.LinkButton12.Visible = false;
			this.LinkButton8.Visible = false;
			this.LinkButton9.Visible = false;
			this.LinkButton10.Visible = false;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00004F24 File Offset: 0x00003124
		private void ShowAdminLinks()
		{
			this.LinkButton1.Visible = false;
			this.LinkButton2.Visible = false;
			this.LinkButton6.Visible = false;
			this.LinkButton3.Visible = true;
			this.LinkButton7.Visible = true;
			this.LinkButton7.Text = "Hello Admin";
			this.LinkButton11.Visible = true;
			this.LinkButton12.Visible = true;
			this.LinkButton8.Visible = true;
			this.LinkButton9.Visible = true;
			this.LinkButton10.Visible = true;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00004FB9 File Offset: 0x000031B9
		protected void LinkButton1_Click(object sender, EventArgs e)
		{
			base.Response.Redirect("User_Login.aspx");
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00004FCB File Offset: 0x000031CB
		protected void LinkButton2_Click(object sender, EventArgs e)
		{
			base.Response.Redirect("usersignup.aspx");
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00004FDD File Offset: 0x000031DD
		protected void LinkButton3_Click(object sender, EventArgs e)
		{
			base.Session.Clear();
			base.Session.Abandon();
			base.Response.Redirect("homepage.aspx");
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00005005 File Offset: 0x00003205
		protected void LinkButton5_Click(object sender, EventArgs e)
		{
			base.Response.Redirect("Viewbooks.aspx");
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00005017 File Offset: 0x00003217
		protected void LinkButton6_Click(object sender, EventArgs e)
		{
			base.Response.Redirect("Admin_Login.aspx");
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00005029 File Offset: 0x00003229
		protected void LinkButton7_Click(object sender, EventArgs e)
		{
			base.Response.Redirect("userprofile.aspx");
		}

		// Token: 0x06000053 RID: 83 RVA: 0x0000503B File Offset: 0x0000323B
		protected void LinkButton8_Click(object sender, EventArgs e)
		{
			base.Response.Redirect("adminbookinventory.aspx");
		}

		// Token: 0x06000054 RID: 84 RVA: 0x0000504D File Offset: 0x0000324D
		protected void LinkButton9_Click(object sender, EventArgs e)
		{
			base.Response.Redirect("adminbookissuing.aspx");
		}

		// Token: 0x06000055 RID: 85 RVA: 0x0000505F File Offset: 0x0000325F
		protected void LinkButton10_Click(object sender, EventArgs e)
		{
			base.Response.Redirect("adminmembermanagment.aspx");
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00005071 File Offset: 0x00003271
		protected void LinkButton11_Click(object sender, EventArgs e)
		{
			base.Response.Redirect("adminauthormanagment.aspx");
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00005083 File Offset: 0x00003283
		protected void LinkButton12_Click(object sender, EventArgs e)
		{
			base.Response.Redirect("adminpublishermanagment.aspx");
		}

		// Token: 0x04000051 RID: 81
		protected ContentPlaceHolder head;

		// Token: 0x04000052 RID: 82
		protected HtmlForm form1;

		// Token: 0x04000053 RID: 83
		protected LinkButton LinkButton5;

		// Token: 0x04000054 RID: 84
		protected LinkButton LinkButton1;

		// Token: 0x04000055 RID: 85
		protected LinkButton LinkButton2;

		// Token: 0x04000056 RID: 86
		protected LinkButton LinkButton3;

		// Token: 0x04000057 RID: 87
		protected LinkButton LinkButton7;

		// Token: 0x04000058 RID: 88
		protected ContentPlaceHolder ContentPlaceHolder1;

		// Token: 0x04000059 RID: 89
		protected LinkButton LinkButton6;

		// Token: 0x0400005A RID: 90
		protected LinkButton LinkButton11;

		// Token: 0x0400005B RID: 91
		protected LinkButton LinkButton12;

		// Token: 0x0400005C RID: 92
		protected LinkButton LinkButton8;

		// Token: 0x0400005D RID: 93
		protected LinkButton LinkButton9;

		// Token: 0x0400005E RID: 94
		protected LinkButton LinkButton10;
	}
}
