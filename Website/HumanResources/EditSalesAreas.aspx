<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="EditSalesAreas.aspx.cs" Inherits="HumanResources_EditSalesAreas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .regionDescription
        {
            font-weight:bold;
            font-size: 1.1em;
            padding-bottom: 3px;
        }

        .territoryDescription
        {
            color: white;
            background-color:#777777;
            margin: 3px, 5px;
            border-radius: 7px;
            -moz-border-radius: 7px;
            -webkit-border-radius: 7px;
            border: 1px solid #AAAAAA;
        }
    </style>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
    <h1>Edit Sales Areas</h1>
    <asp:Label ID="MessageLabel" runat="server" />
    <asp:ListView ID="RegionListView" runat="server">
        <LayoutTemplate>
            <div id="itemPlaceholder" runat="server"></div>
        </LayoutTemplate>
        <ItemSeparatorTemplate>
            <hr />
        </ItemSeparatorTemplate>
        <ItemTemplate>
            <span class="regionDescription"><%# Eval("RegionDescription") %></span>
            <div>
                <asp:Repeater ID="TerritoryRepeater" runat="server" DataSource='<%# Eval("Territories") %>'>
                    <ItemTemplate>
                        <span class="territoryDescription"><%# Eval("TerritoryDescription") %></span>
                    </ItemTemplate>
                    <SeparatorTemplate>, </SeparatorTemplate>
                </asp:Repeater>
            </div>
        </ItemTemplate>
        <EditItemTemplate>
        </EditItemTemplate>
        <InsertItemTemplate>
        </InsertItemTemplate>
    </asp:ListView>
</asp:Content>

