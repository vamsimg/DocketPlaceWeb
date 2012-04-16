<%@ Page Title="" Language="C#" MasterPageFile="~/web.Master" AutoEventWireup="true" CodeBehind="EmailDocket.aspx.cs" Inherits="WebApp.EmailDocket" %>



<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">

	<h2>Send this receipt to your email</h2>

	<asp:Label ID="EmailLabel" runat="server" Text="* Email:" CssClass="leftLabel" AssociatedControlID="EmailTextBox" ValidationGroup="email"></asp:Label>
		<asp:TextBox ID="EmailTextBox" runat="server" CssClass="textField" Columns="50" MaxLength="50"></asp:TextBox>
		<asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="EmailTextBox" SetFocusOnError="True" ErrorMessage="Email is required" Display="dynamic" CssClass="errorMessage" ValidationGroup="email"></asp:RequiredFieldValidator>							   	
		<asp:RegularExpressionValidator ID="EmailRegularExpressionValidator" runat="server" ErrorMessage="The email address is not valid. Check if theres an extra space at the end of the email."  CssClass="errorMessage"
			ControlToValidate="EmailTextBox" ValidationExpression="^([a-zA-Z0-9_\-\.+]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$" 
			Display="Dynamic"
			ValidationGroup="email">
		</asp:RegularExpressionValidator>
	
	<div class="brclear"></div>

	<asp:Button ID="SubmitButton" runat="server" Text="Submit" ValidationGroup="email" onclick="SubmitButton_Click" />
	
	<span class="errorMessage"><asp:Literal ID="EmailErrorLiteral" runat="server"></asp:Literal></span>

</asp:Content>

