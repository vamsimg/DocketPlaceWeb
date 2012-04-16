<%@ Page Title="" Language="C#" MasterPageFile="~/web.Master" AutoEventWireup="true" CodeBehind="ViewCustomerList.aspx.cs" Inherits="WebApp.manage.Rewards.ViewCustomerList" %>


<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

	<h1>View Customer List</h1>
	
	<span class="leftLabel">List Title</span>		
		<asp:Label ID="TitleLabel" runat="server" CssClass="textField"></asp:Label>        
	<div class="brclear"></div> 

	<span class="leftLabel">Notes</span>
		<asp:Label ID="NotesLabel" runat="server" CssClass="textField"></asp:Label>        
	
	<div class="brclear"></div> 

	<asp:Panel ID="MailchimpPanel" runat="server" Enabled="false" Visible="false">

		<h3>Mailchimp Sync</h3>	

		<i>This button will upload the customers below to a Mailchimp group. If the group doesn't already exist then a new one with the title above will be created. Otherwise the current members in the Mailchimp group will be deleted and the ones below added.</i>

		<div class="brclear"></div> 
	
		<asp:Button ID="MailchimpButton" runat="server" Text="Sync" onclick="MailchimpButtonButton_Click"/>
	
		<div class="brclear"></div> 
		<br />
		<asp:Label ID="MailchimpErrorLabel" runat="server" Text="" CssClass="errorMessage" EnableViewState="false" ></asp:Label>	
	
	</asp:Panel>

	<asp:Panel ID="SMSPanel" runat="server" Enabled="false" Visible="false">

		<h3>Send SMS</h3>		

		<div id="leftInnerSMSPanel" style="float:left;width:30%">			
			<b>Message:</b>
			<br />			
			<asp:TextBox ID="MessageTextBox" runat="server" Rows="4" Columns="40" TextMode="MultiLine" ValidationGroup="SMS" ></asp:TextBox>			
			<br />
			<asp:Button ID="PreviewSMSButton" runat="server" Text="Preview" onclick="PreviewSMSButton_Click"  ValidationGroup="SMS" />
			
			<i><asp:Literal ID="PreviewSMSLiteral" runat="server"></asp:Literal></i>

		</div>

		<div id="middleInnerSMSPanel"style="float:left;width:15%;margin-left:2em;" >			
			<b>Notes:</b>
			<br />			
			<asp:TextBox ID="NotesTextBox" runat="server" Rows="4" Columns="30" TextMode="MultiLine"  ValidationGroup="SMS"></asp:TextBox>			
			<br />
		</div>

		<div id="rightInnerSMSPanel" style="float:right;width:40%;margin-left:2em;">
						
			<asp:Button ID="CalculateCostButton" runat="server" Text="Calculate Costs" onclick="CalculateCostButton_Click"  ValidationGroup="SMS"/>
			<br />
			<span class="errorMessage"><asp:Literal ID="SMSCostLiteral" runat="server" EnableViewState="false"></asp:Literal></span>	
		
			<br />
			<br />
			<br />
			<asp:Button ID="SendSMSButton" runat="server" Text="Send SMS"  ValidationGroup="SMS" onclick="SendSMSButton_Click" style="height: 26px" />
				<ajaxToolkit:ConfirmButtonExtender ID="SendSMSConfirmButtonExtender" runat="server" ConfirmText="Are you sure you wish to send this SMS ?" TargetControlID="SendSMSButton">
				</ajaxToolkit:ConfirmButtonExtender>	
			<br />
			
			<asp:RegularExpressionValidator runat="server" ID="MessageRegularExpressionValidator"    ControlToValidate="MessageTextBox"    ValidationExpression="^[\s\S]{30,140}$"   ErrorMessage="Too many characters, enter a maximum of 140 and at least 30."
					Display="Dynamic" CssClass="errorMessage"  ValidationGroup="SMS"></asp:RegularExpressionValidator>
			<span class="errorMessage"><asp:Literal ID="SendSMSLiteral" runat="server" EnableViewState="false"></asp:Literal></span>			
				
		</div>

	
	</asp:Panel>

	<div class="brclear"></div> 
	<br />
	<br />
	<br />
	<br />
	

	<asp:GridView ID="CustomersGridView" runat="server" AutoGenerateColumns="False" DataKeyNames="customer_id">
		<Columns>						
			<asp:BoundField DataField="local_customer_id" HeaderText="Local Customer ID" InsertVisible="False" ReadOnly="True" SortExpression="local_customer_id" />
			<asp:BoundField DataField="store_id" HeaderText="Store ID" InsertVisible="False" ReadOnly="True" SortExpression="store_id" />			
			<asp:BoundField DataField="title" HeaderText="Title" SortExpression="title" />
			<asp:BoundField DataField="first_name" HeaderText="First Name" SortExpression="first_name" />
			<asp:BoundField DataField="last_name" HeaderText="Last Name" SortExpression="last_name" />			
			<asp:BoundField DataField="suburb" HeaderText="Suburb" SortExpression="suburb" />
			<asp:BoundField DataField="mobile" HeaderText="Mobile" SortExpression="mobile" />
			<asp:BoundField DataField="total_revenue" HeaderText="Total Revenue" SortExpression="total_revenue" DataFormatString="{0:C0}" />
			<asp:BoundField DataField="frequency" HeaderText="# of Sales" SortExpression="frequency" />
			<asp:BoundField DataField="creation_datetime" HeaderText="Created On" SortExpression="creation_datetime" DataFormatString="{0:dd-MMM-yyyy}" />
			<asp:HyperLinkField DataNavigateUrlFields="customer_id" ItemStyle-CssClass="action" HeaderText="DocketPlace ID"
				DataTextFormatString ="{0:G}"
				DataTextField="customer_id"
				DataNavigateUrlFormatString="/manage/Rewards/ViewCustomer.aspx?customer_id={0}"
				Text="{0}"/>		
		</Columns>
	</asp:GridView>
	

</asp:Content>

