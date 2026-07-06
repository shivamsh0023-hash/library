using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
	// Token: 0x02000009 RID: 9
	public class dbtest : Page
	{
		// Token: 0x06000045 RID: 69 RVA: 0x00004B84 File Offset: 0x00002D84
		protected void Page_Load(object sender, EventArgs e)
		{
			string cs = "";
			var conSetting = ConfigurationManager.ConnectionStrings["con"] ?? ConfigurationManager.ConnectionStrings["elibraryDBConnectionString"] ?? ConfigurationManager.ConnectionStrings["LibraryDB"];
			if (conSetting != null)
			{
				cs = conSetting.ConnectionString;
			}
			else
			{
				cs = "Data Source=library123.mssql.somee.com;Initial Catalog=library123;User ID=shivam12345_SQLLogin_4;Password=Radha@soami123;";
			}

			// Diagnostic: Try to connect using the configured connection string first, then fallback to testing the new password.
			using (SqlConnection con = new SqlConnection(cs))
			{
				try
				{
					con.Open();
				}
				catch (Exception ex)
				{
					// If the first attempt failed due to login, try with the new password
					if (ex.Message.Contains("Login failed"))
					{
						string alternateCs = "Data Source=library.mssql.somee.com;Initial Catalog=library;User ID=shivam12345_SQLLogin_1;Password=Radha@soami123;";
						using (SqlConnection conAlt = new SqlConnection(alternateCs))
						{
							try
							{
								conAlt.Open();
								cs = alternateCs; // Use the successful alternate connection string
								this.litResult.Text = "<b style='color:green;'>✅ Successfully connected using your new password (Radha@soami123)!</b><br/>";
							}
							catch (Exception exAlt)
							{
								this.litResult.Text = "<b style='color:red;'>❌ Both passwords failed.</b><br/>First Error: " + ex.Message + "<br/>Second Error: " + exAlt.Message;
								return;
							}
						}
					}
					else
					{
						this.litResult.Text = "<b style='color:red;'>❌ Connection failed:</b> " + ex.Message;
						return;
					}
				}
			}

			using (SqlConnection con = new SqlConnection(cs))
			{
				try
				{
					con.Open();
					string query = "SELECT TOP 5 * FROM book_master_tbl";
					SqlCommand cmd = new SqlCommand(query, con);
					SqlDataReader reader = cmd.ExecuteReader();
					if (reader.HasRows)
					{
						this.litResult.Text = "<b style='color:green;'>✅ Database connection successful! Showing top 5 books:</b><br/><table border='1'>";
						Literal literal = this.litResult;
						literal.Text += "<tr>";
						for (int i = 0; i < reader.FieldCount; i++)
						{
							Literal literal2 = this.litResult;
							literal2.Text = literal2.Text + "<th>" + reader.GetName(i) + "</th>";
						}
						Literal literal3 = this.litResult;
						literal3.Text += "</tr>";
						while (reader.Read())
						{
							Literal literal4 = this.litResult;
							literal4.Text += "<tr>";
							for (int j = 0; j < reader.FieldCount; j++)
							{
								Literal literal5 = this.litResult;
								literal5.Text += string.Format("<td>{0}</td>", reader[j]);
							}
							Literal literal6 = this.litResult;
							literal6.Text += "</tr>";
						}
						Literal literal7 = this.litResult;
						literal7.Text += "</table>";
					}
					else
					{
						this.litResult.Text = "<b style='color:orange;'>⚠\ufe0f Database connected but no data found in book_master_tbl.</b>";
					}
					reader.Close();
				}
				catch (Exception ex)
				{
					this.litResult.Text = "<b style='color:red;'>❌ Error:</b> " + ex.Message;
				}
			}
		}

		// Token: 0x04000050 RID: 80
		protected Literal litResult;
	}
}
