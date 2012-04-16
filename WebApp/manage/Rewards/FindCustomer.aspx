<%@ Page Title="" Language="C#" MasterPageFile="~/web.Master" AutoEventWireup="true" CodeBehind="FindCustomer.aspx.cs" Inherits="WebApp.manage.Rewards.FindCustomer" %>



<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
	
	<h1>Find a Customer</h1>

	<span class="leftLabel">Local Customer Number:</span>	
		<asp:TextBox ID="LocalCustomerIDTextBox" runat="server" ValidationGroup="Search" CssClass="textField"></asp:TextBox>
	<div class="brclear"></div>
	
	<span class="leftLabel">Local Barcode Number:</span>	
		<asp:TextBox ID="BarcodeTextBox" runat="server" ValidationGroup="Search" CssClass="textField"></asp:TextBox>
	<div class="brclear"></div>

	<span class="leftLabel">DocketPlace ID:</span>	
		<asp:TextBox ID="DocketPlaceIDTextBox" runat="server" ValidationGroup="Search" CssClass="textField"></asp:TextBox>
	<div class="brclear"></div>					

	<span class="leftLabel">First Name:</span>	
		<asp:TextBox ID="FirstNameTextBox" runat="server" ValidationGroup="Search" CssClass="textField"></asp:TextBox>
	
	<div class="brclear"></div>
				
	<span class="leftLabel">Last Name:</span>	
		<asp:TextBox ID="LastNameTextBox" runat="server" ValidationGroup="Search" CssClass="textField"></asp:TextBox>

	<div class="brclear"></div>				

	<span class="leftLabel">Mobile:</span>	
		<asp:TextBox ID="MobileTextBox" runat="server" ValidationGroup="Search" CssClass="textField"></asp:TextBox>

	<div class="brclear"></div>
		
	<span class="leftLabel">Phone:</span>	
		<asp:TextBox ID="PhoneTextBox" runat="server" ValidationGroup="Search" CssClass="textField"></asp:TextBox>
	
	<div class="brclear"></div>			
	
	<span class="leftLabel">Email:</span>	
		<asp:TextBox ID="EmailTextBox" runat="server" ValidationGroup="Search" CssClass="textField"></asp:TextBox>

	<div class="brclear"></div>
		
	<asp:Button ID="SearchButton" runat="server" Text="Search" ValidationGroup="Search" onclick="SearchButton_Click" />
	<asp:Label ID="SearchErrorLabel" runat="server" CssClass="errorMessage" EnableViewState="false"></asp:Label>
		
	<br />
	<br />
		
	<asp:GridView ID="CustomersGridView" runat="server" AutoGenerateColumns="False" DataKeyNames="customer_id" EnableViewState="false">
		<Columns>
			<asp:BoundField DataField="customer_id" HeaderText="DocketPlace ID" InsertVisible="False" ReadOnly="True" SortExpression="local_customer_id" />
			<asp:BoundField DataField="local_customer_id" HeaderText="Local ID" InsertVisible="False" ReadOnly="True" SortExpression="local_customer_id" />
			<asp:BoundField DataField="barcode_id" HeaderText="Barcode ID" InsertVisible="False" ReadOnly="True" SortExpression="local_customer_id" />
			<asp:BoundField DataField="title" HeaderText="Title" SortExpression="title" />
			<asp:BoundField DataField="first_name" HeaderText="First Name" SortExpression="first_name" />
			<asp:BoundField DataField="last_name" HeaderText="Last Name" SortExpression="last_name" />			
			<asp:BoundField DataField="postcode" HeaderText="Postcode" SortExpression="postcode" />
			<asp:BoundField DataField="mobile" HeaderText="Mobile" SortExpression="mobile" />
			<asp:BoundField DataField="email" HeaderText="Email" SortExpression="email" />
			<asp:BoundField DataField="phone" HeaderText="Phone" SortExpression="phone" />
			<asp:HyperLinkField DataNavigateUrlFields="customer_id" ItemStyle-CssClass="action"
				DataTextFormatString ="{0:G}"
				DataNavigateUrlFormatString="/manage/Rewards/ViewCustomer.aspx?customer_id={0}"
				Text="View"/>		
		</Columns>
	</asp:GridView>	
</asp:Content>

