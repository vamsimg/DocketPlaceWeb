<%@ Page Title="" Language="C#" MasterPageFile="~/web.Master" AutoEventWireup="true" CodeBehind="UpdateDetails.aspx.cs" Inherits="WebApp.manage.Account.UpdateDetails" %>



<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">

	<h1>Update Details</h1>

     <asp:Label ID="FirstNameLabel" runat="server" Text="* First Name:" CssClass="leftLabel" AssociatedControlID="FirstNameTextBox"></asp:Label>
		<asp:TextBox ID="FirstNameTextBox" runat="server" Columns="50" MaxLength="50" CssClass="textField"></asp:TextBox>
		<asp:RequiredFieldValidator ID="FirstNameRequiredFieldValidator" runat="server" ErrorMessage="Please enter your first name." ControlToValidate="FirstNameTextBox" Display="dynamic" CssClass="errorMessage"></asp:RequiredFieldValidator>

	<div class="brclear"></div>   

	<asp:Label ID="LastNameLabel" runat="server" Text="* Last Name:" CssClass="leftLabel" AssociatedControlID="LastNameTextBox"></asp:Label>
		<asp:TextBox ID="LastNameTextBox" runat="server" Columns="50" MaxLength="50" CssClass="textField"></asp:TextBox>
		<asp:RequiredFieldValidator ID="LastNameRequiredFieldValidator" runat="server" ErrorMessage="Please enter your last name." ControlToValidate="LastNameTextBox" Display="dynamic" CssClass="errorMessage"></asp:RequiredFieldValidator>

	<div class="brclear"></div> 
     <asp:Label ID="PhoneNumberLabel" runat="server" Text="* Phone Number:" CssClass="leftLabel" AssociatedControlID="PhoneTextBox"></asp:Label>
		<asp:TextBox ID="PhoneTextBox" runat="server" Columns="15" MaxLength="15" CssClass="textField" ></asp:TextBox>
		<asp:RequiredFieldValidator ID="PhoneRequiredFieldValidator" runat="server" ErrorMessage="* Please enter a phone number for your account." ControlToValidate="PhoneTextBox" Display="dynamic" CssClass="errorMessage" ></asp:RequiredFieldValidator>
		
	<div class="brclear"></div> 	
	
	<asp:Label ID="MobileNumberLabel" runat="server" Text="* Mobile Number:" CssClass="leftLabel" AssociatedControlID="MobileTextBox"></asp:Label>
		<asp:TextBox ID="MobileTextBox" runat="server" Columns="10" MaxLength="10" CssClass="textField" ValidationGroup="Details" ></asp:TextBox>
		<asp:RegularExpressionValidator ID="MobileRegularExpressionValidator" runat="server" ErrorMessage="Invalid mobile number. Mobile must start with '04' and have no spaces."  ControlToValidate="MobileTextBox" Display="dynamic" CssClass="errorMessage" ValidationExpression="(04)?[0-9]{8}"></asp:RegularExpressionValidator>
	     <asp:RequiredFieldValidator ID="MobileRequiredFieldValidator" runat="server" ErrorMessage="* Please enter a mobile number for your account." ControlToValidate="MobileTextBox" Display="dynamic" CssClass="errorMessage" ></asp:RequiredFieldValidator>
		

	<div class="brclear"></div>
      
     <asp:Button ID="UpdateDetailsSubmitButton" runat="server" Text="Update Details" OnClick="UpdateDetailsSubmitButton_Click"/>
	
	<br />
	<br />
	<asp:Label ID="StatusLabel" runat="server" CssClass="errorMessage"></asp:Label>
	
     
          
</asp:Content>

