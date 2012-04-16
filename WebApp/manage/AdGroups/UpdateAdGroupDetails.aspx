<%@ Page Title="" Language="C#" MasterPageFile="~/web.Master" AutoEventWireup="true" CodeBehind="UpdateAdGroupDetails.aspx.cs" Inherits="WebApp.manage.AdGroups.UpdateAdGroupDetails" %>


<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">
     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
     
	     
     <h2>Update Ad Group Details</h2>
     
        
     <asp:Label ID="TitleLabel" runat="server" Text="Ad Group Title" CssClass="leftLabel"></asp:Label>
               <asp:TextBox ID="TitleTextBox" runat="server" CssClass="textField" Columns="50" MaxLength="200" ></asp:TextBox>
               <asp:RequiredFieldValidator ID="TitleRequiredFieldValidator" runat="server" ErrorMessage="Enter a title for the ad." ControlToValidate="TitleTextBox" ValidationGroup="NewAdGroup" ></asp:RequiredFieldValidator>
         
     <div class="brclear"></div> 


     <asp:Label ID="NotesLabel" runat="server" Text="Notes:" CssClass="leftLabel" AssociatedControlID="NotesTextBox"></asp:Label>
	               <asp:TextBox ID="NotesTextBox" runat="server" Columns="40" Rows="4" 
                         MaxLength="1000" CssClass="textField" ValidationGroup="NewStore" TextMode="MultiLine">
                    </asp:TextBox>
     
     <div class="brclear"></div> 
     <br />

     <asp:Label ID="StartDateLabel1" runat="server" Text="Start Date:" CssClass="leftLabel" AssociatedControlID="StartDateTextBox"></asp:Label>
	          <asp:TextBox ID="StartDateTextBox" runat="server" Columns="10" CssClass="textField"></asp:TextBox>
	          <cc1:CalendarExtender ID="StartDateCalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="StartDateTextBox"></cc1:CalendarExtender>
	          <cc1:MaskedEditExtender ID="StartDateMaskedEditExtender" runat="server" Mask="99/99/9999" MaskType="Date" TargetControlID="StartDateTextBox"></cc1:MaskedEditExtender>
	          <asp:RequiredFieldValidator ID="StartDateRequiredFieldValidator" runat="server" ErrorMessage="Start Date Required" Display="dynamic" ControlToValidate="StartDateTextBox" ValidationGroup="NewAdGroup"></asp:RequiredFieldValidator>
     
     <div class="brclear"></div> 

     <asp:Label ID="EndDateLabel1" runat="server" Text="End Date:" CssClass="leftLabel" AssociatedControlID="EndDateTextBox"></asp:Label>
	          <asp:TextBox ID="EndDateTextBox" runat="server" Columns="10" CssClass="textField"></asp:TextBox>
	          <cc1:CalendarExtender ID="EndDateCalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="EndDateTextBox"></cc1:CalendarExtender>
	          <cc1:MaskedEditExtender ID="EndDateMaskedEditExtender" runat="server" Mask="99/99/9999" MaskType="Date" TargetControlID="EndDateTextBox"></cc1:MaskedEditExtender>
	          <asp:RequiredFieldValidator ID="EndDateRequiredFieldValidator" runat="server" ErrorMessage="End Date Required" Display="dynamic" ControlToValidate="EndDateTextBox" ValidationGroup="NewAdGroup"></asp:RequiredFieldValidator>
		     <asp:CompareValidator ID="DateCompareValidator" runat="server" ErrorMessage="End date must be later than start date." Display="Dynamic" ControlToCompare="StartDateTextBox" ControlToValidate="EndDateTextBox" Operator="GreaterThan" Type="Date" ValidationGroup="NewAdGroup"></asp:CompareValidator>
     
     <div class="brclear"></div> 

     <div class="brclear"></div> 
     
     <asp:Button ID="UpdateAdGroupButton" runat="server" Text="Update Ad Group" onclick="UpdateAdGroupButton_Click" ValidationGroup="NewAdGroup"/>
		
	or  <asp:HyperLink ID="BackAdGroupHyperLink" runat="server" >Go Back</asp:HyperLink>

     <div class="brclear"></div> 

     <asp:Label ID="UpdateAdGroupErrorLabel" runat="server" Text="" CssClass="errorMessage" EnableViewState="false"></asp:Label>
     

</asp:Content>

