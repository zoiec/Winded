<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

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
    <h3>Employees</h3>
    <p>
        <asp:GridView ID="EmployeeGridView" runat="server" AutoGenerateColumns="False" DataSourceID="EmployeeDataSource" RowStyle-VerticalAlign="Top">
            <Columns>
                <asp:BoundField DataField="EmployeeID" HeaderText="ID" />
                <asp:TemplateField HeaderText="Employee" SortExpression="LastName">
                    <ItemTemplate>
                        <img alt="Employee Photo" src='<%# AsBase64(Eval("Photo")) %>' style="float:left; zoom:0.35; margin-right: 7px;" />
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("FirstName") %>'></asp:Label>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("LastName") %>'></asp:Label>
                        &mdash;
                        <asp:Label ID="Label8" runat="server" Text='<%# Bind("Title") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Address" SortExpression="City">
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("Address") %>'></asp:Label>
                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("City") %>'></asp:Label>
                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("Region") %>'></asp:Label>
                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("PostalCode") %>'></asp:Label>
                        <asp:Label ID="Label7" runat="server" Text='<%# Bind("Country") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Territories">
                    <ItemTemplate>
                        <asp:GridView ID="Orders" runat="server" AutoGenerateColumns="false" DataSource='<%# Eval("Territories") %>'>
                            <Columns>
                                <asp:BoundField DataField="TerritoryDescription" HeaderText="Territory" />
                                <asp:TemplateField HeaderText="Region">
                                    <ItemTemplate>
                                        <asp:Label ID="Label7" runat="server" Text='<%# Bind("Region.RegionDescription") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:ObjectDataSource ID="EmployeeDataSource" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetEmployees" TypeName="NorthWindSystem.BLL.NorthwindManager"></asp:ObjectDataSource>
    </p>
    
</asp:Content>