<%@ Page Title="" Language="C#" MasterPageFile="~/web.Master" AutoEventWireup="true" CodeBehind="UpdatePassword.aspx.cs" Inherits="WebApp.manage.Account.UpdatePassword" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">

  <asp:Panel ID="PasswordPanel" runat="server" DefaultButton="ChangePasswordButton">

          <h1>Change Password</h1>   

          <div class="brclear"></div>  
                   
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

          <div class="brclear"></div>


          <asp:Label ID="PasswordChangeErrorLabel" runat="server" Text="" CssClass="errorMessage"></asp:Label>

          <div class="brclear"></div>
                   
     </asp:Panel> 

</asp:Content>

