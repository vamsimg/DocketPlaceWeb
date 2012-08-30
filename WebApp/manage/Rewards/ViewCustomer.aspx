<%@ Page Title="" Language="C#" MasterPageFile="~/web.Master" AutoEventWireup="true" CodeBehind="ViewCustomer.aspx.cs" Inherits="WebApp.manage.Rewards.ViewCustomer" %>


<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">
	<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

	<h1>Customer Profile</h1>

	<div id="SMSPanel" style="float:right; width:40%">
		
		<h3>Send SMS</h3>		

		<b>Caller ID: </b>
			<i><asp:Literal ID="SMSCallerIDLiteral" runat="server"></asp:Literal></i>			
		<br />		
		<br />	
		<b>Message</b>
		<br />			
		
		<asp:TextBox ID="MessageTextBox" runat="server" Rows="4" Columns="40" TextMode="MultiLine" ValidationGroup="SMS" ></asp:TextBox>			
		
		<br />
	
		<asp:Button ID="SendSMSButton" runat="server" Text="Send SMS"  ValidationGroup="SMS" onclick="SendSMSButton_Click" />

		<br />
		<span class="errorMessage"><asp:Literal ID="SendSMSLiteral" runat="server" EnableViewState="false"></asp:Literal></span>	
		<asp:RegularExpressionValidator runat="server" ID="MessageRegularExpressionValidator"    ControlToValidate="MessageTextBox"    ValidationExpression="^[\s\S]{0,160}$"   ErrorMessage="Too many characters, enter a maximum of 160."
				Display="Dynamic" CssClass="errorMessage"  ValidationGroup="SMS"></asp:RegularExpressionValidator>
		
	</div>

	<div id="DetatilsPanel" style="width:55%; float:left">
		
		<span class="leftLabel">DocketPlace Customer ID: </span>	
			<asp:Label ID="DocketPlaceCustomerIDLabel" runat="server" CssClass="textField"></asp:Label>
		
		<div class="brclear"></div>		

		<span class="leftLabel">Local Customer ID: </span>	
			<asp:Label ID="LocalCustomerIDLabel" runat="server" CssClass="textField"></asp:Label>
			
		<div class="brclear"></div>

		<span class="leftLabel">Local Barcode ID: </span>	
			<asp:TextBox ID="BarcodeTextBox" runat="server" ValidationGroup="Barcode"></asp:TextBox>
			<asp:Button ID="UpdateBarcodeButton" runat="server" Text="Update" ValidationGroup="Barcode" onclick="UpdateBarcodeButton_Click" />
			<asp:Label ID="UpdateBarcodeLabel" runat="server" CssClass="errorMessage" ViewStateMode="Disabled"></asp:Label>
			
		<div class="brclear"></div>

		<span class="leftLabel">Grade: </span>	
			<asp:Label ID="GradeLabel" runat="server" CssClass="textField"></asp:Label>
		
		<div class="brclear"></div>
		
		<span class="leftLabel">Title: </span>	
			<asp:Label ID="TitleLabel" runat="server" CssClass="textField"></asp:Label>
			
		<div class="brclear"></div>

		<span class="leftLabel">First Name: </span>	
			<asp:Label ID="FirstNameLabel" runat="server" CssClass="textField"></asp:Label>
		
		<div class="brclear"></div>
	
		<span class="leftLabel">Last Name: </span>	
			<asp:Label ID="LastNameLabel" runat="server" CssClass="textField"></asp:Label>
	
		<div class="brclear"></div>

		<span class="leftLabel">Email: </span>	
			<asp:Label ID="EmailLabel" runat="server" CssClass="textField"></asp:Label>
	
		<div class="brclear"></div>

		<span class="leftLabel">Mobile: </span>
			<asp:Label ID="MobileLabel" runat="server" CssClass="textField"></asp:Label>	

		<div class="brclear"></div>

		<span class="leftLabel">Phone: </span>	
			<asp:Label ID="PhoneLabel" runat="server" CssClass="textField"></asp:Label>

		<div class="brclear"></div>		

		<span class="leftLabel">Suburb: </span>
			<asp:Label ID="SuburbLabel" runat="server" CssClass="textField"></asp:Label>
		
		<div class="brclear"></div>		

		<span class="leftLabel">State: </span>
			<asp:Label ID="StateLabel" runat="server" CssClass="textField"></asp:Label>	
	
		<div class="brclear"></div>		

		<span class="leftLabel">Postcode: </span>
			<asp:Label ID="PostcodeLabel" runat="server" CssClass="textField"></asp:Label>	
		
		<div class="brclear"></div>		

	<%--	<span class="leftLabel">No SMS ?</span>
			<asp:CheckBox ID="NoSMSCheckBox" runat="server"  CssClass="textField" />

		<div class="brclear"></div>		
		
		<span class="leftLabel">No Email ?</span>
			<asp:CheckBox ID="NoEmailCheckBox" runat="server" CssClass="textField" />			
		
		<asp:Button ID="UpdateCommsButton" runat="server" Text="Update SMS and Email Receipt" onclick="UpdateCommsButton_Click" />--%>

	</div>

	<div class="brclear"></div>

	<asp:Panel ID="PointsPanel" runat="server" Enabled="false" Visible="false">
		
		<span class="leftLabel">Current Points: </span>	
			<asp:Label ID="CurrentPointsLabel" runat="server" CssClass="textField"></asp:Label>

		<div class="brclear"></div>
		
		<span class="leftLabel">Add Points: </span>	
			<asp:TextBox ID="AddPointsTextBox" runat="server" ValidationGroup="AddPoints" Columns="6">0</asp:TextBox>
			<asp:TextBox ID="AddDescriptionTextBox" runat="server" ValidationGroup="AddPoints"></asp:TextBox>
							
				<cc1:TextBoxWatermarkExtender ID="AddDescriptionTextBox_TextBoxWatermarkExtender" 
					runat="server" Enabled="True" TargetControlID="AddDescriptionTextBox" 
					WatermarkText="Description">
				</cc1:TextBoxWatermarkExtender>
				
			
			<asp:Button ID="AddPointsButton" runat="server" Text="Add      " ValidationGroup="AddPoints" onclick="AddPointsButton_Click" />
			<asp:Label ID="AddPointsErrorLabel" runat="server" CssClass="errorMessage" ViewStateMode="Disabled"></asp:Label>
			
			<asp:RequiredFieldValidator ID="AddDescriptionRequiredFieldValidator" runat="server" ErrorMessage="Please include a description." 
					ControlToValidate="AddDescriptionTextBox" Display="Dynamic" ValidationGroup="AddPoints" />					
		
			<asp:RangeValidator ID="AddPointsRangeValidator"  Display="Dynamic" Type="Integer"
			   MaximumValue="9999" MinimumValue="1" EnableClientScript="true" 
			   ControlToValidate="AddPointsTextBox" runat="server" SetFocusOnError="true" 
			   ErrorMessage="Enter a whole number greater than 0." ValidationGroup="AddPoints" />
			
			

		<div class="brclear"></div>		
			
		<span class="leftLabel">Remove Points: </span>	
			<asp:TextBox ID="RemovePointsTextBox" runat="server" ValidationGroup="RemovePoints" Columns="6">0</asp:TextBox>
			<asp:TextBox ID="RemoveDescriptionTextBox" runat="server" ValidationGroup="RemovePoints"></asp:TextBox>
				<cc1:TextBoxWatermarkExtender ID="RemoveDescriptionTextBox_TextBoxWatermarkExtender" 
				runat="server" Enabled="True" TargetControlID="RemoveDescriptionTextBox" 
				WatermarkText="Description">
			</cc1:TextBoxWatermarkExtender>
				
				

			<asp:Button ID="RemovePointsButton" runat="server" Text="Remove" ValidationGroup="RemovePoints" onclick="RemovePointsButton_Click" />

			<asp:Label ID="RemovePointsErrorLabel" runat="server" CssClass="errorMessage" ViewStateMode="Disabled"></asp:Label>
			
			<asp:RequiredFieldValidator ID="RemoveDescriptionRequiredFieldValidator" runat="server" ErrorMessage="Please include a description." 
					ControlToValidate="RemoveDescriptionTextBox" Display="Dynamic" ValidationGroup="RemovePoints" />
			
			
			<asp:RangeValidator ID="RemovePointsRangeValidator"  Display="Dynamic" Type="Integer"
			   MaximumValue="9999" MinimumValue="1" EnableClientScript="true" 
			   ControlToValidate="RemovePointsTextBox" runat="server" SetFocusOnError="true" 
			   ErrorMessage="Enter a whole number greater than 0." ValidationGroup="RemovePoints">
			</asp:RangeValidator>
		
		</asp:Panel>

	<div class="brclear"></div>

	<h3>Points Log</h3>

	<asp:GridView ID="PointsLogGridView" runat="server" AutoGenerateColumns="False" DataKeyNames="pointslog_id" >
		<EmptyDataTemplate>None</EmptyDataTemplate>
		<Columns>			
			<asp:BoundField DataField="creation_datetime" HeaderText="Date" SortExpression="creation_datetime" DataFormatString="{0:dd-MMM-yyyy}"  />			
			<asp:BoundField DataField="admin_name" HeaderText="Admin" SortExpression="admin_name" />
			<asp:BoundField DataField="description" HeaderText="Description" SortExpression="total" />
			<asp:BoundField DataField="reward_points" HeaderText="Rewards" SortExpression="reward_points" />	
			<asp:TemplateField HeaderText="Docket" SortExpression="">
				<ItemTemplate>

					<asp:HyperLink ID="DocketHyperLink" runat="server" NavigateUrl='<%# Eval("docket_id", "/manage/Rewards/ViewReceipt.aspx?docket_id={0}") %>'
						Text='<%# Eval("docket_id") %>' Enabled='<%# (string)Eval("docket_id") != "" %>'></asp:HyperLink>
				</ItemTemplate>
			</asp:TemplateField>		
						
		</Columns>
	</asp:GridView>
		
	
	
	<h3>Purchases</h3>

	<span class="leftLabel">Total Revenue: </span>	
		<span class="textField"><asp:Literal ID="TotalRevenueLiteral" runat="server"></asp:Literal></span>	
		
	<div class="brclear"></div>

	<span class="leftLabel"># of Sales: </span>	
		<span class="textField"><asp:Literal ID="FrequencyLiteral" runat="server"></asp:Literal></span>	
		
	<div class="brclear"></div>

	<span class="leftLabel">Average Sale: </span>	
		<span class="textField"><asp:Literal ID="AverageSaleLiteral" runat="server"></asp:Literal></span>	
		
	<div class="brclear"></div>

	
	<asp:GridView ID="DocketsGridView" runat="server" AutoGenerateColumns="False" DataKeyNames="docket_id" >
		<EmptyDataTemplate>None</EmptyDataTemplate>
		<Columns>
			<asp:HyperLinkField DataNavigateUrlFields="docket_id" ItemStyle-CssClass="action"
				DataTextField="docket_id"
				DataTextFormatString ="{0:G}"
				DataNavigateUrlFormatString="/manage/Rewards/ViewReceipt.aspx?docket_id={0}"
				Text="{0}"/>					
			<asp:BoundField DataField="creation_datetime" HeaderText="Date" SortExpression="creation_datetime" DataFormatString="{0:dd-MMM-yyyy}"  />			
			<asp:BoundField DataField="total" HeaderText="Total" SortExpression="total" DataFormatString="{0:C}"  />
			<asp:BoundField DataField="reward_points" HeaderText="Rewards" SortExpression="reward_points" />			
		</Columns>
	</asp:GridView>

	
	<h3>Vouchers</h3>
	
	<asp:GridView ID="VouchersGridView" runat="server" AutoGenerateColumns="False" DataKeyNames="voucher_id">
		<EmptyDataTemplate>None</EmptyDataTemplate>
		<Columns>									
			<asp:BoundField DataField="dollar_value" HeaderText="Value" SortExpression="dollar_value" DataFormatString="{0:C}" />
			<asp:BoundField DataField="creation_datetime" HeaderText="Created" SortExpression="creation_datetime"  DataFormatString="{0:dd-MMM-yyyy}"  />
			<asp:BoundField DataField="expiry_datetime" HeaderText="Expires" SortExpression="expiry_datetime"  DataFormatString="{0:dd-MMM-yyyy}" />			
			<asp:BoundField DataField="used_datetime" HeaderText="Used On" SortExpression="used_datetime"  DataFormatString="{0:dd-MMM-yyyy}" />			
		</Columns>
	</asp:GridView>

	<h3>Items Purchased</h3>

	<asp:GridView ID="DocketItemsGridView" runat="server" AutoGenerateColumns="False" EnableViewState="false">
		<Columns>			
			<asp:BoundField DataField="product_code" HeaderText="Product Code"/>
			<asp:BoundField DataField="cost_ex" HeaderText="Cost Ex" DataFormatString="{0:C0}" />	
               <asp:BoundField DataField="sale_ex" HeaderText="Cost Ex" DataFormatString="{0:C0}" />	
               <asp:BoundField DataField="sale_inc" HeaderText="Sale Inc" DataFormatString="{0:C0}" />	
			<asp:BoundField DataField="total_count" HeaderText="# of Sales"  />
			<asp:BoundField DataField="total_revenue" HeaderText="Total Revenue" DataFormatString="{0:C0}" />		
			<asp:BoundField DataField="description" HeaderText="Description"/>
		</Columns>
	</asp:GridView>	


</asp:Content>

