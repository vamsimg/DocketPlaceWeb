﻿<%@ Page Title="" Language="C#" MasterPageFile="~/web.Master" AutoEventWireup="true" CodeBehind="ConfigureRewards.aspx.cs" Inherits="WebApp.manage.Rewards.ConfigureRewards" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">

	<h1>Configure Rewards</h1>

	
	<span class="leftLabel">Points Per Dollar: </span>
		<asp:DropDownList ID="PPDDropDownList" runat="server">
			<asp:ListItem Value="1">1</asp:ListItem>
			<asp:ListItem Value="5">5</asp:ListItem>
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
				<asp:ListItem Value="500">500</asp:ListItem>
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
				<asp:ListItem Value="10">10</asp:ListItem>
				<asp:ListItem Value="20">20</asp:ListItem>
				<asp:ListItem Value="30">30</asp:ListItem>
			</asp:DropDownList>
		
		<div class="brclear"></div>		
	
	</asp:Panel>

	<asp:Button ID="SaveButton" runat="server" Text="Save Settings" onclick="SaveButton_Click" /> or <a href ="/manage/Rewards/RewardsHome.aspx">Cancel</a>

	<div class="brclear"></div>		
	
	<span class="errorMessage"><asp:Literal ID="SaveErrorLiteral" runat="server"></asp:Literal></span>

	
</asp:Content>

