using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
	// Token: 0x02000005 RID: 5
	public class adminbookinventory : Page
	{
		// Token: 0x06000013 RID: 19 RVA: 0x000028DC File Offset: 0x00000ADC
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.BindRepeater();
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000028EC File Offset: 0x00000AEC
		protected void Button4_Click(object sender, EventArgs e)
		{
			this.GetBookByID();
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000028F4 File Offset: 0x00000AF4
		protected void Button1_Click(object sender, EventArgs e)
		{
			if (this.CheckIfBookExists())
			{
				base.Response.Write("<script>alert('Book Already Exists');</script>");
				return;
			}
			this.AddNewBook();
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002915 File Offset: 0x00000B15
		protected void Button2_Click(object sender, EventArgs e)
		{
			this.UpdateBookByID();
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000291D File Offset: 0x00000B1D
		protected void Button3_Click(object sender, EventArgs e)
		{
			this.DeleteBookByID();
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002928 File Offset: 0x00000B28
		protected void btnUploadImage_Click(object sender, EventArgs e)
		{
			if (!this.FileUpload1.HasFile)
			{
				return;
			}
			try
			{
				string folder = base.Server.MapPath("~/book_inventory/");
				if (!Directory.Exists(folder))
				{
					Directory.CreateDirectory(folder);
				}
				string fileName = Path.GetFileName(this.FileUpload1.PostedFile.FileName);
				string fullPath = Path.Combine(folder, fileName);
				using (global::System.Drawing.Image original = global::System.Drawing.Image.FromStream(this.FileUpload1.PostedFile.InputStream))
				{
					int maxWidth = 200;
					int maxHeight = 200;
					int newWidth = original.Width;
					int newHeight = original.Height;
					if (original.Width > maxWidth || original.Height > maxHeight)
					{
						double ratio = Math.Min((double)maxWidth / (double)original.Width, (double)maxHeight / (double)original.Height);
						newWidth = (int)((double)original.Width * ratio);
						newHeight = (int)((double)original.Height * ratio);
					}
					using (Bitmap resized = new Bitmap(original, newWidth, newHeight))
					{
						resized.Save(fullPath, ImageFormat.Png);
					}
				}
				adminbookinventory.global_img_path = "~/book_inventory/" + fileName;
				this.imgPreview.ImageUrl = adminbookinventory.global_img_path;
				base.Response.Write("<script>alert('Image Uploaded Successfully');</script>");
			}
			catch (Exception ex)
			{
				base.Response.Write("<script>alert('Image Upload Failed: " + ex.Message.Replace("'", "\\'") + "');</script>");
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002AE8 File Offset: 0x00000CE8
		private void BindRepeater()
		{
			try
			{
				using (SqlConnection con = new SqlConnection(this.strcon))
				{
					using (SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM book_master_tbl", con))
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
				base.Response.Write("<script>alert('" + ex.Message.Replace("'", "\\'") + "');</script>");
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002BA8 File Offset: 0x00000DA8
		private bool CheckIfBookExists()
		{
			bool flag;
			using (SqlConnection con = new SqlConnection(this.strcon))
			{
				using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM book_master_tbl WHERE book_id=@id", con))
				{
					cmd.Parameters.AddWithValue("@id", this.TextBox1.Text.Trim());
					con.Open();
					int count = (int)cmd.ExecuteScalar();
					flag = count > 0;
				}
			}
			return flag;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002C38 File Offset: 0x00000E38
		private void GetBookByID()
		{
			using (SqlConnection con = new SqlConnection(this.strcon))
			{
				using (SqlCommand cmd = new SqlCommand("SELECT * FROM book_master_tbl WHERE book_id=@id", con))
				{
					cmd.Parameters.AddWithValue("@id", this.TextBox1.Text.Trim());
					using (SqlDataAdapter da = new SqlDataAdapter(cmd))
					{
						DataTable dt = new DataTable();
						da.Fill(dt);
						if (dt.Rows.Count == 0)
						{
							base.Response.Write("<script>alert('Invalid Book ID');</script>");
						}
						else
						{
							DataRow row = dt.Rows[0];
							this.TextBox2.Text = row["book_name"].ToString();
							this.TextBox9.Text = row["edition"].ToString();
							this.TextBox10.Text = row["book_cost"].ToString();
							this.TextBox5.Text = row["book_description"].ToString();
							this.TextBox11.Text = row["no_of_pages"].ToString();
							this.TextBox3.Text = row["actual_stock"].ToString();
							this.TextBox6.Text = row["current_stock"].ToString();
							int.TryParse(row["actual_stock"].ToString(), out adminbookinventory.global_actual_stock);
							int.TryParse(row["current_stock"].ToString(), out adminbookinventory.global_current_stock);
							adminbookinventory.global_issued_books = Math.Max(adminbookinventory.global_actual_stock - adminbookinventory.global_current_stock, 0);
							this.TextBox7.Text = adminbookinventory.global_issued_books.ToString();
							this.TextBox4.Text = row["publisher_date"].ToString();
							this.txtAuthor.Text = row["author_name"].ToString();
							this.txtPublisher.Text = row["publisher_name"].ToString();
							string img = row["book_img_link"].ToString();
							this.imgPreview.ImageUrl = (string.IsNullOrEmpty(img) ? "~/book_inventory/default.png" : img);
							adminbookinventory.global_img_path = this.imgPreview.ImageUrl;
							this.ListBox3.ClearSelection();
							string[] genres = row["genre"].ToString().Split(new char[] { ',' });
							foreach (string g in genres)
							{
								for (int i = 0; i < this.ListBox3.Items.Count; i++)
								{
									if (this.ListBox3.Items[i].Text.Trim() == g.Trim())
									{
										this.ListBox3.Items[i].Selected = true;
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002F98 File Offset: 0x00001198
		private void AddNewBook()
		{
			int actualStock;
			if (!int.TryParse(this.TextBox3.Text.Trim(), out actualStock))
			{
				base.Response.Write("<script>alert('Enter a valid Actual Stock number');</script>");
				return;
			}
			int currentStock = actualStock;
			int issuedBooks = 0;
			this.TextBox6.Text = currentStock.ToString();
			this.TextBox7.Text = issuedBooks.ToString();
			string genres = "";
			foreach (int i in this.ListBox3.GetSelectedIndices())
			{
				genres = genres + this.ListBox3.Items[i].Text.Trim() + ",";
			}
			if (genres.Length > 0)
			{
				genres = genres.TrimEnd(new char[] { ',' });
			}
			using (SqlConnection con = new SqlConnection(this.strcon))
			{
				using (SqlCommand cmd = new SqlCommand("\r\n                    INSERT INTO book_master_tbl\r\n                    (book_id, book_name, genre, author_name, publisher_name, publisher_date, language, edition, book_cost, no_of_pages, book_description, actual_stock, current_stock, book_img_link)\r\n                    VALUES(@book_id,@book_name,@genre,@author_name,@publisher_name,@publisher_date,@language,@edition,@book_cost,@no_of_pages,@book_description,@actual_stock,@current_stock,@book_img_link)", con))
				{
					cmd.Parameters.AddWithValue("@book_id", this.TextBox1.Text.Trim());
					cmd.Parameters.AddWithValue("@book_name", this.TextBox2.Text.Trim());
					cmd.Parameters.AddWithValue("@genre", genres);
					cmd.Parameters.AddWithValue("@author_name", string.IsNullOrEmpty(this.txtAuthor.Text) ? "Shivam" : this.txtAuthor.Text.Trim());
					cmd.Parameters.AddWithValue("@publisher_name", string.IsNullOrEmpty(this.txtPublisher.Text) ? "Shivam" : this.txtPublisher.Text.Trim());
					cmd.Parameters.AddWithValue("@publisher_date", this.TextBox4.Text.Trim());
					cmd.Parameters.AddWithValue("@language", this.DropDownList1.SelectedValue);
					cmd.Parameters.AddWithValue("@edition", this.TextBox9.Text.Trim());
					cmd.Parameters.AddWithValue("@book_cost", this.TextBox10.Text.Trim());
					cmd.Parameters.AddWithValue("@no_of_pages", this.TextBox11.Text.Trim());
					cmd.Parameters.AddWithValue("@book_description", this.TextBox5.Text.Trim());
					cmd.Parameters.AddWithValue("@actual_stock", actualStock);
					cmd.Parameters.AddWithValue("@current_stock", currentStock);
					cmd.Parameters.AddWithValue("@book_img_link", adminbookinventory.global_img_path);
					con.Open();
					cmd.ExecuteNonQuery();
				}
			}
			this.BindRepeater();
			base.Response.Write("<script>alert('Book Added Successfully');</script>");
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000032D0 File Offset: 0x000014D0
		private void UpdateBookByID()
		{
			if (!this.CheckIfBookExists())
			{
				base.Response.Write("<script>alert('Invalid Book ID');</script>");
				return;
			}
			int actualStock;
			if (!int.TryParse(this.TextBox3.Text.Trim(), out actualStock))
			{
				base.Response.Write("<script>alert('Enter a valid Actual Stock number');</script>");
				return;
			}
			int issuedBooks = Math.Max(adminbookinventory.global_actual_stock - adminbookinventory.global_current_stock, 0);
			if (actualStock < issuedBooks)
			{
				base.Response.Write("<script>alert('Actual Stock cannot be less than Issued Books');</script>");
				return;
			}
			int currentStock = actualStock - issuedBooks;
			this.TextBox6.Text = currentStock.ToString();
			this.TextBox7.Text = issuedBooks.ToString();
			string genres = "";
			foreach (int i in this.ListBox3.GetSelectedIndices())
			{
				genres = genres + this.ListBox3.Items[i].Text.Trim() + ",";
			}
			if (genres.Length > 0)
			{
				genres = genres.TrimEnd(new char[] { ',' });
			}
			using (SqlConnection con = new SqlConnection(this.strcon))
			{
				using (SqlCommand cmd = new SqlCommand("\r\n                    UPDATE book_master_tbl SET\r\n                    book_name=@book_name, genre=@genre, author_name=@author_name, publisher_name=@publisher_name,\r\n                    publisher_date=@publisher_date, language=@language, edition=@edition, book_cost=@book_cost,\r\n                    no_of_pages=@no_of_pages, book_description=@book_description, actual_stock=@actual_stock,\r\n                    current_stock=@current_stock, book_img_link=@book_img_link\r\n                    WHERE book_id=@book_id", con))
				{
					cmd.Parameters.AddWithValue("@book_id", this.TextBox1.Text.Trim());
					cmd.Parameters.AddWithValue("@book_name", this.TextBox2.Text.Trim());
					cmd.Parameters.AddWithValue("@genre", genres);
					cmd.Parameters.AddWithValue("@author_name", string.IsNullOrEmpty(this.txtAuthor.Text) ? "Shivam" : this.txtAuthor.Text.Trim());
					cmd.Parameters.AddWithValue("@publisher_name", string.IsNullOrEmpty(this.txtPublisher.Text) ? "Shivam" : this.txtPublisher.Text.Trim());
					cmd.Parameters.AddWithValue("@publisher_date", this.TextBox4.Text.Trim());
					cmd.Parameters.AddWithValue("@language", this.DropDownList1.SelectedValue);
					cmd.Parameters.AddWithValue("@edition", this.TextBox9.Text.Trim());
					cmd.Parameters.AddWithValue("@book_cost", this.TextBox10.Text.Trim());
					cmd.Parameters.AddWithValue("@no_of_pages", this.TextBox11.Text.Trim());
					cmd.Parameters.AddWithValue("@book_description", this.TextBox5.Text.Trim());
					cmd.Parameters.AddWithValue("@actual_stock", actualStock);
					cmd.Parameters.AddWithValue("@current_stock", currentStock);
					cmd.Parameters.AddWithValue("@book_img_link", adminbookinventory.global_img_path);
					con.Open();
					cmd.ExecuteNonQuery();
				}
			}
			this.BindRepeater();
			base.Response.Write("<script>alert('Book Updated Successfully');</script>");
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00003648 File Offset: 0x00001848
		private void DeleteBookByID()
		{
			if (!this.CheckIfBookExists())
			{
				base.Response.Write("<script>alert('Invalid Book ID');</script>");
				return;
			}
			using (SqlConnection con = new SqlConnection(this.strcon))
			{
				using (SqlCommand cmd = new SqlCommand("DELETE FROM book_master_tbl WHERE book_id=@id", con))
				{
					cmd.Parameters.AddWithValue("@id", this.TextBox1.Text.Trim());
					con.Open();
					cmd.ExecuteNonQuery();
				}
			}
			this.BindRepeater();
			base.Response.Write("<script>alert('Book Deleted Successfully');</script>");
		}

		// Token: 0x0400000E RID: 14
		private string strcon
		{
			get
			{
				var conSetting = ConfigurationManager.ConnectionStrings["con"] ?? ConfigurationManager.ConnectionStrings["elibraryDBConnectionString"] ?? ConfigurationManager.ConnectionStrings["LibraryDB"];
				return conSetting != null ? conSetting.ConnectionString : "Data Source=library123.mssql.somee.com;Initial Catalog=library123;User ID=shivam12345_SQLLogin_4;Password=Radha@soami123;";
			}
		}

		// Token: 0x0400000F RID: 15
		private static int global_actual_stock;

		// Token: 0x04000010 RID: 16
		private static int global_current_stock;

		// Token: 0x04000011 RID: 17
		private static int global_issued_books;

		// Token: 0x04000012 RID: 18
		private static string global_img_path = "~/book_inventory/default.png";

		// Token: 0x04000013 RID: 19
		protected TextBox TextBox1;

		// Token: 0x04000014 RID: 20
		protected Button Button4;

		// Token: 0x04000015 RID: 21
		protected TextBox TextBox2;

		// Token: 0x04000016 RID: 22
		protected DropDownList DropDownList1;

		// Token: 0x04000017 RID: 23
		protected TextBox txtPublisher;

		// Token: 0x04000018 RID: 24
		protected TextBox txtAuthor;

		// Token: 0x04000019 RID: 25
		protected TextBox TextBox4;

		// Token: 0x0400001A RID: 26
		protected ListBox ListBox3;

		// Token: 0x0400001B RID: 27
		protected TextBox TextBox9;

		// Token: 0x0400001C RID: 28
		protected TextBox TextBox10;

		// Token: 0x0400001D RID: 29
		protected TextBox TextBox11;

		// Token: 0x0400001E RID: 30
		protected TextBox TextBox3;

		// Token: 0x0400001F RID: 31
		protected TextBox TextBox6;

		// Token: 0x04000020 RID: 32
		protected TextBox TextBox7;

		// Token: 0x04000021 RID: 33
		protected FileUpload FileUpload1;

		// Token: 0x04000022 RID: 34
		protected Button btnUploadImage;

		// Token: 0x04000023 RID: 35
		protected global::System.Web.UI.WebControls.Image imgPreview;

		// Token: 0x04000024 RID: 36
		protected TextBox TextBox5;

		// Token: 0x04000025 RID: 37
		protected Button Button1;

		// Token: 0x04000026 RID: 38
		protected Button Button2;

		// Token: 0x04000027 RID: 39
		protected Button Button3;

		// Token: 0x04000028 RID: 40
		protected Repeater Repeater1;
	}
}
