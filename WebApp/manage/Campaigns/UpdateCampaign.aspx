<%@ Page Title="" Language="C#" MasterPageFile="~/web.Master" AutoEventWireup="true" CodeBehind="UpdateCampaign.aspx.cs" Inherits="WebApp.manage.Campaigns.UpdateCampaign" %>


<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
	
	<h1>Update Campaign</h1>

	<asp:Label ID="TitleViewLabel" runat="server" Text="Title: " CssClass="leftLabel"></asp:Label>
		<asp:TextBox ID="TitleTextBox" runat="server" Columns="100" MaxLength="100" CssClass="textField" ></asp:TextBox>
			<asp:RequiredFieldValidator ID="TitleRequiredFieldValidator" runat="server" ControlToValidate="TitleTextBox" ErrorMessage="Required" CssClass="errorMessage" Display="Dynamic" ValidationGroup="NewCampaign"></asp:RequiredFieldValidator>
	<div class="brclear"></div>
      
	<asp:Label ID="NotesViewLabel" runat="server" Text="Notes: " CssClass="leftLabel"></asp:Label>
		<asp:TextBox ID="NotesTextBox" runat="server" Columns="76" Rows="5" MaxLength="1000" TextMode="MultiLine" CssClass="textField" ></asp:TextBox>

	<div class="brclear"></div>              
	<br />	 
             
    <asp:Label ID="StartDateLabel" runat="server" Text="Start Date:" CssClass="leftLabel" AssociatedControlID="StartDateTextBox"></asp:Label>
		          <asp:TextBox ID="StartDateTextBox" runat="server" Columns="10" CssClass="textField"></asp:TextBox>
		          <cc1:CalendarExtender ID="StartDateCalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="StartDateTextBox"></cc1:CalendarExtender>
		          <cc1:MaskedEditExtender ID="StartDateMaskedEditExtender" runat="server" Mask="99/99/9999" MaskType="Date" TargetControlID="StartDateTextBox"></cc1:MaskedEditExtender>
		          <asp:RequiredFieldValidator ID="StartDateRequiredFieldValidator" runat="server" ErrorMessage="Start Date Required" Display="dynamic" ControlToValidate="StartDateTextBox" ValidationGroup="NewCampaign"></asp:RequiredFieldValidator>
          
     <div class="brclear"></div> 

     <asp:Label ID="EndDateLabel" runat="server" Text="End Date:" CssClass="leftLabel" AssociatedControlID="EndDateTextBox"></asp:Label>
	          <asp:TextBox ID="EndDateTextBox" runat="server" Columns="10" CssClass="textField"></asp:TextBox>
	          <cc1:CalendarExtender ID="EndDateCalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="EndDateTextBox"></cc1:CalendarExtender>
	          <cc1:MaskedEditExtender ID="EndDateMaskedEditExtender" runat="server" Mask="99/99/9999" MaskType="Date" TargetControlID="EndDateTextBox"></cc1:MaskedEditExtender>
	          <asp:RequiredFieldValidator ID="EndDateRequiredFieldValidator" runat="server" ErrorMessage="End Date Required" Display="dynamic" ControlToValidate="EndDateTextBox" ValidationGroup="NewCampaign" ></asp:RequiredFieldValidator>
		     <asp:CompareValidator ID="DateCompareValidator" runat="server" ErrorMessage="End date must be later than start date." Display="Dynamic" ControlToCompare="StartDateTextBox" ControlToValidate="EndDateTextBox" Operator="GreaterThan" Type="Date" ValidationGroup="NewCampaign"></asp:CompareValidator>
     <div class="brclear"></div> 
	<div class="brclear"></div>	

	<asp:Button ID="UpdateCampaignButton" runat="server" Text="Update" CssClass="submitButton" onclick="UpdateCampaignButton_Click" ValidationGroup="NewCampaign" /> or <asp:HyperLink ID="CancelHyperLink" runat="server">Cancel</asp:HyperLink>

	<div class="brclear"></div>
	
	<asp:Label ID="UpdateCampaignErrorLabel" runat="server" Text="" CssClass="errorMessage"></asp:Label>


</asp:Content>

