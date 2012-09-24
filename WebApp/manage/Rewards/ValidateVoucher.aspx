<%@ Page Title="" Language="C#" MasterPageFile="~/web.Master" AutoEventWireup="true" CodeBehind="ValidateVoucher.aspx.cs" Inherits="WebApp.manage.Rewards.ValidateVoucher" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

     <h1>Validate Voucher</h1>

     <span class="leftLabel">Voucher ID: </span>	
		<asp:TextBox ID="VoucherIDTextBox" runat="server" CssClass="textField"></asp:TextBox>
          <asp:RequiredFieldValidator ID="VoucherIDTextBoxRequiredFieldValidator" runat="server" ErrorMessage="Please enter a voucher ID" ControlToValidate="VoucherIDTextBox" Display="dynamic" CssClass="errorMessage"></asp:RequiredFieldValidator>

	<div class="brclear"></div>
	
	<span class="leftLabel">Voucher Code: </span>	
		<asp:TextBox ID="VoucherCodeTextBox" runat="server" CssClass="textField"></asp:TextBox>
          <asp:RequiredFieldValidator ID="VoucherCodeTextBoxRequiredFieldValidator" runat="server" ErrorMessage="Please enter a voucher code" ControlToValidate="VoucherCodeTextBox" Display="dynamic" CssClass="errorMessage"></asp:RequiredFieldValidator>

	<div class="brclear"></div>

     <asp:Button ID="ValidateButton" runat="server" Text="Validate" onclick="ValidateButton_Click" />

     <asp:Label ID="ValidateErrorLabel" runat="server" CssClass="errorMessage" EnableViewState="false"></asp:Label>
		

</asp:Content>
