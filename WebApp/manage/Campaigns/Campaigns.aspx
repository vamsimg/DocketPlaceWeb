<%@ Page Title="" Language="C#" MasterPageFile="~/web.Master" AutoEventWireup="true" CodeBehind="Campaigns.aspx.cs" Inherits="WebApp.manage.Campaigns.Campaigns" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">
	<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

	

    	<h1>Campaigns</h1>
		
	<a href="/manage/Campaigns/CreateNewCampaign.aspx">Create a new Campaign</a>     

     <br />
     <br />

     <asp:GridView ID="CampaignsGridView" runat="server" AutoGenerateColumns="False" CssClass="gridview">
		<EmptyDataTemplate>None</EmptyDataTemplate>
          <Columns>
               <asp:HyperLinkField HeaderText="Title" DataNavigateUrlFields="campaign_id" DataTextFormatString ="{0:G}" DataNavigateUrlFormatString="/manage/Campaigns/ManageCampaign.aspx?campaign_id={0}" Text="{0}" DataTextField="title" />				              
               
               <asp:BoundField DataField="start_datetime" HeaderText="Start Date" DataFormatString="{0:dd/MM/yyyy}" htmlencode="false"  />
               <asp:BoundField DataField="end_datetime" HeaderText="End Date" DataFormatString="{0:dd/MM/yyyy}" htmlencode="false"  />	
               <asp:BoundField DataField="creation_datetime" HeaderText="Created" DataFormatString="{0:dd/MM/yyyy}" htmlencode="false"  />
		                             
          </Columns>
     </asp:GridView>

    
     

</asp:Content>

