<%@ Page Title="" Language="C#" MasterPageFile="~/web.Master" AutoEventWireup="true" CodeBehind="ProcessReplies.aspx.cs" Inherits="WebApp.admin.ProcessReplies" %>


<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>


	<asp:DropDownList ID="CompaniesDropDownList" runat="server" DataSourceID="CompaniesSqlDataSource" DataTextField="name" DataValueField="company_id">
	</asp:DropDownList>	

     <div class="brclear"></div> 

	<asp:SqlDataSource ID="CompaniesSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:ConnString %>" SelectCommand="SELECT [company_id], [name] FROM [Companies]"></asp:SqlDataSource>

     <div class="brclear"></div> 

     <span class="leftLabel">Start Date:</span>
			<asp:TextBox ID="StartDateTextBox" runat="server" Columns="10" CssClass="textField"></asp:TextBox>
			<cc1:calendarextender ID="StartDateCalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="StartDateTextBox"></cc1:calendarextender>
			<cc1:maskededitextender ID="StartDateMaskedEditExtender" runat="server" Mask="99/99/9999" MaskType="Date" TargetControlID="StartDateTextBox"></cc1:maskededitextender>
			<asp:RequiredFieldValidator ID="StartDateRequiredFieldValidator" runat="server" ErrorMessage="Start Date Required" Display="dynamic" ControlToValidate="StartDateTextBox" ValidationGroup="NewInvoice"></asp:RequiredFieldValidator>
	          
	<div class="brclear"></div> 
     
     <asp:Button ID="CheckButton" runat="server" Text="Process" onclick="CheckButton_Click" />
</asp:Content>
