<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="About.aspx.cs" Inherits="About" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <section class="featured">
        <div class="content-wrapper">
            <hgroup class="title">
                <h1><%: Title %></h1>
                <h2>Northwind Traders &amp; Entity Framework</h2>
            </hgroup>
            <p>
                The Entity Framework auto-generates a central DAL class and various Entity and other classes to represent database tables and result sets from stored procedures. The Entity classes are essentially object graphs &mdash; classes composed of other Entities and collections of entities. The relationships between tables in the database are represented through the composition of the Entity classes.
            </p>
        </div>
    </section>
</asp:Content>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <article>
        <h2>Ad-Hoc Demo Notes</h2>
        <p>
            This is a rapid prototype example to illustrate how to use Entity Framework on a database without stored procedures. Based on the <a href="https://northwinddatabase.codeplex.com/">Northwind Traders database</a>.
        </p>
    </article>

    <aside>
        <h3>Demo Roadmap</h3>
        <p>        
            A few features will be demonstrated in this sample.
        </p>
        <ul>
            <li><asp:CheckBox ID="CheckBox1" runat="server" Enabled="false" Checked="true" />
                Nested GridView to display Object Graph details of an Entity
            </li>
            <li><asp:CheckBox ID="CheckBox2" runat="server" Enabled="false" Checked="true" />
                Display images from byte[] in database
            </li>
            <li><asp:CheckBox ID="CheckBox3" runat="server" Enabled="false" Checked="false" />
                Transactional processing in class library using Linq
            </li>
            <li><asp:CheckBox ID="CheckBox4" runat="server" Enabled="false" Checked="false" />
                TBA
            </li>
            <li><asp:CheckBox ID="CheckBox5" runat="server" Enabled="false" Checked="false" />
                TBA
            </li>
        </ul>
    </aside>
</asp:Content>