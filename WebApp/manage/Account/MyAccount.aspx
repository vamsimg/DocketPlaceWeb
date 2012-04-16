<%@ Page Title="" Language="C#" MasterPageFile="~/web.Master" AutoEventWireup="true" CodeBehind="MyAccount.aspx.cs" Inherits="WebApp.manage.Account.MyAccount" %>



<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">

     <h2>My Account</h2>

     <asp:HyperLink ID="DetailsHyperLink" runat="server" NavigateUrl="/manage/Account/UpdateDetails.aspx">My Details</asp:HyperLink>
     <br />
     <asp:HyperLink ID="UpdateEmailHyperLink" runat="server" NavigateUrl="/manage/Account/UpdateEmail.aspx">Update Email</asp:HyperLink>
     <br />
     <asp:HyperLink ID="UpdatePasswordHyperLink" runat="server" NavigateUrl="/manage/Account/UpdatePassword.aspx">Update Password</asp:HyperLink>

</asp:Content>

