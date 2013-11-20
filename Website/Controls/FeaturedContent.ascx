<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FeaturedContent.ascx.cs" Inherits="Controls_FeaturedContent" %>

    <section class="featured">
        <div class="content-wrapper">
            <hgroup class="title">
                <h1><asp:Literal ID="PageTitle" runat="server" /></h1>
                <h2>Northwind Traders &amp; Entity Framework</h2>
            </hgroup>
            <p>
                The Entity Framework auto-generates a central DAL class and various Entity and other classes to represent database tables and result sets from stored procedures. Entity classes are essentially <em>object graphs</em> &mdash; classes composed of other Entities and collections of entities. <em>LINQ-to-Entities</em> provides a great way to build <a id="About" runat="server" href="~/About.aspx">"sproc-lite"</a> applications.
            </p>
        </div>
    </section>
