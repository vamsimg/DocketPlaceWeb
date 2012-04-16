<%@ Page Title="" Language="C#" MasterPageFile="~/web.Master" AutoEventWireup="true" CodeBehind="Triggers.aspx.cs" Inherits="WebApp.manage.Rewards.Triggers" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">

	<h1>Triggers</h1>

	
		
	<asp:GridView ID="TriggersGridView" runat="server" AutoGenerateColumns="False" DataKeyNames="trigger_id" >
		<Columns>
			<asp:TemplateField> 
				<ItemTemplate>
					<asp:ImageButton ID="UpImageButton" AlternateText="Move Up" runat="server" ToolTip="Move Up" ImageUrl="/images/arrow_up.png" CommandArgument='<%# Eval("trigger_id") %>' OnCommand="UpImageButton_Command" />						
				</ItemTemplate> 
				<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
			</asp:TemplateField>
			<asp:TemplateField> 
				<ItemTemplate>
					<asp:ImageButton ID="DownImageButton" AlternateText="Delete" runat="server"   ToolTip="Move Down" ImageUrl="/images/arrow_down.png" CommandArgument='<%# Eval("trigger_id") %>' OnCommand="DownImageButton_Command" />						
				</ItemTemplate> 
				<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
			</asp:TemplateField>
			<asp:BoundField DataField="priority" HeaderText="Priority"/>
			<asp:BoundField DataField="suburb" HeaderText="Store"/>
			<asp:BoundField DataField="trigger_type" HeaderText="Type"/>			
			<asp:BoundField DataField="value" HeaderText="Value"/>			
			<asp:HyperLinkField DataNavigateUrlFields="uploadedad_id" ItemStyle-CssClass="action" HeaderText="Ad"
				DataTextField="ad_title"
				DataTextFormatString="{0}"
				DataNavigateUrlFormatString="/manage/UploadedAds/UpdateUploadedAd.aspx?uploaded_id={0}"
				/>	
			<asp:TemplateField> 
				<ItemTemplate>
					<asp:ImageButton ID="DeleteImageButton" AlternateText="Delete" runat="server"   ToolTip="Make Supplier" ImageUrl="/images/delete.png" CommandArgument='<%# Eval("trigger_id") %>' OnCommand="DeleteImageButton_Command" />						
				</ItemTemplate> 
				<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
			</asp:TemplateField>		
		</Columns>
	</asp:GridView>
		

	<br />
	<br />


	<div id="CreateTriggerPanel">
	
		
		<asp:Button ID="CreateButton" runat="server" Text="Create" onclick="CreateButton_Click" /> <asp:Label ID="CreateTriggerErrorLabel" runat="server" EnableViewState="false" CssClass="errorMessage"></asp:Label>
		<br />
		<br />		

		<div class="brclear"></div>

		<span class="leftLabel">Select Store: </span>
		<asp:DropDownList ID="StoresDropDownList" runat="server" CssClass="textField" DataTextField="suburb" DataValueField="store_id" EnableViewState="true">			     
		</asp:DropDownList>
	
		<div class="brclear"></div>

		<span class="leftLabel">Select Trigger Type: </span>
		<asp:DropDownList ID="TriggerTypeDropDownList" runat="server" CssClass="textField" onselectedindexchanged="TriggerTypeDropDownList_SelectedIndexChanged" AutoPostBack="true">			     
			<asp:ListItem Text="Is Member?" Value="Member"></asp:ListItem>
			<asp:ListItem Text="Number of Points" Value="Points"></asp:ListItem>
			<asp:ListItem Text="Purchase Total" Value="Purchase"></asp:ListItem>			
		</asp:DropDownList>
	
		<asp:Panel ID="MemberPanel" runat="server">		
		
			<asp:RadioButtonList ID="IsMemberRadioButtonList" runat="server">
				<asp:ListItem Text="Yes" Value="true" Selected="True"></asp:ListItem>
				<asp:ListItem Text="No" Value="false"></asp:ListItem>	
			</asp:RadioButtonList>			
	
		</asp:Panel>

		<asp:Panel ID="PointsPanel" runat="server" Visible="false" Enabled="false">
	
			<asp:TextBox ID="PointsTextBox" runat="server" Text="500"></asp:TextBox>
				<br />
				<asp:RangeValidator ID="PointsRangeValidator" runat="server" ErrorMessage="Please enter a whole number between 1 and 10000." Display="Dynamic" MinimumValue="1" MaximumValue="10000" ControlToValidate="PointsTextBox" Type="Integer" ></asp:RangeValidator>

		</asp:Panel>

		<asp:Panel ID="PurchasePanel" runat="server" Visible="false" Enabled="false">
			
			$<asp:TextBox ID="PurchaseTextBox" runat="server" Text="50"></asp:TextBox>
				<br />
				<asp:RangeValidator ID="PurchaseRangeValidator" runat="server" ErrorMessage="Please enter a whole amount between 1 and 10000." Display="Dynamic" MinimumValue="1" MaximumValue="10000" ControlToValidate="PurchaseTextBox" Type="Integer" ></asp:RangeValidator>

		</asp:Panel>

		<div class="brclear"></div>

		<span class="leftLabel">Select Ad to Display: </span>
	
		<asp:DropDownList ID="AdsDropDownList" runat="server" CssClass="textField" 
			AutoPostBack="True" onselectedindexchanged="AdsDropDownList_SelectedIndexChanged" >			     
		</asp:DropDownList>
	
		<div class="brclear"></div>
	
		<asp:Image ID="AdImage" runat="server" />
	
		<br />

		<span class="leftLabel">Footer: </span> <span class="textField"><asp:Literal ID="FooterLiteral" runat="server"></asp:Literal></span>
	
	
	</div>

	

</asp:Content>

