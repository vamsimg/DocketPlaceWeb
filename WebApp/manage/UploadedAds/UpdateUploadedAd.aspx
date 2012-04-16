<%@ Page Title="" Language="C#" MasterPageFile="~/web.Master" AutoEventWireup="true" CodeBehind="UpdateUploadedAd.aspx.cs" Inherits="WebApp.manage.UploadedAds.UpdateUploadedAd" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">

	<h1>Update Ad Details</h1>
          
     <asp:Label ID="TitleLabel" runat="server" Text="Title for Ad" CssClass="leftLabel"></asp:Label>
          <asp:TextBox ID="TitleTextBox" runat="server" CssClass="textField" Columns="50" MaxLength="200" ></asp:TextBox>
          <asp:RequiredFieldValidator ID="TitleRequiredFieldValidator" runat="server" ErrorMessage="Enter a title for the ad." ControlToValidate="TitleTextBox" ValidationGroup="UpdateAdDetails" ></asp:RequiredFieldValidator>
     
     <div class="brclear"></div>
	
     <asp:Label ID="FooterLabel" runat="server" Text="Footer:" CssClass="leftLabel" AssociatedControlID="FooterTextBox"></asp:Label>
          <asp:TextBox ID="FooterTextBox" runat="server" Columns="40" Rows="5" 
               MaxLength="1000" CssClass="textField" TextMode="MultiLine">
          </asp:TextBox>              
	
	
	<div class="brclear"></div>
	<br />
	
	<asp:Label ID="NotesLabel" runat="server" Text="Notes:" CssClass="leftLabel" AssociatedControlID="NotesTextBox"></asp:Label>
	     <asp:TextBox ID="NotesTextBox" runat="server" Columns="40" Rows="4" 
               MaxLength="1000" CssClass="textField" ValidationGroup="UpdateAdDetails" TextMode="MultiLine">
          </asp:TextBox>
	     
	<div class="brclear"></div>
	
	<asp:Label ID="ActiveLabel" runat="server" Text="Is this ad active ?" CssClass="leftLabel" AssociatedControlID="ActiveRadioButtonList"></asp:Label>
		<asp:RadioButtonList ID="ActiveRadioButtonList"  runat="server">
			<asp:ListItem Value="True" Selected ="True" >Yes</asp:ListItem>
			<asp:ListItem Value="False" >No</asp:ListItem>
		</asp:RadioButtonList>

     <br />
	
	<asp:Label ID="CreationViewLabel" runat="server" Text="Created on: " CssClass="leftLabel"></asp:Label>
			<asp:Label ID="CreationLabel" runat="server" />

	<div class="brclear"></div>

	<asp:Button ID="UpdateDetailsButton" runat="server" Text="Update Details" 
          ValidationGroup="UpdateAdDetails" UseSubmitBehavior="false" 
		 onclick="UpdateDetailsButton_Click" /> or <a href="AdLibrary.aspx">Cancel</a>
	
	<div class="brclear"></div>
	
	<asp:Label ID="UpdateDetailsErrorLabel" runat="server" CssClass="errorMessage"></asp:Label>	
     


	<h2>Preview</h2>

	<asp:Label ID="AdSelectLabel" runat="server" Text="Select File:" CssClass="leftLabel" AssociatedControlID="AdFileUpload"></asp:Label>
		<asp:FileUpload ID="AdFileUpload" runat="server" CssClass="textField" Width="30em" /> 
		     <asp:RegularExpressionValidator ID="ExtensionRegularExpressionValidator" runat="server" ErrorMessage="File must end in png" ControlToValidate="AdFileUpload" ValidationExpression="..+(\.png)$" ValidationGroup="UploadAd" Display="Dynamic"></asp:RegularExpressionValidator>
			<asp:RequiredFieldValidator ID="AdFileUploadRequiredFieldValidator" runat="server" ErrorMessage="Please select an Ad to upload" ControlToValidate="AdFileUpload" ValidationGroup="UploadAd" Display="Dynamic"></asp:RequiredFieldValidator>
	<div class="brclear"></div>

	<asp:Button ID="UpdateImageButton" runat="server" Text="Update Image" ValidationGroup="UploadAd" onclick="UpdateImageButton_Click" />

	<div class="brclear"></div>

	<asp:Label ID="AdViewLabel" runat="server" Text="Image: " CssClass="leftLabel"></asp:Label>
		<asp:Image ID="AdImage" runat="server" CssClass="textField" BorderStyle="Solid" BorderColor="Black" BorderWidth="1px"  />
	<div class="brclear"></div>
		


</asp:Content>

