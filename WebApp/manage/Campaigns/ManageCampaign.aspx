<%@ Page Title="" Language="C#" MasterPageFile="~/web.Master" AutoEventWireup="true" CodeBehind="ManageCampaign.aspx.cs" Inherits="WebApp.manage.Campaigns.ManageCampaign" %>



<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">
     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
     
	<h1>Manage Campaign</h1>

	<div class="brclear"></div>    
     
	
	<asp:Panel ID="CampaignDetailsPanel" runat="server">
	
		<asp:Label ID="TitleViewLabel" runat="server" Text="Title: " CssClass="leftLabel"></asp:Label>
			<asp:Label ID="TitleFieldLabel" runat="server" CssClass="textField" />
	      
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
		
		<asp:HyperLink ID="UpdateCampaignHyperLink" runat="server" >Edit Campaign</asp:HyperLink>					
	
	</asp:Panel>         

	<asp:Panel ID="AdGroupsPanel" runat="server">
		<h2>Ad Groups</h2>			
		
          <asp:Button ID="CreateAdGroupPopupButton" runat="server" Text="+ Create a new Ad Group" />
          <br />
          <asp:Label ID="GenericErrorLabel" runat="server" Text="" CssClass="errorMessage"></asp:Label>
          
          <div class="brclear"></div>
          <br />
          <br />

		<asp:GridView ID="AdGroupsGridView" runat="server" AutoGenerateColumns="False" CssClass="gridview">
			<Columns>
				<asp:HyperLinkField HeaderText="Title" DataNavigateUrlFields="adgroup_id" DataTextFormatString ="{0:G}" DataNavigateUrlFormatString="/manage/Adgroups/ManageAdGroup.aspx?adgroup_id={0}" Text="{0}" DataTextField="title" />
				<%--<asp:BoundField DataField="budget" HeaderText="Budget" />		--%>
				<asp:BoundField DataField="start_datetime" HeaderText="Start Date" DataFormatString="{0:dd/MM/yyyy}" htmlencode="false"  />
				<asp:BoundField DataField="end_datetime" HeaderText="End Date" DataFormatString="{0:dd/MM/yyyy}" htmlencode="false"  />	
                    <asp:BoundField DataField="creation_datetime" HeaderText="Created" DataFormatString="{0:dd/MM/yyyy}" htmlencode="false"  />		
			     
			</Columns>	
		</asp:GridView>				
	</asp:Panel>  

     
      <cc1:ModalPopupExtender ID="NewAGroupModalPopupExtender" runat="server" PopupControlID="NewAdGroupPopupPanel" OkControlID="DummyButton"  
               CancelControlID="CancelButton" DropShadow="True" BackgroundCssClass="modalBackground" TargetControlID="CreateAdGroupPopupButton" DynamicServicePath="" Enabled="True">
     </cc1:ModalPopupExtender>
			<asp:Button ID="DummyButton" runat="server" style="display:none" />    


      <asp:Panel ID="NewAdGroupPopupPanel" runat="server" CssClass="modalPopup">
          
          <h2>Create New Ad Group</h2>
          
          <hr />
          
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
<%--
	     <asp:Label ID="BudgetLabel1" runat="server" Text="Ad Group Budget:" CssClass="leftLabel" AssociatedControlID="BudgetTextBox"></asp:Label>
		     <asp:TextBox ID="BudgetTextBox" runat="server" Columns="5" CssClass="textField"></asp:TextBox>
		     <asp:RequiredFieldValidator ID="BudgetRequiredFieldValidator" runat="server" ErrorMessage="Please enter a budget for the AdGroup." ControlToValidate="BudgetTextBox" Display="Dynamic" ValidationGroup="NewAdGroup"></asp:RequiredFieldValidator>
		     <asp:RangeValidator ID="BudgetRangeValidator" runat="server" ControlToValidate="BudgetTextBox" Display="Dynamic" MinimumValue="10" Type="Integer" ValidationGroup="NewAdGroup"></asp:RangeValidator>          

          <div class="brclear"></div>      --%>
       <%--   
          <asp:Label ID="CampaignBudgetLabel" runat="server" Text="Campaign Budget:" CssClass="leftLabel" AssociatedControlID="CampaignBudgetFieldLabel"></asp:Label>
		     <asp:Label ID="CampaignBudgetFieldLabel" runat="server" Columns="5" CssClass="textField"></asp:Label>

          <div class="brclear"></div> 

          <asp:Label ID="AvailableBudgetLabel" runat="server" Text="Available Budget:" CssClass="leftLabel" AssociatedControlID="AvailableBudgetFieldLabel"></asp:Label>
		     <asp:Label ID="AvailableBudgetFieldLabel" runat="server" Columns="5" CssClass="textField"></asp:Label>
          		
          --%>
	     <div class="brclear"></div> 
          
          <asp:Button ID="CreateAdGroupButton" runat="server" Text="Create AdGroup" onclick="CreateAdGroupButton_Click" ValidationGroup="NewAdGroup"/>
     	<asp:Button ID="CancelButton" runat="server" Text="Cancel" CssClass="modalCancelButton" />         
     	
	     <div class="brclear"></div> 

	     <asp:Label ID="CreateAdGroupErrorLabel" runat="server" Text="" CssClass="errorMessage"></asp:Label>
          
     </asp:Panel>
     
        
                       

</asp:Content>

