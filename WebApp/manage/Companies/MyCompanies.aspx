<%@ Page Title="" Language="C#" MasterPageFile="~/web.Master" AutoEventWireup="true" CodeBehind="MyCompanies.aspx.cs" Inherits="WebApp.manage.Companies.MyCompanies" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">

	<h1>My Companies</h1>
     
	<i>Select a company: </i>


     <asp:DropDownList ID="CompaniesDropDownList" runat="server" DataTextField="name"    DataValueField="company_id">
     </asp:DropDownList>
     
     <br />
	<br />
     
     <asp:Button ID="SelectButton" runat="server" Text="Go to Company Portal" onclick="SelectButton_Click" />

  
</asp:Content>

