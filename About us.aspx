<%@ Page Title="About Us" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="About us.aspx.cs" Inherits="WebApplication1.About_us" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .about-section {
            background: #f5f5f5;
            padding: 40px;
            border-radius: 12px;
            box-shadow: 0 4px 8px rgba(0,0,0,0.1);
            max-width: 900px;
            margin: 20px auto;
            font-family: Arial, sans-serif;
            line-height: 1.7;
        }
        .about-section h2 {
            text-align: center;
            color: #2c3e50;
            margin-bottom: 20px;
        }
        .about-section p {
            font-size: 16px;
            color: #333;
            margin-bottom: 15px;
            text-align: justify;
        }
        .about-section .highlight {
            color: #2980b9;
            font-weight: bold;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="about-section">
        <h2>About Us</h2>
        <p>
            Welcome to the <span class="highlight">Library Management System</span>, a complete solution designed to simplify 
            and digitalize the process of managing libraries. This system is built to help administrators, 
            librarians, and students in managing books, issuing and returning transactions, and maintaining 
            accurate book records.
        </p>
        <p>
            Our system provides features such as <span class="highlight">book catalog management</span>, 
            <span class="highlight">member registration</span>, <span class="highlight">book issue & return tracking</span>, 
            and <span class="highlight">inventory monitoring</span>. It reduces manual paperwork and ensures 
            accuracy in maintaining records.
        </p>
        <p>
            With user-friendly design, real-time updates, and secure database handling, 
            the Library Management System aims to save time, improve efficiency, 
            and provide a smooth experience for all users.
        </p>
        <p>
            This project was developed as part of an academic initiative, showcasing the practical 
            implementation of <span class="highlight">C#, ASP.NET, and SQL Server</span> in creating 
            real-world applications.
        </p>
    </div>
</asp:Content>
