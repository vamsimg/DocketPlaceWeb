<%@ Page Title="" Language="C#" MasterPageFile="~/web.Master" AutoEventWireup="true" CodeBehind="ManageAdGroup.aspx.cs" Inherits="WebApp.manage.AdGroups.ManageAdGroup" %>


<%@ Register src="AdMatchUserControl.ascx" tagname="AdMatchUserControl" tagprefix="uc1" %>
<%@ Register src="NewMatchesUserControl.ascx" tagname="NewMatchesUserControl" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">
 
     <h1>Manage Ad Group</h1>
     
     <asp:Panel ID="DetailsPanel" runat="server">
          
		<asp:Label ID="AdGroupTitleViewLabel" runat="server" Text="Ad Group Title: " CssClass="leftLabel"></asp:Label>
			<asp:Label ID="AdGroupTitleFieldLabel" runat="server" CssClass="textField" />
      
		<div class="brclear"></div>
	     
		<asp:Label ID="CampaignTitleViewLabel" runat="server" Text="Campaign Title: " CssClass="leftLabel"></asp:Label>
			<asp:Label ID="CampaignTitleFieldLabel" runat="server" CssClass="textField" />

		<div class="brclear"></div>	      

		<asp:Label ID="NotesViewLabel" runat="server" Text="Notes: " CssClass="leftLabel"></asp:Label>
			<asp:Label ID="NotesFieldLabel" runat="server"  CssClass="textField"  />
		<div class="brclear"></div>
	                 
		<asp:Label ID="CreatorLabel" runat="server" Text="Created By: " CssClass="leftLabel"></asp:Label>
			<asp:Label ID="CreatorFieldLabel" runat="server"  CssClass="textField"  />
		<div class="brclear"></div> 
			
		<asp:Label ID="CreatedLabel" runat="server" Text="Creation Date: " CssClass="leftLabel"></asp:Label>
			<asp:Label ID="CreatedFieldLabel" runat="server"  CssClass="textField" />
		<div class="brclear"></div>               
	     
		<asp:Label ID="StartDateLabel" runat="server" Text="Start Date: " CssClass="leftLabel"></asp:Label>
			<asp:Label ID="StartDateFieldLabel" runat="server"   CssClass="textField" />
		<div class="brclear"></div>
     
		<asp:Label ID="EndDateLabel" runat="server" Text="End Date: " CssClass="leftLabel"></asp:Label>
			<asp:Label ID="EndDateFieldLabel" runat="server"  CssClass="textField" />
		<div class="brclear"></div>	
          
		<asp:Label ID="TotalDaysLabel" runat="server" Text="Total # of Days: " CssClass="leftLabel"></asp:Label>
			<asp:Label ID="TotalDaysFieldLabel" runat="server"  CssClass="textField" />
		<div class="brclear"></div>	    
		
          <asp:HyperLink ID="UpdateAdGroupHyperLink" runat="server" >Edit Details</asp:HyperLink>
     </asp:Panel>

     <br />
  
     <asp:Panel ID="AdMatchesPanel" runat="server">
     
          <h2>Ads in Stores</h2>  
          
          <asp:HyperLink ID="CreateAdMatchesHyperLink" runat="server">+ Create New Placements</asp:HyperLink>
          
          <br />
          <br />
          <br />
          <%--<asp:Button ID="PublishButton" runat="server" Text="Publish Selected Matches" onclick="PublishButton_Click" />   
               <asp:Label ID="PublishErrorLabel" runat="server" CssClass="errorMessage"></asp:Label>
          
          <div class="brclear"></div>--%>
          
                              
        
          <br /> 
     </asp:Panel>
</asp:Content>

