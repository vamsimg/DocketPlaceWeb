<%@ Page Title="" Language="C#" MasterPageFile="~/web.Master" AutoEventWireup="true" CodeBehind="AllCustomers.aspx.cs" Inherits="WebApp.manage.Rewards.AllCustomers" %>


<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

	<h1>Our Customers</h1>
	

	<asp:Panel ID="MailchimpPanel" runat="server" Enabled="false" Visible="false">

		<h3>Mailchimp Sync</h3>	

		<i>This button will upload and update the customers below to the Mailchimp masterlist</i>

		<div class="brclear"></div> 
	
		<asp:Button ID="MailchimpButton" runat="server" Text="Sync" onclick="MailchimpButtonButton_Click"/>
	
		<div class="brclear"></div> 
		<br />
		<asp:Label ID="MailchimpErrorLabel" runat="server" Text="" CssClass="errorMessage" EnableViewState="false"></asp:Label>	
	
	</asp:Panel>

	<asp:Panel ID="SMSPanel" runat="server" Enabled="false" Visible="false">

		<h3>Send SMS</h3>		

		<div id="leftInnerSMSPanel" style="float:left;width:30%">			
			<b>Message:</b>
			<br />			
			<asp:TextBox ID="MessageTextBox" runat="server" Rows="4" Columns="40" TextMode="MultiLine" ValidationGroup="SMS" ></asp:TextBox>			
			<br />
			<br />
			<asp:Button ID="PreviewSMSButton" runat="server" Text="1.Preview" onclick="PreviewSMSButton_Click"  ValidationGroup="SMS" />
			
			<i><asp:Literal ID="PreviewSMSLiteral" runat="server"></asp:Literal></i>


		</div>

		<div id="middleInnerSMSPanel"style="float:left;width:15%;margin-left:2em;" >			
			<b>Notes:</b>
			<br />			
			<asp:TextBox ID="NotesTextBox" runat="server" Rows="4" Columns="30" TextMode="MultiLine"></asp:TextBox>			
			<br />
		</div>

		<div id="rightInnerSMSPanel" style="float:right;width:40%;margin-left:2em;">
						
			<asp:Button ID="CalculateCostButton" runat="server" Text="2.Calculate Costs" onclick="CalculateCostButton_Click"/>
			<br />
			<span class="errorMessage"><asp:Literal ID="SMSCostLiteral" runat="server" EnableViewState="false"></asp:Literal></span>	
		
			<br />
			<br />
			<br />
			<asp:Button ID="SendSMSButton" runat="server" Text="3.Send SMS"  ValidationGroup="SMS" onclick="SendSMSButton_Click" style="height: 26px" />
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
	<h3>Statistics</h3>

	<span class="leftLabel">Total Members: </span><asp:Label ID="TotalMembersLabel" runat="server" CssClass="textField"></asp:Label>

	<div class="brclear"></div>
	
	<span class="leftLabel">Members with Email: </span><asp:Label ID="EmailMembersLabel" runat="server" CssClass="textField"></asp:Label>
	
	<div class="brclear"></div>
	
	<span class="leftLabel">Members with Mobile: </span><asp:Label ID="MobileMembersLabel" runat="server" CssClass="textField"></asp:Label>

	<%--
	<asp:GridView ID="CustomersGridView" runat="server" AutoGenerateColumns="False" DataKeyNames="customer_id" DataSourceID="CustomersSqlDataSource" EnableViewState="false">
		<Columns>			
			<asp:TemplateField>	
				<HeaderTemplate>
					<asp:CheckBox ID="SelectAllCheckBox" Checked="false" runat="server" CssClass="headerCheckBox" EnableViewState="false"/>
				</HeaderTemplate>          
				<ItemTemplate>
					<asp:CheckBox ID="CustomerSelectCheckBox" Checked="false" runat="server" CssClass="itemCheckBox" EnableViewState="false"/>
				</ItemTemplate>
				<ItemStyle HorizontalAlign="Center" />
			 </asp:TemplateField>    
			<asp:BoundField DataField="local_customer_id" HeaderText="Local Customer ID" InsertVisible="False" ReadOnly="True" SortExpression="local_customer_id" />
			<asp:BoundField DataField="store_id" HeaderText="Store ID" InsertVisible="False" ReadOnly="True" SortExpression="store_id" />			
			<asp:BoundField DataField="title" HeaderText="Title" SortExpression="title" />
			<asp:BoundField DataField="first_name" HeaderText="First Name" SortExpression="first_name" />
			<asp:BoundField DataField="last_name" HeaderText="Last Name" SortExpression="last_name" />			
			<asp:BoundField DataField="suburb" HeaderText="Suburb" SortExpression="suburb" />
			<asp:BoundField DataField="mobile" HeaderText="Mobile" SortExpression="mobile" />
			<asp:BoundField DataField="total_revenue" HeaderText="Total Revenue" SortExpression="total_revenue" DataFormatString="{0:C0}" />
			<asp:BoundField DataField="frequency" HeaderText="# of Sales" SortExpression="frequency" />
			<asp:BoundField DataField="average_sale" HeaderText="Average Sale" SortExpression="average_sale" DataFormatString="{0:C0}" />			
			<asp:BoundField DataField="creation_datetime" HeaderText="Created On" SortExpression="creation_datetime" DataFormatString="{0:dd-MMM-yyyy}" />
			<asp:HyperLinkField DataNavigateUrlFields="customer_id" ItemStyle-CssClass="action" HeaderText="DocketPlace ID"
				DataTextFormatString ="{0:G}"
				DataTextField="customer_id"
				DataNavigateUrlFormatString="/manage/Rewards/ViewCustomer.aspx?customer_id={0}"
				Text="{0}"/>		
		</Columns>
	</asp:GridView>

	<asp:SqlDataSource ID="CustomersSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:ConnString %>" 
			SelectCommand="select m.local_customer_id, store_id, c.customer_id , title, first_name , last_name, postcode, mobile , email , suburb, m.creation_datetime , total_revenue, frequency, (total_revenue/ISNULL(NULLIF(frequency,0),1)) as average_sale 
			FROM Customers as c
			inner join Members as m
			on c.customer_id = m.customer_id
			where m.company_id = @company_id
			order by m.total_revenue desc">
		<SelectParameters>
			<asp:SessionParameter Name="company_id" SessionField="company_id" Type="Int32" />
		</SelectParameters>
	</asp:SqlDataSource>
--%>

	

</asp:Content>

