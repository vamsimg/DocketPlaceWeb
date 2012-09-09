<%@ Page Title="" Language="C#" MasterPageFile="~/web.Master" AutoEventWireup="true" CodeBehind="ConfigureRewards.aspx.cs" Inherits="WebApp.manage.Rewards.ConfigureRewards" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">

	<h1>Configure Rewards</h1>

	
	<span class="leftLabel">Points Per Dollar: </span>
		<asp:DropDownList ID="PPDDropDownList" runat="server">
               <asp:ListItem Value="0">0</asp:ListItem>
			<asp:ListItem Value="1">1</asp:ListItem>
			<asp:ListItem Value="2">2</asp:ListItem>
			<asp:ListItem Value="3">3</asp:ListItem>
               <asp:ListItem Value="4">4</asp:ListItem>
			<asp:ListItem Value="5">5</asp:ListItem>
			<asp:ListItem Value="6">6</asp:ListItem>
               <asp:ListItem Value="7">7</asp:ListItem>
			<asp:ListItem Value="8">8</asp:ListItem>
			<asp:ListItem Value="9">9</asp:ListItem>
               <asp:ListItem Value="10">10</asp:ListItem>
			
		</asp:DropDownList>

	<div class="brclear"></div>	

	<span class="leftLabel">Enable Vouchers ? </span>

	<asp:CheckBox ID="EnableVouchersCheckBox" runat="server" oncheckedchanged="EnableVouchersCheckBox_CheckedChanged" AutoPostBack="True"  />

	<div class="brclear"></div>	

	<asp:Panel ID="VouchersPanel" runat="server">	

		<span class="leftLabel">Points Threshold: </span>
			<asp:DropDownList ID="PointsThresholdDropDownList" runat="server">
				<asp:ListItem Value="100">100</asp:ListItem>
                    <asp:ListItem Value="100">150</asp:ListItem>
				<asp:ListItem Value="200">200</asp:ListItem>
				<asp:ListItem Value="300">300</asp:ListItem>
                    <asp:ListItem Value="400">400</asp:ListItem>
				<asp:ListItem Value="500">500</asp:ListItem>
                    <asp:ListItem Value="600">600</asp:ListItem>
				<asp:ListItem Value="700">700</asp:ListItem>
                    <asp:ListItem Value="800">800</asp:ListItem>
				<asp:ListItem Value="900">900</asp:ListItem>
                    <asp:ListItem Value="1000">1000</asp:ListItem>
			</asp:DropDownList>

		<div class="brclear"></div>			

		<span class="leftLabel">Voucher Amount: </span>
			<asp:DropDownList ID="VoucherAmountDropDownList" runat="server">
				<asp:ListItem Value="5">5</asp:ListItem>
				<asp:ListItem Value="10">10</asp:ListItem>
				<asp:ListItem Value="20">20</asp:ListItem>
			</asp:DropDownList>

		<div class="brclear"></div>	

		<asp:Button ID="CalculateCostButton" runat="server" Text="Calculate Cost per Customer" onclick="CalculateCostButton_Click" /> <span class="errorMessage"><asp:Literal ID="CostLiteral" runat="server"></asp:Literal></span>
	
		<div class="brclear"></div>		

		<br />

		<span class="leftLabel">Days before Voucher Expires:</span>
			<asp:DropDownList ID="ExpiryDropDownList" runat="server">
                    <asp:ListItem Value="5">5</asp:ListItem>
				<asp:ListItem Value="10">10</asp:ListItem>
				<asp:ListItem Value="15">15</asp:ListItem>
				<asp:ListItem Value="20">20</asp:ListItem>
                    <asp:ListItem Value="21">21</asp:ListItem>
                    <asp:ListItem Value="22">22</asp:ListItem>
                    <asp:ListItem Value="23">23</asp:ListItem>
                    <asp:ListItem Value="24">24</asp:ListItem>
				<asp:ListItem Value="25">25</asp:ListItem>
				<asp:ListItem Value="30">30</asp:ListItem>
			</asp:DropDownList>
		
		<div class="brclear"></div>		
	
	</asp:Panel>

	<asp:Button ID="SaveButton" runat="server" Text="Save Settings" onclick="SaveButton_Click" /> or <a href ="/manage/Rewards/RewardsHome.aspx">Cancel</a>

	<div class="brclear"></div>		
	
	<span class="errorMessage"><asp:Literal ID="SaveErrorLiteral" runat="server"></asp:Literal></span>

	
</asp:Content>

