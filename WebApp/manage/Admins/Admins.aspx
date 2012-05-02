<%@ Page Title="" Language="C#" MasterPageFile="~/web.Master" AutoEventWireup="true" CodeBehind="Admins.aspx.cs" Inherits="WebApp.manage.Admins.Admins" %>


<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

     <h1>Admins</h1>
	
	<asp:GridView ID="AdminsGridView" runat="server" AutoGenerateColumns="False" CssClass="gridview">
          <Columns>
              
              <asp:BoundField DataField="First Name" HeaderText="First Name" />
              <asp:BoundField DataField="Last Name" HeaderText="Last Name" />
              <asp:BoundField DataField="Email" HeaderText="Email" />
              <asp:BoundField DataField="Phone" HeaderText="Phone" />
              <asp:BoundField DataField="Position" HeaderText="Position" />
			<asp:BoundField DataField="Role" HeaderText="Role" />
           
              
              <asp:TemplateField HeaderText="Delete">
              <ItemTemplate>
                    <asp:ImageButton ID="DeletePermissionImageButton" AlternateText="Delete" 
                         runat="server"   CausesValidation="False"  
                         CommandArgument='<%# Eval("PermissionID") %>' 
                         oncommand="DeletePermissionImageButton_Command"
                         ImageUrl="~/icons/delete.png" />						
              </ItemTemplate>
                   <ItemStyle HorizontalAlign="Center" />
              </asp:TemplateField>
           
              
          </Columns>
          
     </asp:GridView>

	<br />
     <br />     

     <asp:Label ID="AdminListErrorLabel" runat="server" Text="" CssClass="errorMessage"></asp:Label>
     
     <br />
     <br />
     
     <div class="brclear"></div> 
    
	<h4>Select User Type</h4>          

		<asp:RadioButtonList ID="AdminTypeRadioButtonList" runat="server" EnableViewState="false">			
			<asp:ListItem Value="Clerk" Selected="True">Clerk (Manage customers and rewards only) </asp:ListItem>
			<asp:ListItem Value="Admin">Admin (Able to create,manage campaigns, manage customers and rewards)</asp:ListItem>			
			<asp:ListItem Value="Owner">Owner (Super user , can do everything)</asp:ListItem>		
		</asp:RadioButtonList>


	 <asp:Panel ID="NewAdminPanel" runat="server" DefaultButton="CreateAdminButton">
          
          <h4>New Admin</h4>
          
	     <asp:Label ID="EmailLabel" runat="server" Text="* Email:" CssClass="leftLabel" AssociatedControlID="EmailTextBox"></asp:Label>
		     <asp:TextBox ID="EmailTextBox" runat="server" CssClass="textField" Columns="50" MaxLength="50" ValidationGroup="NewAdmin"></asp:TextBox>
		     <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="EmailTextBox" SetFocusOnError="True" ErrorMessage="* Email is required" Display="dynamic" CssClass="errorMessage" ValidationGroup="NewAdmin"></asp:RequiredFieldValidator>							   	
		     <asp:RegularExpressionValidator ID="EmailRegularExpressionValidator" runat="server" ErrorMessage="The email address is not valid."  CssClass="errorMessage"
			     ControlToValidate="EmailTextBox" ValidationExpression="^([a-zA-Z0-9_\-\.+]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$" 
			     Display="Dynamic"
			     ValidationGroup="NewAdmin">
		     </asp:RegularExpressionValidator>
     	
	     <div class="brclear"></div>    
     			    
	     <asp:Label ID="ConfirmEmailLabel" runat="server" Text="* Confirm Email:" CssClass="leftLabel" AssociatedControlID="ConfirmEmailTextBox"></asp:Label>
		     <asp:TextBox ID="ConfirmEmailTextBox" runat="server" CssClass="textField" Columns="50" MaxLength="50" ValidationGroup="NewAdmin"></asp:TextBox>
			     <asp:CompareValidator ID="EmailCompare" runat="server" ControlToCompare="EmailTextBox" ControlToValidate="ConfirmEmailTextBox" SetFocusOnError="True" ErrorMessage="* Email Addresses do not match." Display="dynamic" CssClass="errorMessage" ValidationGroup="NewAdmin"></asp:CompareValidator>
			     <asp:RequiredFieldValidator ID="ConfirmEmailRequired" runat="server" ControlToValidate="ConfirmEmailTextBox" SetFocusOnError="True" ErrorMessage="* Please Confirm above Email" Display="dynamic" CssClass="errorMessage" ValidationGroup="NewAdmin"></asp:RequiredFieldValidator>
     	
	     <div class="brclear"></div>
     	
	     <asp:Label ID="FirstNameLabel" runat="server" Text="* First Name:" CssClass="leftLabel" AssociatedControlID="FirstNameTextBox"></asp:Label>
		     <asp:TextBox ID="FirstNameTextBox" runat="server" Columns="50" 
               MaxLength="50" CssClass="textField" ValidationGroup="NewAdmin" ></asp:TextBox>
		     <asp:RequiredFieldValidator ID="FirstNameRequiredFieldValidator" 
               runat="server" ControlToValidate="FirstNameTextBox" SetFocusOnError="True" 
               ErrorMessage="* Please enter user's first name" Display="dynamic" 
               CssClass="errorMessage" ValidationGroup="NewAdmin"></asp:RequiredFieldValidator>
     	
     		
	     <div class="brclear"></div> 
     	
	     <asp:Label ID="LastNameLabel" runat="server" Text="* Last Name:" CssClass="leftLabel" AssociatedControlID="LastNameTextBox"></asp:Label>
		     <asp:TextBox ID="LastNameTextBox" runat="server" Columns="50" 
               MaxLength="50" CssClass="textField" ValidationGroup="NewAdmin" ></asp:TextBox>
		     <asp:RequiredFieldValidator ID="LastNameRequiredFieldValidator" 
               runat="server" ControlToValidate="LastNameTextBox" SetFocusOnError="True" 
               ErrorMessage="* Please enter user's first name" Display="dynamic" 
               CssClass="errorMessage" ValidationGroup="NewAdmin"></asp:RequiredFieldValidator>
     	
	     <div class="brclear"></div> 
     	
	     <asp:Label ID="PhoneNumberLabel" runat="server" Text="* Phone or Mobile:" CssClass="leftLabel" AssociatedControlID="PhoneTextBox"></asp:Label>
		     <asp:TextBox ID="PhoneTextBox" runat="server" Columns="15" MaxLength="15" 
               CssClass="textField" ValidationGroup="NewAdmin" ></asp:TextBox>
		     <asp:RequiredFieldValidator ID="PhoneRequiredFieldValidator" 
               runat="server" 
               ErrorMessage="* Please enter a phone or mobile number for the user." 
               ControlToValidate="PhoneTextBox" Display="dynamic" CssClass="errorMessage" 
               ValidationGroup="NewAdmin" ></asp:RequiredFieldValidator>
     		
	     <div class="brclear"></div> 
     	
     	<asp:Label ID="PositionLabel" runat="server" Text="* Position in your company:" CssClass="leftLabel" AssociatedControlID="PositionTextBox"></asp:Label>
		     <asp:TextBox ID="PositionTextBox" runat="server" Columns="50" 
               MaxLength="100" CssClass="textField" ValidationGroup="NewAdmin" ></asp:TextBox>
		     <asp:RequiredFieldValidator ID="PositionRequiredFieldValidator" 
               runat="server" 
               ErrorMessage="* Please describe how the new admin is related to your company. " 
               ControlToValidate="PositionTextBox" Display="dynamic" CssClass="errorMessage" 
               ValidationGroup="NewAdmin" ></asp:RequiredFieldValidator>
     		
	     <div class="brclear"></div>  
     	
	     <asp:Button ID="CreateAdminButton" runat="server" Text="Create Admin" 
               onclick="CreateAdminButton_Click" ValidationGroup="NewAdmin"/>
     	
	     <div class="brclear"></div>   
     	
     </asp:Panel>

	<br />     

     OR
     
     <br />

     <asp:Panel ID="AddExistingAdminPanel" runat="server" DefaultButton="AddExistingAdminButton">		

          <h4>Existing Admin</h4>
     
          <asp:Label ID="ExistingEmailLabel" runat="server" Text="* Email:" CssClass="leftLabel" AssociatedControlID="ExistingEmailTextBox"></asp:Label>
		     <asp:TextBox ID="ExistingEmailTextBox" runat="server" CssClass="textField" Columns="50" MaxLength="50" ValidationGroup="ExistingEmail"></asp:TextBox>
     	     <asp:RequiredFieldValidator ID="ExistingEmailRequiredFieldValidator" runat="server" ControlToValidate="ExistingEmailTextBox" SetFocusOnError="True" ErrorMessage="* Email is required" Display="dynamic" CssClass="errorMessage" ValidationGroup="ExistingEmail"></asp:RequiredFieldValidator>							   	
     	 
     	<div class="brclear"></div>  
     	
     	<asp:Label ID="ExistingPositionLabel" runat="server" Text="* Position in your company:" CssClass="leftLabel" AssociatedControlID="ExistingEmailTextBox"></asp:Label>
		     <asp:TextBox ID="ExistingPositionTextBox" runat="server" CssClass="textField" Columns="50" MaxLength="50" ValidationGroup="ExistingEmail"></asp:TextBox>
     	     <asp:RequiredFieldValidator ID="RequiredFieldValidator" runat="server" ErrorMessage="* Please describe how the new admin is related to your company. " ControlToValidate="ExistingPositionTextBox" Display="dynamic" CssClass="errorMessage" ValidationGroup="ExistingEmail" ></asp:RequiredFieldValidator>
     		
     	<div class="brclear"></div>  
     	     			
          <asp:Button ID="AddExistingAdminButton" runat="server" Text="Add Admin" onclick="AddExistingAdminButton_Click" ValidationGroup="ExistingEmail"/>	
                             
          <div class="brclear"></div>
     
     </asp:Panel>	
     
    
     
    

</asp:Content>

