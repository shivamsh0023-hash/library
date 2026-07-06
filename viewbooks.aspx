<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="viewbooks.aspx.cs" Inherits="WebApplication1.viewbooks" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .grid-container {
            max-height: 600px;
            overflow-y: auto;
        }
        .book-item {
            background-color: #f8f9fa;
            border: 1px solid #dee2e6;
            border-radius: 5px;
            padding: 10px;
            margin-bottom: 10px;
            display: flex;
            gap: 10px;
        }
        .book-image {
            width: 100px;
            height: 120px;
            object-fit: cover;
            border-radius: 5px;
            border: 1px solid #ccc;
        }
        .book-details {
            font-size: 14px;
            line-height: 1.5;
        }
        .book-description {
            font-size: 13px;
            color: #495057;
            margin-top: 5px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div class="row">
            <div class="col-md-12 grid-container">
                <div class="card">
                    <div class="card-body">
                        <center><h4>Book Inventory List</h4></center>
                        <hr />
                        <asp:Repeater ID="Repeater1" runat="server">
                            <HeaderTemplate></HeaderTemplate>
                            <ItemTemplate>
                                <div class="book-item">
                                    <asp:Image ID="BookImage" CssClass="book-image" runat="server" 
                                        ImageUrl='<%# string.IsNullOrEmpty(Eval("book_img_link").ToString()) ? "~/book_inventory/default.png" : Eval("book_img_link") %>' />
                                    <div>
                                        <p class="book-details">
                                            <strong>Book ID:</strong> <%# Eval("book_id") %>, 
                                            <strong>Book Name:</strong> <%# Eval("book_name") %>, 
                                            <strong>Author Name:</strong> <%# Eval("author_name") %>, 
                                            <strong>Publisher Name:</strong> <%# Eval("publisher_name") %>, 
                                            <strong>Issued Date:</strong> <%# Eval("publisher_date") %>, 
                                            <strong>Language:</strong> <%# Eval("language") %>, 
                                            <strong>Genre:</strong> <%# Eval("genre") %>, 
                                            <strong>Edition:</strong> <%# Eval("edition") %>, 
                                            <strong>Cost:</strong> <%# Eval("book_cost") %>, 
                                            <strong>Pages:</strong> <%# Eval("no_of_pages") %>, 
                                            <strong>Actual Stock:</strong> <%# Eval("actual_stock") %>, 
                                            <strong>Current Stock:</strong> <%# Eval("current_stock") %>, 
                                            <strong>Issued Books:</strong> <%# (Convert.ToInt32(Eval("actual_stock")) - Convert.ToInt32(Eval("current_stock")) < 0 ? 0 : Convert.ToInt32(Eval("actual_stock")) - Convert.ToInt32(Eval("current_stock"))) %>
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


