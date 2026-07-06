<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="adminbookinventory.aspx.cs" Inherits="WebApplication1.adminbookinventory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .date-box { width: 200px !important; }
        .grid-container { max-height: 600px; overflow-y: auto; }
        .book-item { background-color: #f8f9fa; border: 1px solid #dee2e6; border-radius: 5px; padding: 10px; margin-bottom: 10px; display: flex; align-items: flex-start; }
        .book-item img { width: 80px; height: 80px; object-fit: cover; border-radius: 5px; margin-right: 10px; }
        .book-details-container { flex: 1; }
        .book-details { font-size: 14px; line-height: 1.5; margin: 0; }
        .book-description { font-size: 13px; color: #495057; margin-top: 5px; }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div class="row">
            <!-- Left Column: Book Details -->
            <div class="col-md-5">
                <div class="card">
                    <div class="card-body">
                        <div class="row"><div class="col"><center><h4>Book Details</h4></center></div></div><hr />
                        
                        <div class="row mb-2">
                            <div class="col-md-4">
                                <label>Book ID</label>
                                <div class="input-group">
                                    <asp:TextBox CssClass="form-control" ID="TextBox1" runat="server" placeholder="Book ID"></asp:TextBox>
                                    <asp:Button CssClass="btn btn-primary" ID="Button4" runat="server" Text="Go" OnClick="Button4_Click" />
                                </div>
                            </div>
                            <div class="col-md-8">
                                <label>Book Name</label>
                                <asp:TextBox CssClass="form-control" ID="TextBox2" runat="server" placeholder="Book Name"></asp:TextBox>
                            </div>
                        </div>

                        <div class="row mb-2">
                            <div class="col-md-4">
                                <label>Language</label>
                                <asp:DropDownList CssClass="form-control" ID="DropDownList1" runat="server">
                                    <asp:ListItem Text="English" Value="English" />
                                    <asp:ListItem Text="Hindi" Value="Hindi" />
                                    <asp:ListItem Text="Marathi" Value="Marathi" />
                                    <asp:ListItem Text="Urdu" Value="Urdu" />
                                    <asp:ListItem Text="German" Value="German" />
                                </asp:DropDownList>
                                <label>Publisher Name</label>
                                <asp:TextBox CssClass="form-control" ID="txtPublisher" runat="server" placeholder="Publisher Name"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                <label>Author Name</label>
                                <asp:TextBox CssClass="form-control" ID="txtAuthor" runat="server" placeholder="Author Name"></asp:TextBox>
                                <label>Issued Date</label>
                                <asp:TextBox CssClass="form-control date-box" ID="TextBox4" runat="server" placeholder="Date" TextMode="Date"></asp:TextBox>
                            </div>
                            <div class="col-md-4">
                                <label>Genre</label>
                                <asp:ListBox CssClass="form-control" ID="ListBox3" runat="server" SelectionMode="Multiple" Rows="5">
                                    <asp:ListItem Text="Fiction" Value="Fiction"></asp:ListItem>
                                    <asp:ListItem Text="Non-Fiction" Value="Non-Fiction"></asp:ListItem>
                                    <asp:ListItem Text="Science" Value="Science"></asp:ListItem>
                                    <asp:ListItem Text="Mathematics" Value="Mathematics"></asp:ListItem>
                                    <asp:ListItem Text="History" Value="History"></asp:ListItem>
                                    <asp:ListItem Text="Biography" Value="Biography"></asp:ListItem>
                                    <asp:ListItem Text="Comics" Value="Comics"></asp:ListItem>
                                    <asp:ListItem Text="Novel" Value="Novel"></asp:ListItem>
                                    <asp:ListItem Text="Poetry" Value="Poetry"></asp:ListItem>
                                </asp:ListBox>
                            </div>
                        </div>

                        <div class="row mb-2">
                            <div class="col-md-4"><label>Edition</label><asp:TextBox CssClass="form-control" ID="TextBox9" runat="server" placeholder="Edition"></asp:TextBox></div>
                            <div class="col-md-4"><label>Book Cost(Per Unit)</label><asp:TextBox CssClass="form-control" ID="TextBox10" runat="server" placeholder="Book Cost" TextMode="Number"></asp:TextBox></div>
                            <div class="col-md-4"><label>Pages</label><asp:TextBox CssClass="form-control" ID="TextBox11" runat="server" placeholder="Pages" TextMode="Number"></asp:TextBox></div>
                        </div>
                        <div class="row mb-2">
                            <div class="col-md-4"><label>Actual Stock</label><asp:TextBox CssClass="form-control" ID="TextBox3" runat="server" placeholder="Actual Stock" TextMode="Number"></asp:TextBox></div>
                            <div class="col-md-4"><label>Current Stock</label><asp:TextBox CssClass="form-control" ID="TextBox6" runat="server" placeholder="Current Stock" TextMode="Number" ReadOnly="true"></asp:TextBox></div>
                            <div class="col-md-4"><label>Issued Books</label><asp:TextBox CssClass="form-control" ID="TextBox7" runat="server" placeholder="Issued Books" TextMode="Number" ReadOnly="true"></asp:TextBox></div>
                        </div>

                        <!-- Book Image -->
                        <div class="row mb-2">
                            <div class="col-md-12">
                                <label>Book Image</label><br />
                                <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control" />
                                <asp:Button ID="btnUploadImage" runat="server" Text="Upload" CssClass="btn btn-secondary mt-2" OnClick="btnUploadImage_Click" />
                                <br /><br />
                                <asp:Image ID="imgPreview" runat="server" Width="120px" Height="120px" ImageUrl="~/book_inventory/default.png" />
                            </div>
                        </div>

                        <div class="row mb-2">
                            <div class="col-12"><label>Book Description</label><asp:TextBox CssClass="form-control" ID="TextBox5" runat="server" placeholder="Book Description" TextMode="MultiLine" Rows="2"></asp:TextBox></div>
                        </div>

                        <div class="row mb-3">
                            <div class="col-4"><asp:Button ID="Button1" CssClass="btn btn-lg btn-block btn-success" runat="server" Text="Add" OnClick="Button1_Click" /></div>
                            <div class="col-4"><asp:Button ID="Button2" CssClass="btn btn-lg btn-block btn-warning" runat="server" Text="Update" OnClick="Button2_Click" /></div>
                            <div class="col-4"><asp:Button ID="Button3" CssClass="btn btn-lg btn-block btn-danger" runat="server" Text="Delete" OnClick="Button3_Click" /></div>
                        </div>
                        <a href="homepage.aspx">Back to Home</a>
                    </div>
                </div>
            </div>

            <!-- Right Column: Book Inventory List -->
            <div class="col-md-7 grid-container">
                <div class="card">
                    <div class="card-body">
                        <center><h4>Book Inventory List</h4></center><hr />
                        <asp:Repeater ID="Repeater1" runat="server">
                            <HeaderTemplate></HeaderTemplate>
                            <ItemTemplate>
                                <div class="book-item">
                                    <asp:Image ID="BookImage" runat="server" ImageUrl='<%# string.IsNullOrEmpty(Eval("book_img_link").ToString()) ? "~/book_inventory/default.png" : Eval("book_img_link") %>' />
                                    <div class="book-details-container">
                                        <p class="book-details">
                                            <strong>Book ID:</strong> <%# Eval("book_id") %>, 
                                            <strong>Book Name:</strong> <%# Eval("book_name") %>, 
                                            <strong>Author:</strong> <%# Eval("author_name") %>, 
                                            <strong>Publisher:</strong> <%# Eval("publisher_name") %>, 
                                            <strong>Issued Date:</strong> <%# Eval("publisher_date") %>, 
                                            <strong>Language:</strong> <%# Eval("language") %>, 
                                            <strong>Genre:</strong> <%# Eval("genre") %>, 
                                            <strong>Edition:</strong> <%# Eval("edition") %>, 
                                            <strong>Cost:</strong> <%# Eval("book_cost") %>, 
                                            <strong>Pages:</strong> <%# Eval("no_of_pages") %>, 
                                            <strong>Actual Stock:</strong> <%# Eval("actual_stock") %>, 
                                            <strong>Current Stock:</strong> <%# Eval("current_stock") %>, 
                                            <strong>Issued Books:</strong> <%# (Convert.ToInt32(Eval("actual_stock")) - Convert.ToInt32(Eval("current_stock"))) %>
                                        </p>
                                        <p class="book-description"><strong>Description:</strong> <%# Eval("book_description") %></p>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>


