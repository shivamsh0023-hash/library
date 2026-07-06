<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="homepage.aspx.cs" Inherits="WebApplication1.homepage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section>
        <img src="imgs/home.jpg.jpg"  class="img-fluid" />
    </section>
    <section>

        <div class="container">
            <div class="row">
                <div class="col-12">
                    <center>
                        <h2>Our Feature</h2>
                        <p><b>Our 3 Primary Features -</b></p>
                    </center>
                </div>
            </div>

            <div class="row">
                <div class="col-md-4">
                    <center>
                   <img width="90px" src="imgs/inventory.png"/>
                    <h4>Digital Book Inventory</h4>
                    <p class="text-flexible">Duis aute irure dolor in reprehender in volupate</p>
                    </center>
                </div>

                <div class="col-md-4">
                    <center>
                   <img width="90px" src="imgs/search%20online.jpg" />
                    <h4>Search Books</h4>
                    <p class="text-flexible">Duis aute irure dolor in reprehender in volupate</p>
                    </center>
                </div>

                <div class="col-md-4">
                    <center>
                   <img width="85px" src="imgs/defaulter%20list.jpg" />
                    <h4>Defaulter List</h4>
                    <p class="text-flexible">Duis aute irure dolor in reprehender in volupate</p>
                    </center>
                </div>


            </div>
        </div>
    </section>

     <section>
        <img src="imgs/banner.jpg.png"  class="img-fluid" />
    </section>


    <section>
     <div class="container">
            <div class="row">
                <div class="col-12">
                    <center>
                        <h2>Our Process</h2>
                        <p><b>We have Simple 3 Steps Process</b></p>
                    </center>
                </div>
            </div>

            <div class="row">
                <div class="col-md-4">
                    <center>
                   <img width="90px" src="imgs/Sign%20up.png" />
                    <h4>Sign Up</h4>
                    <p class="text-flexible">Duis aute irure dolor in reprehender in volupate</p>
                    </center>
                </div>

                <div class="col-md-4">
                    <center>
                   <img width="90px" src="imgs/search%20online.jpg" />
                    <h4>Search Books</h4>
                    <p class="text-flexible">Duis aute irure dolor in reprehender in volupate</p>
                    </center>
                </div>

                <div class="col-md-4">
                    <center>
                   <img width="90px" src="imgs/visit%20us.png"  />
                    <h4>Visit Us</h4>
                    <p class="text-flexible">Duis aute irure dolor in reprehender in volupate</p>
                    </center>
                </div>


            </div>
        </div>
    </section>



</asp:Content>
