<%@ Page Title="" Language="C#" MasterPageFile="~/web.Master" AutoEventWireup="true" CodeBehind="UpdateDetails.aspx.cs" Inherits="WebApp.manage.Customers.UpdateDetails" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">

	<h1>Update Details</h1>

	<h3>Personal Details</h3>

		<span class="shortLeftLabel">First Name:</span>
			<asp:TextBox ID="FirstNameTextBox" runat="server" Columns="50" MaxLength="50" CssClass="textField"></asp:TextBox>
			<asp:RequiredFieldValidator ID="FirstNameRequiredFieldValidator" runat="server" ErrorMessage="Please enter your first name." ControlToValidate="FirstNameTextBox" Display="dynamic" CssClass="errorMessage" ValidationGroup="name"></asp:RequiredFieldValidator>

		<div class="brclear"></div>   

		<span class="shortLeftLabel">Last Name: </span>
			<asp:TextBox ID="LastNameTextBox" runat="server" Columns="50" MaxLength="50" CssClass="textField"></asp:TextBox>
			<asp:RequiredFieldValidator ID="LastNameRequiredFieldValidator" runat="server" ErrorMessage="Please enter your last name." ControlToValidate="LastNameTextBox" Display="dynamic" CssClass="errorMessage"  ValidationGroup="name"></asp:RequiredFieldValidator>

		<div class="brclear"></div>

		<span class="shortLeftLabel">Postcode:</span>
		<asp:TextBox ID="PostcodeTextBox" runat="server" Columns="4" MaxLength="4" CssClass="textField"></asp:TextBox>
			<asp:RegularExpressionValidator ID="PostcodeRegularExpressionValidator" runat="server" ErrorMessage="Postcode must be a number" Display="Dynamic" ValidationExpression="\d{4}" ControlToValidate="PostcodeTextBox" ValidationGroup="Details"></asp:RegularExpressionValidator>
				

		<div class="brclear"></div>
		
		<asp:Button ID="UpdateNameButton" runat="server" Text="Update Details"  ValidationGroup="name" onclick="UpdateNameButton_Click" />
		
		
		<asp:Label ID="NameErrorLabel" runat="server" Text="" CssClass="errorMessage" EnableViewState="false"></asp:Label>
		
		<div class="brclear"></div> 

		<br />
		<br />

	<hr />
		
	<h3>Email Address</h3>

		Current Email: <asp:Label ID="CurrentEmailLabel" runat="server" Text=""></asp:Label>
          
          <br />
          <br />
        
          <asp:Label ID="NewEmailLabel" runat="server" Text="* New Email:" CssClass="leftLabel" AssociatedControlID="EmailTextBox"></asp:Label>
	          <asp:TextBox ID="EmailTextBox" runat="server" CssClass="textField" Columns="50" MaxLength="50" ValidationGroup="NewAdmin"></asp:TextBox>
	          <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="EmailTextBox" SetFocusOnError="True" ErrorMessage="* Email is required" Display="dynamic" CssClass="errorMessage" ValidationGroup="ChangeEmail"></asp:RequiredFieldValidator>							   	
	          <asp:RegularExpressionValidator ID="EmailRegularExpressionValidator" runat="server" ErrorMessage="The email address is not valid."  CssClass="errorMessage"
		          ControlToValidate="EmailTextBox" ValidationExpression="^([a-zA-Z0-9_\-\.+]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$" 
		          Display="Dynamic"
		          ValidationGroup="email">
	          </asp:RegularExpressionValidator>
     	
          <div class="brclear"></div>    
     			    
          <asp:Label ID="ConfirmEmailLabel" runat="server" Text="* Confirm New Email:" CssClass="leftLabel" AssociatedControlID="ConfirmEmailTextBox"></asp:Label>
	          <asp:TextBox ID="ConfirmEmailTextBox" runat="server" CssClass="textField" Columns="50" MaxLength="50" ValidationGroup="ChangeEmail"></asp:TextBox>
		          <asp:CompareValidator ID="EmailCompare" runat="server" ControlToCompare="EmailTextBox" ControlToValidate="ConfirmEmailTextBox" SetFocusOnError="True" ErrorMessage="* Email Addresses do not match." Display="dynamic" CssClass="errorMessage"  ValidationGroup="email"></asp:CompareValidator>
		          <asp:RequiredFieldValidator ID="ConfirmEmailRequired" runat="server" ControlToValidate="ConfirmEmailTextBox" SetFocusOnError="True" ErrorMessage="* Please Confirm above Email" Display="dynamic" CssClass="errorMessage"  ValidationGroup="email"></asp:RequiredFieldValidator>
     	
          <div class="brclear"></div>
          
          <asp:Button ID="UpdateEmailButton" runat="server" Text="Update Email"  ValidationGroup="email" onclick="UpdateEmailButton_Click" />
          
		<div class="brclear"></div>
          
		<asp:Label ID="EmailErrorLabel" runat="server" Text="" CssClass="errorMessage" EnableViewState="false"></asp:Label>
		
		<div class="brclear"></div> 

	
	<br />
	<br />

	<hr />	


	<h3>Mobile Number </h3>

		<div class="brclear"></div> 
		<span class="shortLeftLabel">Mobile: </span>
			<asp:TextBox ID="MobileTextBox" runat="server" Columns="15" MaxLength="15" CssClass="textField" ></asp:TextBox>
			<asp:RequiredFieldValidator ID="MobileRequiredFieldValidator" runat="server" ErrorMessage="* Please enter a mobile number starting with 04" ControlToValidate="MobileTextBox" Display="dynamic" CssClass="errorMessage"  ValidationGroup="mobile" ></asp:RequiredFieldValidator>
		
			<div class="brclear"></div>
			<asp:Button ID="UpdateMobileButton" runat="server" Text="Change Mobile"  ValidationGroup="mobile" onclick="UpdateMobileButton_Click" />
			
			<asp:Label ID="MobileErrorLabel" runat="server" Text="" CssClass="errorMessage" EnableViewState="false"></asp:Label>
			
			<div class="brclear"></div> 	
	
	<br />
	<br />

	<hr />

	<h3>Password</h3>	

	<asp:Label ID="NewPasswordLabel" runat="server" Text="New Password" CssClass="leftLabel"></asp:Label>
          <asp:TextBox ID="NewPasswordTextBox" runat="server" CssClass="textField" Columns="50" MaxLength="50" TextMode="Password"></asp:TextBox>
          <asp:RequiredFieldValidator ID="NewPasswordRequiredFieldValidator" runat="server" ErrorMessage="Enter New Password" ControlToValidate="NewPasswordTextBox" ValidationGroup="Password" ></asp:RequiredFieldValidator>

     <div class="brclear"></div>    

     <asp:Label ID="ConfirmNewPasswordLabel" runat="server" Text="Confirm New Password" CssClass="leftLabel"></asp:Label>
          <asp:TextBox ID="ConfirmNewPasswordTextBox" runat="server" CssClass="textField" Columns="50" MaxLength="50" TextMode="Password"></asp:TextBox>
          <asp:RequiredFieldValidator ID="ConfirmNewPasswordRequiredFieldValidator" runat="server" ErrorMessage="Confirm New Password" ControlToValidate="ConfirmNewPasswordTextBox"  ValidationGroup="Password"></asp:RequiredFieldValidator>
          <asp:CompareValidator ID="PasswordCompareValidator" runat="server" ControlToCompare="NewPasswordTextBox" ControlToValidate="ConfirmNewPasswordTextBox" SetFocusOnError="True" ErrorMessage="* Passwords do not match." CssClass="errorMessage"  ValidationGroup="Password"></asp:CompareValidator>

     <div class="brclear"></div>
          			
     <asp:button id="ChangePasswordButton" runat="server" text="Change Password" onclick="ChangePasswordButton_Click"  ValidationGroup="Password" UseSubmitBehavior="false" />

     <asp:Label ID="PasswordChangeErrorLabel" runat="server" Text="" CssClass="errorMessage"></asp:Label>

</asp:Content>