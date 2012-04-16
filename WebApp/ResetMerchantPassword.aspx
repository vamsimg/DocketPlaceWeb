<%@ Page Title="" Language="C#" MasterPageFile="~/web.Master" AutoEventWireup="true" CodeBehind="ResetMerchantPassword.aspx.cs" Inherits="WebApp.ResetMerchantPassword" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">

	<h1>Merchant Password Reset</h1>			
	
	<hr id="titleLine" />
	
	<br />
	
	<span class="errorMessage"><asp:Literal ID="ErrorMessage" runat="server"></asp:Literal></span>
		
	<asp:Label ID="EmailLabel" runat="server" CssClass="leftLabel" AssociatedControlID="EmailTextBox">Please enter your email address.</asp:Label>
		<asp:TextBox ID="EmailTextBox" runat="server" CssClass="textField" Columns="50"></asp:TextBox>
		<asp:RequiredFieldValidator ID="EmailRequiredFieldValidator" runat="server" ControlToValidate="EmailTextBox" CssClass="errorMessage">* Email is required</asp:RequiredFieldValidator>
	
	<div class="brclear"></div>
	
	<asp:Button ID="ResetPasswordButton" runat="server" Text="Reset Password" 
		CssClass="submitButton" onclick="ResetPasswordButton_Click"  />
				
	<p class="failuretext"><asp:Literal ID="FailureTextLiteral" runat="server" EnableViewState="False" ></asp:Literal></p>		
		



</asp:Content>

