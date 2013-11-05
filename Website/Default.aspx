<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <section class="featured">
        <div class="content-wrapper">
            <hgroup class="title">
                <h1><%: Title %>.</h1>
                <h2>Modify this template to jump-start your ASP.NET application.</h2>
            </hgroup>
            <p>
                To learn more about ASP.NET, visit <a href="http://asp.net" title="ASP.NET Website">http://asp.net</a>. 
                The page features <mark>videos, tutorials, and samples</mark> to help you get the most from 
                ASP.NET. If you have any questions about ASP.NET visit 
                <a href="http://forums.asp.net/18.aspx" title="ASP.NET Forum">our forums</a>.
            </p>
        </div>
    </section>
</asp:Content>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h3>Customers</h3>
    <p>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="CustomerDataSource">
            <Columns>
                <asp:BoundField DataField="CustomerID" HeaderText="CustomerID" SortExpression="CustomerID" />
                <asp:BoundField DataField="CompanyName" HeaderText="CompanyName" SortExpression="CompanyName" />
                <asp:TemplateField HeaderText="ContactName" SortExpression="ContactName">
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("ContactTitle") %>'></asp:Label>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("ContactName") %>'></asp:Label>
                        <asp:Label ID="Label8" runat="server" Text='<%# Bind("Phone") %>'></asp:Label>
                        <asp:Label ID="Label9" runat="server" Text='<%# Bind("Fax") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Address" SortExpression="Address">
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("Address") %>'></asp:Label>
                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("City") %>'></asp:Label>
                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("Region") %>'></asp:Label>
                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("PostalCode") %>'></asp:Label>
                        <asp:Label ID="Label7" runat="server" Text='<%# Bind("Country") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Orders">
                    <ItemTemplate>
                        <asp:GridView ID="Orders" runat="server" DataSource='<%# Eval("Orders") %>'></asp:GridView>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:ObjectDataSource ID="CustomerDataSource" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetCustomers" TypeName="NorthWindSystem.BLL.NorthwindManager"></asp:ObjectDataSource>
    </p>
    
</asp:Content>