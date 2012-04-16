<%@ Page Title="" Language="C#" MasterPageFile="~/web.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebApp.Login" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">
	<h1>Merchant Login</h1>			
	     
	<hr id="titleLine" />
	
	<br />
	Forgot your password ? <asp:HyperLink ID="ForgottenPasswordHyperLink" runat="server" NavigateUrl="/ResetPassword.aspx"> Click here to obtain a new one.</asp:HyperLink>	 
	
	<br />
     <br />
	<br />
     <asp:Panel ID="LoginPanel" runat="server" DefaultButton="LoginButton">
     
	     <asp:Label ID="EmailLabel" runat="server" CssClass="leftLabel" AssociatedControlID="EmailTextBox">Email:</asp:Label>
		     <asp:TextBox ID="EmailTextBox" runat="server"  Columns="50" CssClass="textField" ValidationGroup="login"></asp:TextBox>
		     <asp:RequiredFieldValidator ID="EmailRequiredFieldValidator" runat="server" ControlToValidate="EmailTextBox" Display="Dynamic" CssClass="errorMessage"  ValidationGroup="login">* Email is required</asp:RequiredFieldValidator>

	     <br />
	     <br />
     			
	     <asp:Label ID="PasswordLabel" runat="server" CssClass="leftLabel" AssociatedControlID="PasswordTextBox" >Password:</asp:Label>
		     <asp:TextBox ID="PasswordTextBox" runat="server" TextMode="Password"  Columns="50" CssClass="textField"  ValidationGroup="login"></asp:TextBox>
		     <asp:RequiredFieldValidator id="PasswordRequiredFieldValidator" runat="server" ControlToValidate="PasswordTextBox" Display="Dynamic" CssClass="errorMessage"  ValidationGroup="login">* Password is required.</asp:RequiredFieldValidator>
     			
	     <br />
	     <br />   
     	
	     <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Log In" OnClick="LoginButton_Click" ValidationGroup="login" />
     	
	     <br />
	     <br />
	     <asp:Label ID="LoginErrorLabel" runat="server" Text="" CssClass="errorMessage"></asp:Label>
     	
	
	</asp:Panel>		 
	
	

</asp:Content>

