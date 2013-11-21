<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="EditSalesAreas.aspx.cs" Inherits="HumanResources_EditSalesAreas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .instructions
        {
            float: right;
            width: 400px;
            background-color: #E0EEFB;
            padding: 5px;
        }
            .instructions h6
            {
                margin-top: 5px;
            }

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
            padding: 0px 3px;
            border-radius: 7px;
            -moz-border-radius: 7px;
            -webkit-border-radius: 7px;
            border: 1px solid #AAAAAA;
            margin-bottom: 4px;
            display: inline-block;
        }

        input.medimWidth
        {
            width:100px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
    <h1>Edit Sales Areas</h1>
    <asp:Label ID="MessageLabel" runat="server" />
    <asp:ListView ID="RegionListView" runat="server" OnItemCommand="RegionListView_ItemCommand"
        InsertItemPosition="FirstItem">
        <LayoutTemplate>
            <div>
                <div id="itemPlaceholder" runat="server"></div>
            </div>
        </LayoutTemplate>
        <ItemSeparatorTemplate>
            <hr />
        </ItemSeparatorTemplate>
        <ItemTemplate>
            <asp:LinkButton ID="EditButton" runat="Server" Text="Edit" CommandName="Edit" />
            <span class="regionDescription"><%# Eval("RegionDescription") %></span>
            <blockquote>
                <asp:Repeater ID="TerritoryRepeater" runat="server" DataSource='<%# Eval("Territories") %>'>
                    <ItemTemplate>
                        <span class="territoryDescription"><%# Eval("TerritoryDescription") %></span>
                    </ItemTemplate>
                    <SeparatorTemplate>, </SeparatorTemplate>
                </asp:Repeater>
            </blockquote>
        </ItemTemplate>
        <EditItemTemplate>
            <asp:LinkButton ID="UpdateButton" runat="server" CommandName="Update" Text="Update" />
            <asp:LinkButton ID="CancelButton" runat="server" CommandName="Cancel" Text="Cancel" />
            <asp:TextBox id="RegionDescription" runat="server" Text='<%# Eval("RegionDescription") %>' />
            <asp:HiddenField ID="RegionID" runat="server" Value='<%# Eval("RegionID") %>' />
            <blockquote>
                <asp:Repeater ID="TerritoryRepeater" runat="server" DataSource='<%# Eval("Territories") %>'>
                    <ItemTemplate>
                        <asp:HiddenField ID="TerritoryID" runat="server" Value='<%# Eval("TerritoryID") %>' />
                        <asp:TextBox id="TerritoryDescription" runat="server" Text='<%# Eval("TerritoryDescription") %>' 
                            CssClass="medimWidth" />
                    </ItemTemplate>
                </asp:Repeater>
            </blockquote>
        </EditItemTemplate>
        <InsertItemTemplate>
            <div class="instructions">
                <h6>Instructions</h6>
                Enter a name for the region and enter the region's territories as a comma-separated list.For example:<br />
                <b>Region Name/Description:</b> <em>Canada</em><br />
                <b>Territory Names/Descriptions:</b> <em>Edmonton, Regina, Ontario</em>
            </div>
            <asp:Label ID="Label1" runat="server" AssociatedControlID="RegionDescription">Region Name/Description:</asp:Label>
            <asp:TextBox id="RegionDescription" runat="server" />
            <asp:Label ID="Label2" runat="server" AssociatedControlID="TerritoryDescriptions">Territory Names/Descriptions:</asp:Label>
            <asp:TextBox id="TerritoryDescriptions" runat="server" TextMode="MultiLine" />
            <br />
            <asp:LinkButton ID="InsertButton" runat="server" CommandName="Insert" Text="Insert" />
        </InsertItemTemplate>
    </asp:ListView>
</asp:Content>

