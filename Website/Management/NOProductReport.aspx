<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="NOProductReport.aspx.cs" Inherits="Management_NOProductReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <h3>Product Sales Report - Section NO</h3>
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="100%">
        <LocalReport ReportPath="Reports\NOProductSalesReport.rdlc">
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="BLLDataSource" Name="NOProductDataSet" />
            </DataSources>
        </LocalReport>
    </rsweb:ReportViewer>
    <asp:ObjectDataSource ID="BLLDataSource" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetProductSaleSummaries" TypeName="NorthWindSystem.BLL.NorthwindManager"></asp:ObjectDataSource>
</asp:Content>

