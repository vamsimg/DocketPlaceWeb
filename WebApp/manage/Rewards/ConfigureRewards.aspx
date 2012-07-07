<%@ Page Title="" Language="C#" MasterPageFile="~/web.Master" AutoEventWireup="true" CodeBehind="ConfigureRewards.aspx.cs" Inherits="WebApp.manage.Rewards.ConfigureRewards" %>



<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">
     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

	<h1>Configure Rewards</h1>

	
	<span class="leftLabel">Points Per Dollar: </span>		
          <asp:TextBox ID="PPDTextBox" runat="server"></asp:TextBox>
	<cc1:SliderExtender ID="PPDTextBox_SliderExtender" runat="server" Enabled="True" Maximum="20" Minimum="1" Steps="1" TargetControlID="PPDTextBox">
     </cc1:SliderExtender>
	<div class="brclear"></div>	

	<span class="leftLabel">Enable Vouchers ? </span>

	<asp:CheckBox ID="EnableVouchersCheckBox" runat="server" oncheckedchanged="EnableVouchersCheckBox_CheckedChanged" AutoPostBack="True"  />

	<div class="brclear"></div>	

	<asp:Panel ID="VouchersPanel" runat="server">	

		<span class="leftLabel">Points Threshold: </span>
			 <asp:TextBox ID="ThresholdTextBox" runat="server"></asp:TextBox>
                    <cc1:SliderExtender ID="ThresholdTextBox_SliderExtender" runat="server" Enabled="True" Maximum="1000" Minimum="100" Steps="50" TargetControlID="PPDTextBox">
                    </cc1:SliderExtender>

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
			 <asp:TextBox ID="ExpiryDaysTextBox" runat="server"></asp:TextBox>
                    <cc1:SliderExtender ID="ExpiryDaysTextBox_SliderExtender" runat="server" Enabled="True" Maximum="60" Minimum="1" Steps="3" TargetControlID="PPDTextBox">
                    </cc1:SliderExtender>
		
		<div class="brclear"></div>		
	
	</asp:Panel>

	<asp:Button ID="SaveButton" runat="server" Text="Save Settings" onclick="SaveButton_Click" /> or <a href ="/manage/Rewards/RewardsHome.aspx">Cancel</a>

	<div class="brclear"></div>		
	
	<span class="errorMessage"><asp:Literal ID="SaveErrorLiteral" runat="server"></asp:Literal></span>

	
</asp:Content>

