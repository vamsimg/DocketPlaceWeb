<%@ Page Title="" Language="C#" MasterPageFile="~/web.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="WebApp.manage.Dashboard" %>



<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">
	
	<h1>Dashboard</h1>


	<div id="CurrentCampaigns">
		
		<h3>Current Campaigns</h3>
			
		<asp:GridView ID="CampaignsGridView" runat="server" AutoGenerateColumns="False" CssClass="gridview">
			<EmptyDataTemplate>None</EmptyDataTemplate>
			<Columns>
				<asp:HyperLinkField HeaderText="Title" DataNavigateUrlFields="campaign_id" DataTextFormatString ="{0:G}" DataNavigateUrlFormatString="/manage/Campaigns/ManageCampaign.aspx?campaign_id={0}" Text="{0}" DataTextField="title" />				              
               
				<asp:BoundField DataField="start_datetime" HeaderText="Start Date" DataFormatString="{0:dd/MM/yyyy}" htmlencode="false"  />
				<asp:BoundField DataField="end_datetime" HeaderText="End Date" DataFormatString="{0:dd/MM/yyyy}" htmlencode="false"  />	
				<asp:BoundField DataField="creation_datetime" HeaderText="Created" DataFormatString="{0:dd/MM/yyyy}" htmlencode="false"  />
		                             
			</Columns>
		</asp:GridView>
		

	</div>	

</asp:Content>

