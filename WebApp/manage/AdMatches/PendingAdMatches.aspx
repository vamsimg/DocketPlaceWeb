<%@ Page Title="" Language="C#" MasterPageFile="~/web.Master" AutoEventWireup="true" CodeBehind="PendingAdMatches.aspx.cs" Inherits="WebApp.manage.AdMatches.PendingAdMatches" %>



<%@ Register src="PendingAdMatchUserControl.ascx" tagname="PendingAdMatchUserControl" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">

     <h1>Ad Matches Waiting for Approval</h1>
          
          <br />
	     <asp:HyperLink ID="BackHyperLink" runat="server"><- Back to Company</asp:HyperLink>	
	     <br />
          <hr id="titleLine" />

          <asp:Panel ID="PendingAdMatchesPanel" runat="server">
              
               

          </asp:Panel>
  



</asp:Content>

