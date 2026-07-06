<%@ Page Title="Terms & Conditions" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Terms.aspx.cs" Inherits="WebApplication1.Terms" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .terms-section {
            background: #fdfdfd;
            padding: 40px;
            margin: 20px auto;
            max-width: 900px;
            border-radius: 10px;
            box-shadow: 0 2px 6px rgba(0,0,0,0.15);
            font-family: Arial, sans-serif;
            line-height: 1.8;
        }
        .terms-section h2 {
            text-align: center;
            color: #2c3e50;
            margin-bottom: 20px;
        }
        .terms-section p {
            font-size: 15px;
            color: #333;
            margin-bottom: 15px;
            text-align: justify;
        }
        .terms-section ul {
            margin-left: 20px;
            list-style-type: disc;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="terms-section">
        <h2>Terms & Conditions</h2>
        <p>
            Welcome to the <strong>Library Management System</strong>. By accessing or using this system, 
            you agree to abide by the following terms and conditions. These rules ensure a smooth and 
            fair usage for all users including students, librarians, and administrators.
        </p>

        <ul>
            <li>Users must register with valid information before accessing library services.</li>
            <li>Each book issued must be returned within the due date to avoid penalties.</li>
            <li>Any damage or loss of books will result in a fine or replacement requirement.</li>
            <li>User credentials (username & password) must not be shared with others.</li>
            <li>Administrators have the right to suspend accounts for misuse of the system.</li>
            <li>The library reserves the right to update or modify these terms at any time.</li>
        </ul>

        <p>
            By continuing to use the system, you acknowledge that you have read, understood, 
            and agreed to these terms. If you do not agree, please refrain from using the application.
        </p>
    </div>
</asp:Content>
