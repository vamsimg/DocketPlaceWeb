<%@ Page Title="" Language="C#" MasterPageFile="~/web.Master" AutoEventWireup="true" CodeBehind="UpdateEmail.aspx.cs" Inherits="WebApp.manage.Account.UpdateEmail" %>


<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">
     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

	<h1>Update Email</h1>

     <asp:Panel ID="EmailPanel" runat="server" DefaultButton="UpdateEmailButton">
          <h4>New Email</h4>
          
          Current Email: <asp:Label ID="CurrentEmailLabel" runat="server" Text=""></asp:Label>
          
          <br />
          <br />
          <br />
          
          <asp:Label ID="NewEmailLabel" runat="server" Text="* New Email:" CssClass="leftLabel" AssociatedControlID="EmailTextBox"></asp:Label>
	          <asp:TextBox ID="EmailTextBox" runat="server" CssClass="textField" Columns="50" MaxLength="50" ValidationGroup="NewAdmin"></asp:TextBox>
	          <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="EmailTextBox" SetFocusOnError="True" ErrorMessage="* Email is required" Display="dynamic" CssClass="errorMessage" ValidationGroup="ChangeEmail"></asp:RequiredFieldValidator>							   	
	          <asp:RegularExpressionValidator ID="EmailRegularExpressionValidator" runat="server" ErrorMessage="The email address is not valid."  CssClass="errorMessage"
		          ControlToValidate="EmailTextBox" ValidationExpression="^([a-zA-Z0-9_\-\.+]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$" 
		          Display="Dynamic"
		          ValidationGroup="ChangeEmail">
	          </asp:RegularExpressionValidator>
     	
          <div class="brclear"></div>    
     			    
          <asp:Label ID="ConfirmEmailLabel" runat="server" Text="* Confirm New Email:" CssClass="leftLabel" AssociatedControlID="ConfirmEmailTextBox"></asp:Label>
	          <asp:TextBox ID="ConfirmEmailTextBox" runat="server" CssClass="textField" Columns="50" MaxLength="50" ValidationGroup="ChangeEmail"></asp:TextBox>
		          <asp:CompareValidator ID="EmailCompare" runat="server" ControlToCompare="EmailTextBox" ControlToValidate="ConfirmEmailTextBox" SetFocusOnError="True" ErrorMessage="* Email Addresses do not match." Display="dynamic" CssClass="errorMessage" ValidationGroup="ChangeEmail"></asp:CompareValidator>
		          <asp:RequiredFieldValidator ID="ConfirmEmailRequired" runat="server" ControlToValidate="ConfirmEmailTextBox" SetFocusOnError="True" ErrorMessage="* Please Confirm above Email" Display="dynamic" CssClass="errorMessage" ValidationGroup="ChangeEmail"></asp:RequiredFieldValidator>
     	
          <div class="brclear"></div>
          
          <asp:Button ID="UpdateEmailButton" runat="server" Text="Update Email" ValidationGroup="ChangeEmail" onclick="UpdateEmailButton_Click" />
          
		 <div class="brclear"></div>
           <asp:Label ID="ChangeEmailErrorLabel" runat="server" Text="" CssClass="errorMessage"></asp:Label>
		<div class="brclear"></div> 
          
     
     </asp:Panel>
     
   

</asp:Content>

