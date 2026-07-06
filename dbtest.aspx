<%@ Page Title="Database Test" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="dbtest.aspx.cs" Inherits="WebApplication1.dbtest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        table { border-collapse: collapse; width: 100%; margin-top: 20px; }
        th, td { border: 1px solid #333; padding: 8px; text-align: left; }
        th { background-color: #eee; }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Database Test</h2>
    <asp:Literal ID="litResult" runat="server"></asp:Literal>
</asp:Content>
