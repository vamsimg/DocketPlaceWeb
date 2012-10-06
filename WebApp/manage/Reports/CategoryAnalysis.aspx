<%@ Page Title="" Language="C#" MasterPageFile="~/web.Master" AutoEventWireup="true" CodeBehind="CategoryAnalysis.aspx.cs" Inherits="WebApp.manage.Reports.CategoryAnalysis" %>


<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">

       
     <script type="text/javascript">
          function encodeMyHtml(toEncode) {
               return toEncode.replace(/&/gi, '&amp;').replace(/\"/gi, '&quot;').replace(/</gi, '&lt;').replace(/>/gi, '&gt;');
          }
     </script>


     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

	
     <h1>Category Analysis</h1>

     <span class="shortLeftLabel">Store: </span>  <asp:DropDownList ID="StoresDropDownList" runat="server" DataTextField="suburb" DataValueField="store_id" CssClass="textField" />               
     <div class="brclear"></div>    
     
     <span class="shortLeftLabel">Department: </span>  <asp:DropDownList ID="DepartmentsDropDownList" runat="server" CssClass="textField"></asp:DropDownList>

     <div class="brclear"></div>

     <span class="shortLeftLabel">Start Date: </span>     
          
	<asp:TextBox ID="StartDateTextBox" runat="server" Columns="10" CssClass="textField"></asp:TextBox>
		<cc1:CalendarExtender ID="StartDateCalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="StartDateTextBox" Enabled="True"></cc1:CalendarExtender>
		<cc1:MaskedEditExtender ID="StartDateMaskedEditExtender" runat="server" Mask="99/99/9999" MaskType="Date" TargetControlID="StartDateTextBox"  CultureName="en-AU" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="$" CultureDateFormat="DMY" CultureDatePlaceholder="/" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="," CultureTimePlaceholder="" Enabled="True" ></cc1:MaskedEditExtender>
	
     <div class="brclear"></div>

     <span class="shortLeftLabel">End Date: </span>     
     <asp:TextBox ID="EndDateTextBox" runat="server" Columns="10" CssClass="textField"></asp:TextBox>
		<cc1:CalendarExtender ID="EndDateCalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="EndDateTextBox" Enabled="True"></cc1:CalendarExtender>
		<cc1:MaskedEditExtender ID="EndDateMaskedEditExtender" runat="server" Mask="99/99/9999" MaskType="Date" TargetControlID="EndDateTextBox"  CultureName="en-AU" CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="$" CultureDateFormat="DMY" CultureDatePlaceholder="/" CultureDecimalPlaceholder="" CultureThousandsPlaceholder="," CultureTimePlaceholder="" Enabled="True" ></cc1:MaskedEditExtender>
	
     <div class="brclear"></div>
  
     <asp:Button ID="UpdateButton" runat="server" Text="Update" onclick="UpdateButton_Click"/>
     
     <br />
               
    

     <h3>Revenue Summary</h3>

     <asp:GridView ID="SalesSummaryGridView" runat="server" AutoGenerateColumns="False" >
		<Columns>			
			<asp:BoundField DataField="type" HeaderText=""/>
			<asp:BoundField DataField="costs" HeaderText="Costs" />
			<asp:BoundField DataField="sales" HeaderText="Sales"/>		
			<asp:BoundField DataField="profit" HeaderText="Profit"/>				
		</Columns>
	</asp:GridView>

          
     <h3>Customers</h3>


     <asp:Panel ID="MailchimpPanel" runat="server" Enabled="false" Visible="false"  CssClass="simpleBorder">

		<h3>Mailchimp Group</h3>	

		<i>This button will upload the customers below to a Mailchimp group with a title about the Department/Category and the date range selected.</i>
          <br />
          <br />
          

		<div class="brclear"></div> 
	
		<asp:Button ID="MailchimpButton" runat="server" Text="Create Group" onclick="MailchimpButtonButton_Click"/>
	
		<div class="brclear"></div> 
		<br />
		<asp:Label ID="MailchimpErrorLabel" runat="server" Text="" CssClass="errorMessage" EnableViewState="false" ></asp:Label>	
	
	</asp:Panel>

	<asp:Panel ID="SMSPanel" runat="server" Enabled="false" Visible="false"  CssClass="simpleBorder">

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


          <div class="brclear"></div>
	
	
	</asp:Panel>

	<div class="brclear"></div> 
	<br />
	<br />
	<br />
	<br />



     
	<asp:GridView ID="CustomersGridView" runat="server" AutoGenerateColumns="False" DataKeyNames="customer_id" >
		<Columns>						
			<asp:BoundField DataField="title" HeaderText="Title" SortExpression="title" />
			<asp:BoundField DataField="first_name" HeaderText="First Name" SortExpression="first_name" />
			<asp:BoundField DataField="last_name" HeaderText="Last Name" SortExpression="last_name" />			
			<asp:BoundField DataField="suburb" HeaderText="Suburb" SortExpression="suburb" />
              
			<asp:BoundField DataField="mobile" HeaderText="Mobile" SortExpression="mobile" />
               <asp:BoundField DataField="email" HeaderText="Email" SortExpression="mobile" />
			<asp:BoundField DataField="profits" HeaderText="Total Profits" DataFormatString="{0:C0}" />
                              			
			<asp:HyperLinkField DataNavigateUrlFields="customer_id" ItemStyle-CssClass="action" HeaderText="DocketPlace ID"
				DataTextFormatString ="{0:G}"
				DataTextField="customer_id"
				DataNavigateUrlFormatString="/manage/Rewards/ViewCustomer.aspx?customer_id={0}"
				Text="{0}"/>		
		</Columns>
	</asp:GridView>


     <h3>Items</h3>

     <asp:GridView ID="ItemsGridView" runat="server" AutoGenerateColumns="False">
		<Columns>			
			<asp:BoundField DataField="barcode" HeaderText="Product Code"/>			
			<asp:BoundField DataField="description" HeaderText="Description"/>
               <asp:BoundField DataField="quantity" HeaderText="# Sold"/>
               <asp:BoundField DataField="costs" HeaderText="Costs" DataFormatString="{0:C0}" />
               <asp:BoundField DataField="sales" HeaderText="Sales" DataFormatString="{0:C0}" />
               <asp:BoundField DataField="profits" HeaderText="Profits" DataFormatString="{0:C0}"/>
		</Columns>
	</asp:GridView>	



</asp:Content>
