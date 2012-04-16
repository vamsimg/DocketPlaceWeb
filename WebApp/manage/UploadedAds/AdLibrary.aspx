<%@ Page Title="" Language="C#" MasterPageFile="~/web.Master" AutoEventWireup="true" CodeBehind="AdLibrary.aspx.cs" Inherits="WebApp.manage.UploadedAds.AdLibrary" %>


<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">
	<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

     <h1>Ad Library</h1>
     
	
	

     <asp:Button ID="NewAdPopupButton" runat="server" Text="+ Upload a New Ad" /> or go to <asp:HyperLink ID="ArchiveHyperLink" runat="server" NavigateUrl="/manage/UploadedAds/AdLibraryArchive.aspx">Archived Ads</asp:HyperLink>	
     
	<br />
	<br />
  
	<asp:Panel ID="AdLibraryPanel" runat="server" CssClass="AdLibraryPanel">
		
		<asp:Label ID="AdUpdateErrorLabel" runat="server" CssClass="errorMessage"></asp:Label>	
          
		<div class="brclear"></div>
		
		<asp:ListView ID="AdLibraryListView" runat="server" DataKeyNames="uploadedad_id" >
		     <ItemTemplate>
               <div class="DiscreteAdPanel">
			     <asp:Label ID="IDLabelView" runat="server" Text="Uploaded Ad ID: " CssClass="leftLabel"></asp:Label>
				     <asp:Label ID="IDLabel" runat="server" Text='<%# Eval("uploadedad_id") %>' CssClass="textField" />				
			     <div class="brclear"></div>

			     <asp:Label ID="TitleViewLabel" runat="server" Text="Title: " CssClass="leftLabel"></asp:Label>
				     <asp:Label ID="titleLabel" runat="server" Text='<%# Eval("title") %>' CssClass="textField" />				
			     <div class="brclear"></div>
     	          
			     <asp:Label ID="AdViewLabel" runat="server" Text="Image: " CssClass="leftLabel"></asp:Label>
				     <asp:Image ID="AdImage" runat="server" ImageUrl='<%# Helpers.GenerateImage(Eval("data").ToString()) %>'  CssClass="textField" BorderStyle="Solid" BorderColor="Black" BorderWidth="1px" />
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
     			
			     <asp:Button ID="DeactivateAdButton" runat="server" Text="Archive Ad" CommandArgument='<%# Eval("uploadedad_id") %>'  		              
						     OnCommand="DeactivateAdButton_Command" />			

			     <div class="brclear"></div>	
			
               </div>
			
	        
		</ItemTemplate>
	     
		     <LayoutTemplate>
			<div ID="itemPlaceholderContainer" runat="server" style="">
				<span ID="itemPlaceholder" runat="server" />
			</div>
	          
		</LayoutTemplate>
	     </asp:ListView>    
	    
	</asp:Panel>

      
     


     <cc1:ModalPopupExtender ID="NewAdModalPopupExtender" runat="server" PopupControlID="NewAdPanel" OkControlID="DummyButton"  CancelControlID="CancelButton" DropShadow="True" BackgroundCssClass="modalBackground" TargetControlID="NewAdPopupButton" DynamicServicePath="" Enabled="True"></cc1:ModalPopupExtender>
			<asp:Button ID="DummyButton" runat="server" style="display:none" />    


     <asp:Panel ID="NewAdPanel" runat="server" CssClass="modalPopup">
          
          <h4>Upload a new Ad</h4>
          
           <asp:Label ID="TitleLabel" runat="server" Text="* Title for Ad" CssClass="leftLabel"></asp:Label>
               <asp:TextBox ID="TitleTextBox" runat="server" CssClass="textField" Columns="50" MaxLength="200" ></asp:TextBox>
               <asp:RequiredFieldValidator ID="TitleRequiredFieldValidator" runat="server" ErrorMessage="Enter a title for the ad." ControlToValidate="TitleTextBox" ValidationGroup="UploadAd" Display="Dynamic"></asp:RequiredFieldValidator>
          
          <div class="brclear"></div>
          
        
		<br />
		<asp:Label ID="AdSelectLabel" runat="server" Text="Select File:" CssClass="leftLabel" AssociatedControlID="AdFileUpload"></asp:Label>
		<asp:FileUpload ID="AdFileUpload" runat="server" CssClass="textField" Width="30em" /> 
		     <asp:RegularExpressionValidator ID="ExtensionRegularExpressionValidator" runat="server" ErrorMessage="File must end in png" ControlToValidate="AdFileUpload" ValidationExpression="..+(\.png)$" ValidationGroup="UploadAd" Display="Dynamic"></asp:RegularExpressionValidator>
			<asp:RequiredFieldValidator ID="AdFileUploadRequiredFieldValidator" runat="server" ErrorMessage="Please select an Ad to upload" ControlToValidate="AdFileUpload" ValidationGroup="UploadAd" Display="Dynamic"></asp:RequiredFieldValidator>
		<div class="brclear"></div>
		
          <asp:Label ID="FooterLabel" runat="server" Text="Footer:" CssClass="leftLabel" AssociatedControlID="FooterTextBox"></asp:Label>
               <asp:TextBox ID="FooterTextBox" runat="server" Columns="50" Rows="2" 
                    MaxLength="100" CssClass="textField" TextMode="MultiLine">
               </asp:TextBox>
               <cc1:TextBoxWatermarkExtender ID="FooterTextBox_TextBoxWatermarkExtender" WatermarkText="Any info to display at the bottom of your ad. eg:Expiry date, codes etc. "
                    runat="server" Enabled="True" TargetControlID="FooterTextBox">
               </cc1:TextBoxWatermarkExtender>
		
		
		<div class="brclear"></div>
		<br />
		
		<asp:Label ID="NotesLabel" runat="server" Text="Notes:" CssClass="leftLabel" AssociatedControlID="NotesTextBox"></asp:Label>
		     <asp:TextBox ID="NotesTextBox" runat="server" Columns="40" Rows="5" 
                    MaxLength="1000" CssClass="textField" ValidationGroup="UploadAd" TextMode="MultiLine">
               </asp:TextBox>
		     
		<div class="brclear"></div>
          <br />
		
          <asp:Button ID="UploadPicButton" runat="server" Text="Upload Picture" ValidationGroup="UploadAd" UseSubmitBehavior="false" onclick="UploadPicButton_Click" CssClass="modalOkButton" />
          <asp:Button ID="CancelButton" runat="server" Text="Cancel" CssClass="modalCancelButton" />

     	<div class="brclear"></div>
		
          	

		<asp:Label ID="AdUploadErrorLabel" runat="server" CssClass="errorMessage" EnableViewState="false"></asp:Label>	
          
     </asp:Panel>



</asp:Content>

