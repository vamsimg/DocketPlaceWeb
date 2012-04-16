<%@ Page Title="" Language="C#" MasterPageFile="~/web.Master" AutoEventWireup="true" CodeBehind="CreateNewCustomerList.aspx.cs" Inherits="WebApp.manage.Rewards.CreateNewCustomerList" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">

	<h1>Create a New List</h1>
	
	<span class="leftLabel">List Title</span>
		<asp:TextBox ID="TitleTextBox" runat="server" CssClass="textField" Columns="50" MaxLength="200" ></asp:TextBox>
		<asp:RequiredFieldValidator ID="TitleRequiredFieldValidator" runat="server" ErrorMessage="Enter a title for the list." ControlToValidate="TitleTextBox" ></asp:RequiredFieldValidator>
	              
	<div class="brclear"></div> 

	<span class="leftLabel">Criteria</span>	

	<asp:RadioButtonList ID="ListTypeRadioButtonList" runat="server" CssClass="textField">	
		<asp:ListItem Value="total_revenue" Selected="True">Top Customers by Purchase Total</asp:ListItem>
		<asp:ListItem Value="frequency">Top Customers by Visits</asp:ListItem>
	</asp:RadioButtonList>

	<div class="brclear"></div> 	

	<span class="leftLabel">Number of Customers </span>
		<asp:TextBox ID="NumCustomersTextBox" runat="server" Columns="10" MaxLength="10" CssClass="textField">100</asp:TextBox>
		<asp:RangeValidator ID="NumCustomersRangeValidator" runat="server" ErrorMessage="RangeValidator" Display="Dynamic" ControlToValidate="NumCustomersTextBox" Type="Integer" MinimumValue="0" MaximumValue="1000"></asp:RangeValidator>    
	<div class="brclear"></div> 	

	<asp:Button ID="CreateButton" runat="server" Text="Create" onclick="CreateButton_Click" />
	<br />
	<asp:Label ID="CreateListErrorLabel" runat="server" CssClass="errorMessage"></asp:Label>

</asp:Content>

