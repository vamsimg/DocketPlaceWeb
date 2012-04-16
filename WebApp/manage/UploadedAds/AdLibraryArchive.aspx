<%@ Page Title="" Language="C#" MasterPageFile="~/web.Master" AutoEventWireup="true" CodeBehind="AdLibraryArchive.aspx.cs" Inherits="WebApp.manage.UploadedAds.AdLibraryArchive" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">

	<h1>Ad Library Archive</h1>
     
	     
	<asp:Panel ID="AdLibraryPanel" runat="server" CssClass="AdLibraryPanel">
	
		<asp:Label ID="AdUpdateErrorLabel" runat="server" CssClass="errorMessage"></asp:Label>	
          
		<div class="brclear"></div>
	
		<asp:ListView ID="AdLibraryListView" runat="server" DataKeyNames="uploadedad_id" >
			<ItemTemplate>
		          <asp:Label ID="IDLabelView" runat="server" Text="Uploaded Ad ID: " CssClass="leftLabel"></asp:Label>
					<asp:Label ID="IDLabel" runat="server" Text='<%# Eval("uploadedad_id") %>' CssClass="textField" />				
				<div class="brclear"></div>
		
				<asp:Label ID="TitleViewLabel" runat="server" Text="Title: " CssClass="leftLabel"></asp:Label>
					<asp:Label ID="titleLabel" runat="server" Text='<%# Eval("title") %>' CssClass="textField" />				
				<div class="brclear"></div>
		          
				<asp:Label ID="AdViewLabel" runat="server" Text="Image: " CssClass="leftLabel"></asp:Label>
					<asp:Image ID="AdImage" runat="server" ImageUrl='<%# Helpers.GenerateImage(Eval("data").ToString()) %>'  CssClass="textField" />
				<div class="brclear"></div>
		        
				<asp:Label ID="FooterViewLabel" runat="server" Text="Footer: " CssClass="leftLabel"></asp:Label>
					<asp:Label ID="footerLabel" runat="server" Text='<%# Eval("footer") %>' />
		          
				<div class="brclear"></div>
		        
				<asp:Label ID="NotesViewLabel" runat="server" Text="Notes: " CssClass="leftLabel"></asp:Label>
					<asp:Label ID="notesLabel" runat="server" Text='<%# Eval("notes") %>' />
				
				<div class="brclear"></div>
				
				<asp:Label ID="CreationViewLabel" runat="server" Text="Created: " CssClass="leftLabel"></asp:Label>
					<asp:Label ID="CreationLabel" runat="server" Text='<%# Eval("creation_datetime") %>' />
				
				<div class="brclear"></div>
				
				<asp:HyperLink ID="UpdateAdHyperLink" runat="server" NavigateUrl= '<%# "/manage/UploadedAds/UpdateUploadedAd.aspx?uploadedad_id=" + Eval("uploadedad_id") %>' >Edit Ad</asp:HyperLink>
				
				<asp:Button ID="ActivateAdButton" runat="server" Text="Activate Ad" CommandArgument='<%# Eval("uploadedad_id") %>' 
					OnCommand="ActivateAdButton_Command" />
				
				<div class="brclear"></div>		        
			</ItemTemplate>
		     
			<LayoutTemplate>
				<div ID="itemPlaceholderContainer" runat="server" style="">
					<span ID="itemPlaceholder" runat="server" />
				</div>
		          
			</LayoutTemplate>
		</asp:ListView>

	</asp:Panel>
</asp:Content>

