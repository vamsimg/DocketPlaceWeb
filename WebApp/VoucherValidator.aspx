<%@ Page Title="" Language="C#" MasterPageFile="~/web.Master" AutoEventWireup="true" CodeBehind="VoucherValidator.aspx.cs" Inherits="WebApp.VoucherValidator" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

     <h1>Validate Voucher</h1>

     <span class="leftLabel">Store ID: </span>
          <asp:TextBox ID="StoreIDTextBox" runat="server"></asp:TextBox> <asp:RequiredFieldValidator ID="IDRequiredFieldValidator" runat="server" ErrorMessage="Store ID Required" CssClass="errorMessage" Display="Dynamic"></asp:RequiredFieldValidator>
	
	<div class="brclear"></div> 

     <span class="leftLabel">Store Password: </span>
          <asp:TextBox ID="StorePasswordTextBox" runat="server" TextMode="Password"></asp:TextBox> <asp:RequiredFieldValidator ID="PasswordRequiredFieldValidator" runat="server" ErrorMessage="Store ID Required" CssClass="errorMessage" Display="Dynamic"></asp:RequiredFieldValidator>
	
     <div class="brclear"></div> 

     <asp:Button ID="TestConnectionButton" runat="server" Text="Test Connection" onclick="TestConnectionButton_Click" /> <asp:Label ID="TestConnectionErrorLabel" runat="server" Text="" CssClass="errorMessage"></asp:Label>
     
	
	<div class="brclear"></div> 

      <span class="leftLabel">Voucher Code: </span>
          <asp:TextBox ID="VoucherCodeTextBox" runat="server" EnableViewState="false"></asp:TextBox> 
	
	<div class="brclear"></div> 

     <asp:Button ID="ValidateVoucherButton" runat="server" Text="Validate Voucher" onclick="ValidateVoucherButton_Click" />

     <div class="brclear"></div> 

     <asp:Label ID="ValidateVoucherErrorLabel" runat="server" Text="" CssClass="errorMessage"></asp:Label>

</asp:Content>
