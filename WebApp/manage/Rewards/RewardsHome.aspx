<%@ Page Title="" Language="C#" MasterPageFile="~/web.Master" AutoEventWireup="true" CodeBehind="RewardsHome.aspx.cs" Inherits="WebApp.manage.Rewards.RewardsHome" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">

	<h1>Customer Relations</h1>


	
	<a href ="Triggers.aspx">Triggers</a>		
	<br />
	<br />

	<a href ="AllCustomers.aspx">Our Customers</a>		
	<br />
	<br />
	
	<a href ="CustomerLists.aspx">Customer Lists</a>		
	<br />
	<br />
		

	<a href ="ViewTransactions.aspx">Sales</a>

	<br />
	<br />

	<a href ="FindCustomer.aspx">Find a Customer</a>

	<br />
	<br />
	
	<asp:Panel ID="EnableRewardsPanel" runat="server" Visible="false" Enabled="false">
		
		The Rewards program is not yet active. To enable Rewards click here:  <asp:Button ID="EnableRewardsButton" runat="server" Text="Enable Rewards" onclick="EnableRewardsButton_Click" />			
		<br />
		<br />
		
		<span class="errorMessage"><asp:Literal ID="EnableRewardsErrorLiteral" runat="server" EnableViewState="false"></asp:Literal></span>

	</asp:Panel>		

	<asp:Panel ID="ActiveModulePanel" runat="server" Visible="false" Enabled="false">
		
		<a href="ValidateVoucher.aspx">Validate Voucher</a>

		<br />
		<br />

		<a href ="ConfigureRewards.aspx">Configure Settings</a>

		<br />
		<br />

	</asp:Panel>	


</asp:Content>

